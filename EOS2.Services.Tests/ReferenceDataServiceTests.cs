namespace EOS2.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Services.BusinessDomain;

    using Moq;

    using NUnit.Framework;

    public abstract class ReferenceDataServiceTestsBase
    {
        protected Mock<IRepository<EquipmentType>> MockEquipmentTypeRepository { get; set; }

        protected Mock<IRepository<InstrumentType>> MockInstrumentTypeRepository { get; set; }

        protected Mock<IRepository<ScheduleFrequency>> MockFrequencyRepository { get; set; }

        protected Mock<IRepository<ChannelType>> MockChannelTypeRepository { get; set; }

        protected Mock<IRepository<CalibrationFrequency>> MockCalibrationFrequencyRepository { get; set; }

        protected Mock<IRepository<CertificateType>> MockCertificateTypeRepository { get; set; }
        
        [SetUp]
        public void FixtureSetup()
        {
            MockEquipmentTypeRepository = new Mock<IRepository<EquipmentType>>();
            MockInstrumentTypeRepository = new Mock<IRepository<InstrumentType>>();
            MockFrequencyRepository = new Mock<IRepository<ScheduleFrequency>>();
            MockChannelTypeRepository = new Mock<IRepository<ChannelType>>();
            MockCalibrationFrequencyRepository = new Mock<IRepository<CalibrationFrequency>>();
            MockCertificateTypeRepository = new Mock<IRepository<CertificateType>>();
        }

        public IReferenceDataService ServiceUnderTest()
        {
            return new ReferenceDataService(
                MockEquipmentTypeRepository.Object,
                MockInstrumentTypeRepository.Object,
                MockFrequencyRepository.Object, 
                MockChannelTypeRepository.Object, 
                MockCalibrationFrequencyRepository.Object,
                MockCertificateTypeRepository.Object);
        }        
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class GetEquipmentTypesMethod : ReferenceDataServiceTestsBase
    {
        [Test]
        public void ReturnsListOfEquipmentTypes()
        {
            var equipmentTypeList = new List<EquipmentType>
                                        {
                                            new EquipmentType { Id = 1, Name = "Type 1" },
                                            new EquipmentType { Id = 2, Name = "Type 2" }
                                        };

            MockEquipmentTypeRepository.Setup(r => r.GetAll()).Returns(equipmentTypeList);

            var result = this.ServiceUnderTest().GetEquipmentTypes();

            Assert.That(result, Is.InstanceOf<IEnumerable<EquipmentType>>());
            Assert.That(result, Is.Not.Empty);
        }
    }

    [TestFixture]
    public class GetInstrumentTypesMethod : ReferenceDataServiceTestsBase
    {
        [Test]
        public void ReturnsListOfInstrumentTypes()
        {
            var instrumentTypeList = new List<InstrumentType>
                                        {
                                            new InstrumentType { Id = 1, Name = "Type 1" },
                                            new InstrumentType { Id = 2, Name = "Type 2" }
                                        };    
        
            MockInstrumentTypeRepository.Setup(r => r.GetAll()).Returns(instrumentTypeList);

            var result = this.ServiceUnderTest().GetInstrumentTypes();

            Assert.That(result, Is.InstanceOf<IEnumerable<InstrumentType>>());
            Assert.That(result, Is.Not.Empty);
        }
    }

    [TestFixture]
    public class GetScheduleFrequenciesMethod : ReferenceDataServiceTestsBase
    {
        [Test]
        public void ReturnsListOfScheduleFrequencies()
        {
            var scheduleFrequenciesList = new List<ScheduleFrequency>
                                        {
                                            new ScheduleFrequency { Id = 1, Name = "Type 1" },
                                            new ScheduleFrequency { Id = 2, Name = "Type 2" }
                                        };    
        
            MockFrequencyRepository.Setup(r => r.GetAll()).Returns(scheduleFrequenciesList);

            var result = this.ServiceUnderTest().GetScheduleFrequencies();

            Assert.That(result, Is.InstanceOf<IEnumerable<ScheduleFrequency>>());
            Assert.That(result, Is.Not.Empty);            
        }
    }

    [TestFixture]
    public class GetChannelTypesMethod : ReferenceDataServiceTestsBase
    {
        [Test]
        public void ReturnsListOfChannelTypes()
        {
            var channelTypesList = new List<ChannelType>
                                        {
                                            new ChannelType { Id = 1, Name = "Type 1" },
                                            new ChannelType { Id = 2, Name = "Type 2" }
                                        };    
        
            MockChannelTypeRepository.Setup(r => r.GetAll()).Returns(channelTypesList);

            var result = this.ServiceUnderTest().GetChannelTypes();

            Assert.That(result, Is.InstanceOf<IEnumerable<ChannelType>>());
            Assert.That(result, Is.Not.Empty);                        
        }
    }

    [TestFixture]
    public class GetCalibrationFrequenciesMethod : ReferenceDataServiceTestsBase
    {
        [Test]
        public void ReturnsListOfCalibrationFrequencies()
        {
            var calibrationFrequenciesList = new List<CalibrationFrequency>
                                        {
                                            new CalibrationFrequency { Id = 1, Name = "Type 1" },
                                            new CalibrationFrequency { Id = 2, Name = "Type 2" }
                                        };    
        
            MockCalibrationFrequencyRepository.Setup(r => r.GetAll()).Returns(calibrationFrequenciesList);

            var result = this.ServiceUnderTest().GetCalibrationFrequencies();

            Assert.That(result, Is.InstanceOf<IEnumerable<CalibrationFrequency>>());
            Assert.That(result, Is.Not.Empty);              
        }
    }

        [TestFixture]
        public class GetEquipmentCertificateTypesMethod : ReferenceDataServiceTestsBase
        {
            [Test]
            public void ReturnsCertificateTypes()
            {
                // ARRANGE
                var certificateTypeList = new List<CertificateType>
                                              {
                                                  new CertificateType { Id = 1, Name = "CertificateType 1", IsEquipmentApplicable = true, IsInstrumentApplicable = false },
                                                  new CertificateType { Id = 2, Name = "CertificateType 2", IsEquipmentApplicable = true, IsInstrumentApplicable = false },
                                                  new CertificateType { Id = 3, Name = "CertificateType 3", IsEquipmentApplicable = false, IsInstrumentApplicable = true }
                                              };

                var foundCertificateTypes = new List<CertificateType>();

                // ReSharper disable PossibleMultipleEnumeration
                MockCertificateTypeRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<CertificateType, bool>>>()))
                    .Callback((Expression<Func<CertificateType, bool>> pred) => foundCertificateTypes = certificateTypeList.AsQueryable().Where(pred).ToList())
                    .Returns(() => foundCertificateTypes);
                //// ReSharper restore PossibleMultipleEnumeration

                var service = this.ServiceUnderTest();

                // ACT 
                var results = service.GetEquipmentCertificateTypes();

                // ASSERT
                Assert.That(results, Is.Not.Empty);
                Assert.That(results.Count(), Is.EqualTo(2));

                MockCertificateTypeRepository.Verify();
            }
        }

        [TestFixture]
        public class GetInstrumentCertificateTypesMethod : ReferenceDataServiceTestsBase
        {
            [Test]
            public void ReturnsCertificateTypes()
            {
                // ARRANGE
                var certificateTypeList = new List<CertificateType>
                                              {
                                                  new CertificateType { Id = 1, Name = "CertificateType 1", IsEquipmentApplicable = true, IsInstrumentApplicable = false },
                                                  new CertificateType { Id = 2, Name = "CertificateType 2", IsEquipmentApplicable = true, IsInstrumentApplicable = false },
                                                  new CertificateType { Id = 3, Name = "CertificateType 3", IsEquipmentApplicable = false, IsInstrumentApplicable = true }
                                              };

                var foundCertificateTypes = new List<CertificateType>();

                // ReSharper disable PossibleMultipleEnumeration
                MockCertificateTypeRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<CertificateType, bool>>>()))
                    .Callback((Expression<Func<CertificateType, bool>> pred) => foundCertificateTypes = certificateTypeList.AsQueryable().Where(pred).ToList())
                    .Returns(() => foundCertificateTypes);
                //// ReSharper restore PossibleMultipleEnumeration

                var service = this.ServiceUnderTest();

                // ACT 
                var results = service.GetInstrumentCertificateTypes();

                // ASSERT
                Assert.That(results, Is.Not.Empty);
                Assert.That(results.Count(), Is.EqualTo(1));

                MockCertificateTypeRepository.Verify();
            }
        }
}
