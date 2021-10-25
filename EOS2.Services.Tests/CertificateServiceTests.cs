namespace EOS2.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using EOS2.Model;

    using Moq;

    using NUnit.Framework;

    public class CertificateServiceTests
    {
        [TestFixture]
        public class GetCertificateMethod : CertificateServiceTestsBase
        {
            [Test]
            public void ReturnsFoundCertificate()
            {
                // ARRANGE
                var certificateList = new List<CertificateHeader>
                                          {
                                              new CertificateHeader { Id = 1 },
                                              new CertificateHeader { Id = 2 }
                                          };

                CertificateHeader foundCertificate = null;

                MockCertificateHeaderRepository.Setup(
                    m => m.Find(It.IsAny<Expression<Func<CertificateHeader, bool>>>()))
                    .Callback(
                        (Expression<Func<CertificateHeader, bool>> pred) =>
                        foundCertificate = certificateList.AsQueryable().SingleOrDefault(pred))
                    .Returns(() => foundCertificate);

                const int IdToUse = 1;

                var certificateService = this.ServiceUnderTest();

                // ACT 
                var result = certificateService.GetCertificate(IdToUse);

                // ASSERT
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(1));

                MockCertificateHeaderRepository.Verify();
            }

            [Test]
            public void ReturnsNullWhenNoCertificateFound()
            {
                // ARRANGE
                var certificateList = new List<CertificateHeader>
                                          {
                                              new CertificateHeader { Id = 1 },
                                              new CertificateHeader { Id = 2 }
                                          };

                CertificateHeader foundCertificate = null;

                MockCertificateHeaderRepository.Setup(
                    m => m.Find(It.IsAny<Expression<Func<CertificateHeader, bool>>>()))
                    .Callback(
                        (Expression<Func<CertificateHeader, bool>> pred) =>
                        foundCertificate = certificateList.AsQueryable().SingleOrDefault(pred))
                    .Returns(() => foundCertificate);

                const int IdToUse = 3;

                var certificateService = this.ServiceUnderTest();

                // ACT 
                var result = certificateService.GetCertificate(IdToUse);

                // ASSERT
                Assert.That(result, Is.Null);

                MockCertificateHeaderRepository.Verify();
            }
        }

        [TestFixture]
        public class SaveMethod : CertificateServiceTestsBase
        {
            [Test]
            public void SaveCertificateToRepositoryWithNullModel()
            {
                var certificateService = this.ServiceUnderTest();

                // ACT ( Run the actual Test ) &  Assert ( Test its worked )
                Assert.Throws<ArgumentNullException>(() => certificateService.Save(null));
            }

            [Test]
            public void AddCertificateToRepository()
            {
                var certificateList = new List<CertificateHeader>();
                var certificateToAdd = new CertificateHeader();

                MockCertificateHeaderRepository.Setup(a => a.Add(It.IsAny<CertificateHeader>()))
                    .Callback((CertificateHeader certificate) => certificateList.Add(certificate));
                MockCertificateHeaderRepository.Setup(a => a.GetAll()).Returns(certificateList);

                var certificateService = this.ServiceUnderTest();

                // ACT ( Run the actual Test )
                certificateService.Save(certificateToAdd);

                // Assert ( Test its worked )
                Assert.That(certificateList, Is.Not.Empty);
            }

            [Test]
            public void SaveCertificateToRepositoryWithCorrectInstrumentId()
            {
                const int NewInstrumentId = 1;

                var certificateList = new List<CertificateHeader>();
                var certificateToAdd = new CertificateHeader { InstrumentId = NewInstrumentId };

                MockCertificateHeaderRepository.Setup(a => a.Add(It.IsAny<CertificateHeader>()))
                    .Callback((CertificateHeader certificate) => certificateList.Add(certificate));
                MockCertificateHeaderRepository.Setup(a => a.GetAll()).Returns(certificateList);

                var certificateService = this.ServiceUnderTest();

                // ACT ( Run the actual Test )
                certificateService.Save(certificateToAdd);

                // Assert ( Test its worked )
                Assert.That(certificateList.Count(), Is.EqualTo(1));
                Assert.That(certificateList.Count(c => c.InstrumentId == NewInstrumentId), Is.EqualTo(1));
            }

            [Test]
            public void SaveCertificateUpdatesExisting()
            {
                const int OwningInstrumentId = 1;

                var certificateList = new List<CertificateHeader>
                                          {
                                              new CertificateHeader { Id = 1, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0001" },
                                              new CertificateHeader { Id = 2, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0002" }
                                          };

                MockCertificateHeaderRepository.Setup(a => a.Update(It.IsAny<CertificateHeader>()))
                    .Callback(
                        (CertificateHeader certificate) =>
                            {
                                certificateList.Single(c => c.Id == certificate.Id).CertificateNumber =
                                    certificate.CertificateNumber;
                            });

                var certificateService = this.ServiceUnderTest();

                var certificateToUpdate = new CertificateHeader { Id = 1, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0003" };

                // ACT ( Run the actual Test )
                certificateService.Save(certificateToUpdate);

                Assert.That(certificateList.Single(e => e.Id == 1).CertificateNumber, Is.EqualTo(certificateToUpdate.CertificateNumber));
                Assert.That(certificateList.Single(e => e.Id == 2).CertificateNumber, Is.Not.EqualTo(certificateToUpdate.CertificateNumber));
            }
        }

        [TestFixture]
        public class CertificateExistsMethod : CertificateServiceTestsBase
        {
            [Test]
            public void ReturnsTrueIfAddingCertificateFoundByNumberAndOrganization()
            {
                // ARRANGE
                const int OwningInstrumentId = 1;
                var certificateList = new List<CertificateHeader>
                                          {
                                              new CertificateHeader 
                                                    { 
                                                        Id = 1, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0001",
                                                        Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                    },
                                              new CertificateHeader
                                                    {
                                                        Id = 2, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0002",
                                                        Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                    }
                                          };

                CertificateHeader foundCertificate = null;

                MockCertificateHeaderRepository.Setup(
                    m => m.Find(It.IsAny<Expression<Func<CertificateHeader, bool>>>()))
                    .Callback((Expression<Func<CertificateHeader, bool>> pred) => foundCertificate = certificateList.AsQueryable().SingleOrDefault(pred))
                    .Returns(() => foundCertificate);

                const int InstrumentIdToUse = 1;
                const string CertificateNumberToFind = "CN0001";
                const int NewCertificateId = new int();
                const int OrganizationIdToUse = 1;

                var certificateService = this.ServiceUnderTest();

                // ACT 
                var result = certificateService.CertificateExists(CertificateNumberToFind, InstrumentIdToUse, NewCertificateId, OrganizationIdToUse);

                // ASSERT
                Assert.That(result, Is.True);

                MockCertificateHeaderRepository.Verify();
            }

            [Test]
            public void ReturnsFalseIfAddingCertificateFoundByNumberNotOrganization()
            {
                // ARRANGE
                const int OwningInstrumentId = 1;
                var certificateList = new List<CertificateHeader>
                                          {
                                              new CertificateHeader
                                                  {
                                                      Id = 1, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0001",
                                                      Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                  },
                                              new CertificateHeader
                                                  {
                                                      Id = 2, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0002",
                                                        Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                  }
                                          };

                CertificateHeader foundCertificate = null;

                MockCertificateHeaderRepository.Setup(m => m.Find(It.IsAny<Expression<Func<CertificateHeader, bool>>>()))
                    .Callback((Expression<Func<CertificateHeader, bool>> pred) => foundCertificate = certificateList.AsQueryable().SingleOrDefault(pred))
                    .Returns(() => foundCertificate);

                const int InstrumentIdToUse = 1;
                const string CertificateNumberToFind = "CN0001";
                const int NewCertificateId = new int();
                const int OrganizationIdToUse = 2;

                var certificateService = this.ServiceUnderTest();

                // ACT 
                var result = certificateService.CertificateExists(CertificateNumberToFind, InstrumentIdToUse, NewCertificateId, OrganizationIdToUse);

                // ASSERT
                Assert.That(result, Is.False);

                MockCertificateHeaderRepository.Verify();
            }

            [Test]
            public void ReturnsFalseIfAddingEquipmentNotFoundByName()
            {
                // ARRANGE
                const int OwningInstrumentId = 1;
                var certificateList = new List<CertificateHeader>
                                          {
                                              new CertificateHeader
                                                  {
                                                      Id = 1, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0001",
                                                      Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                  },
                                              new CertificateHeader
                                                  {
                                                      Id = 2, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0002",
                                                      Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                  }
                                          };

                CertificateHeader foundCertificate = null;

                MockCertificateHeaderRepository.Setup(m => m.Find(It.IsAny<Expression<Func<CertificateHeader, bool>>>()))
                    .Callback((Expression<Func<CertificateHeader, bool>> pred) => foundCertificate = certificateList.AsQueryable().SingleOrDefault(pred))
                    .Returns(() => foundCertificate);

                const int InstrumentIdToUse = 1;
                const string CertificateNumberToFind = "CN0003";
                const int NewCertificateId = new int();
                const int OrganizationIdToUse = 1;

                var certificateService = this.ServiceUnderTest();

                // ACT 
                var result = certificateService.CertificateExists(CertificateNumberToFind, InstrumentIdToUse, NewCertificateId, OrganizationIdToUse);

                // ASSERT
                Assert.That(result, Is.False);

                MockCertificateHeaderRepository.Verify();
            }

            [Test]
            public void ReturnsTrueIfUpdatingCertificateFoundByNumberAndOrganization()
            {
                const int OwningInstrumentId = 1;
                var certificateList = new List<CertificateHeader>
                                          {
                                              new CertificateHeader
                                                  {
                                                      Id = 1, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0001",
                                                        Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                  },
                                              new CertificateHeader
                                                  {
                                                      Id = 2, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0002",
                                                        Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                  }
                                          };

                CertificateHeader foundCertificate = null;

                MockCertificateHeaderRepository.Setup(m => m.Find(It.IsAny<Expression<Func<CertificateHeader, bool>>>()))
                    .Callback((Expression<Func<CertificateHeader, bool>> pred) => foundCertificate = certificateList.AsQueryable().SingleOrDefault(pred))
                    .Returns(() => foundCertificate);

                const int InstrumentIdToUse = 1;
                const string CertificateNumberToFind = "CN0002";
                const int CertificateIdToUse = 1;
                const int OrganizationIdToUse = 1;

                var certificateService = this.ServiceUnderTest();

                // ACT 
                var result = certificateService.CertificateExists(CertificateNumberToFind, InstrumentIdToUse, CertificateIdToUse, OrganizationIdToUse);

                // ASSERT
                Assert.That(result, Is.True);

                MockCertificateHeaderRepository.Verify();
            }

            [Test]
            public void ReturnsFalseIfUpdatingCertificateFoundByNumberNotOrganization()
            {
                // ARRANGE
                const int OwningInstrumentId = 1;
                var certificateList = new List<CertificateHeader>
                                          {
                                              new CertificateHeader
                                                  {
                                                      Id = 1, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0001",
                                                        Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                  },
                                              new CertificateHeader
                                                  {
                                                      Id = 2, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0002",
                                                        Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 2 } } } 
                                                  }
                                          };

                CertificateHeader foundCertificate = null;

                MockCertificateHeaderRepository.Setup(m => m.Find(It.IsAny<Expression<Func<CertificateHeader, bool>>>()))
                    .Callback((Expression<Func<CertificateHeader, bool>> pred) => foundCertificate = certificateList.AsQueryable().SingleOrDefault(pred))
                    .Returns(() => foundCertificate);

                const int InstrumentIdToUse = 1;
                const string CertificateNumberToFind = "CN0002";
                const int CertificateIdToUse = 1;
                const int OrganizationIdToUse = 1;

                var certificateService = this.ServiceUnderTest();

                // ACT 
                var result = certificateService.CertificateExists(CertificateNumberToFind, InstrumentIdToUse, CertificateIdToUse, OrganizationIdToUse);

                // ASSERT
                Assert.That(result, Is.False);

                MockCertificateHeaderRepository.Verify();
            }

            [Test]
            public void ReturnsFalseIfUpdatingEquipmentNotFoundByName()
            {
                const int OwningInstrumentId = 1;
                var certificateList = new List<CertificateHeader>
                                          {
                                              new CertificateHeader
                                                  {
                                                      Id = 1, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0001",
                                                        Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                  },
                                              new CertificateHeader
                                                  {
                                                      Id = 2, InstrumentId = OwningInstrumentId, CertificateNumber = "CN0002",
                                                        Instrument = new Instrument { PlantArea = new PlantArea { Site = new Site { OrganizationId = 1 } } } 
                                                  }
                                          };

                CertificateHeader foundCertificate = null;

                MockCertificateHeaderRepository.Setup(m => m.Find(It.IsAny<Expression<Func<CertificateHeader, bool>>>()))
                    .Callback((Expression<Func<CertificateHeader, bool>> pred) => foundCertificate = certificateList.AsQueryable().SingleOrDefault(pred))
                    .Returns(() => foundCertificate);

                const int InstrumentIdToUse = 1;
                const string CertificateNumberToFind = "CN0003";
                const int CertificateIdToUse = 1;
                const int OrganizationIdToUse = 1;

                var certificateService = this.ServiceUnderTest();

                // ACT 
                var result = certificateService.CertificateExists(CertificateNumberToFind, InstrumentIdToUse, CertificateIdToUse, OrganizationIdToUse);

                // ASSERT
                Assert.That(result, Is.False);
            }
        }
    }
}
