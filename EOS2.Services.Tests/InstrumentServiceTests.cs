namespace EOS2.Services.Tests.InstrumentServiceTests
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

    public abstract class InstrumentServiceTestsBase
    {
        protected Mock<IRepository<Instrument>> MockInstrumentRepository { get; set; }

        protected Mock<IRepository<Channel>> MockChannelRepository { get; set; }

        protected Mock<IRepository<InstrumentType>> MockTypeRepository { get; set; }

        protected Mock<IRepository<CalibrationFrequency>> MockFrequencyRepository { get; set; }
       
        [SetUp]
        public void FixtureSetup()
        {
            MockInstrumentRepository = new Mock<IRepository<Instrument>>();
            MockChannelRepository = new Mock<IRepository<Channel>>();
            MockTypeRepository = new Mock<IRepository<InstrumentType>>();
            MockFrequencyRepository = new Mock<IRepository<CalibrationFrequency>>();
        }

        public IInstrumentService SubjectUnderTest()
        {
            return new InstrumentService(MockInstrumentRepository.Object, MockChannelRepository.Object);            
        }        
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class SaveInstrumentMethod : InstrumentServiceTestsBase
    {
        [Test]
        public void SaveInstrumentToRepositoryWithNullModel()
        {
            var service = this.SubjectUnderTest();

            // ACT ( Run the actual Test ) &  Assert ( Test its worked )
            Assert.Throws<ArgumentNullException>(() => service.SaveInstrument(null));
        }

        [Test]
        public void AddsInstrumentToRepository()
        {
            var itemList = new List<Instrument>();
            var itemToAdd = new Instrument();

            MockInstrumentRepository.Setup(a => a.Add(It.IsAny<Instrument>())).Callback((Instrument instrument) => itemList.Add(instrument));
            MockInstrumentRepository.Setup(a => a.GetAll()).Returns(itemList);

            var service = this.SubjectUnderTest();

            // ACT ( Run the actual Test )
            service.SaveInstrument(itemToAdd);

            // Assert ( Test its worked )
            Assert.That(itemList, Is.Not.Empty);
        }

        [Test]
        public void SaveInstrumentToRepositoryWithCorrectPlantAreaId()
        {
            const int NewPlantAreaId = 1;

            var itemList = new List<Instrument>();
            var itemToAdd = new Instrument { PlantAreaId = NewPlantAreaId };

            MockInstrumentRepository.Setup(a => a.Add(It.IsAny<Instrument>())).Callback((Instrument instrument) => itemList.Add(instrument));
            MockInstrumentRepository.Setup(a => a.GetAll()).Returns(itemList);

            var service = this.SubjectUnderTest();

            // ACT ( Run the actual Test )
            service.SaveInstrument(itemToAdd);

            // Assert ( Test its worked )

            // ReSharper disable PossibleMultipleEnumeration
            Assert.That(itemList.Count(), Is.EqualTo(1));
            Assert.That(itemList.Count(pa => pa.PlantAreaId == NewPlantAreaId), Is.EqualTo(1));
            // ReSharper restore PossibleMultipleEnumeration
        }

        [Test]
        public void SaveInstrumentUpdatesExisting()
        {
            const int OwningPlantAreaId = 1;

            var itemList = new List<Instrument>
                                {
                                    new Instrument { Id = 1, PlantAreaId = OwningPlantAreaId, Name = "Instrument Name" },
                                    new Instrument { Id = 2, PlantAreaId = OwningPlantAreaId, Name = "Instrument Name" }
                                };

            MockInstrumentRepository.Setup(a => a.Update(It.IsAny<Instrument>())).Callback(
                (Instrument instrument) =>
                {
                    itemList.Single(i => i.Id == instrument.Id).Name = instrument.Name;
                });

            var service = this.SubjectUnderTest();

            var itemToUpdate = new Instrument { Id = 1, PlantAreaId = OwningPlantAreaId, Name = "Instrument New Name" };

            // ACT ( Run the actual Test )
            service.SaveInstrument(itemToUpdate);

            Assert.That(itemList.Single(i => i.Id == 1).Name, Is.EqualTo(itemToUpdate.Name));
            Assert.That(itemList.Single(i => i.Id == 2).Name, Is.Not.EqualTo(itemToUpdate.Name));
        }
    }

    [TestFixture]
    public class GetInstrumentMethod : InstrumentServiceTestsBase
    {
        [Test]
        public void ReturnsFoundInstrument()
        {
            // ARRANGE
            var itemList = new List<Instrument>
                                {
                                    new Instrument { Id = 1 },
                                    new Instrument { Id = 2 },
                                };

            Instrument foundItem = null;

            MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
                .Callback(
                    (Expression<Func<Instrument, bool>> pred) => foundItem = itemList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundItem);

            const int ItemIdToUse = 1;
            var service = this.SubjectUnderTest();

            // ACT 
            var result = service.GetInstrument(ItemIdToUse);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));

            MockInstrumentRepository.Verify();
        }

        [Test]
        public void ReturnsNullWhenNoInstrumentFound()
        {
            // ARRANGE
            var itemList = new List<Instrument>
                                {
                                    new Instrument { Id = 1 },
                                    new Instrument { Id = 2 },
                                };

            Instrument foundItem = null;

            MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
                .Callback(
                    (Expression<Func<Instrument, bool>> pred) => foundItem = itemList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundItem);

            const int ItemIdToUse = 3;

            var service = this.SubjectUnderTest();

            // ACT 
            var result = service.GetInstrument(ItemIdToUse);

            // ASSERT
            Assert.That(result, Is.Null);

            MockInstrumentRepository.Verify();
        }
    }

    [TestFixture]
    public class InstrumentExists : InstrumentServiceTestsBase
    {
        [Test]
        public void ReturnsTrueIfAddingInstrumentFoundByName()
        {
            // ARRANGE
            var itemList = new List<Instrument>
                                    {
                                        new Instrument
                                            {
                                                Id = 1,
                                                PlantAreaId = 1,
                                                Name = "Test Instrument 1"
                                            },
                                        new Instrument
                                            {
                                                Id = 2,
                                                PlantAreaId = 2,
                                                Name = "Test Instrument 2"
                                            },
                                    };

            Instrument foundItem = null;

            MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
                .Callback(
                    (Expression<Func<Instrument, bool>> pred) =>
                    foundItem = itemList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundItem);

            const int PlantAreaIdToUse = 1;
            const string ItemNameToFind = "Test Instrument 1";
            const int NewItemId = new int();

            var service = this.SubjectUnderTest();

            // ACT 
            var result = service.InstrumentExists(ItemNameToFind, PlantAreaIdToUse, NewItemId);

            // ASSERT
            Assert.That(result, Is.True);

            MockInstrumentRepository.Verify();
        }

        [Test]
        public void ReturnsFalseIfAddingInstrumentNotFoundByName()
        {
            // ARRANGE
            var itemList = new List<Instrument>
                                    {
                                        new Instrument
                                            {
                                                Id = 1,
                                                PlantAreaId = 1,
                                                Name = "Test Instrument 1"
                                            },
                                        new Instrument
                                            {
                                                Id = 2,
                                                PlantAreaId = 2,
                                                Name = "Test Instrument 2"
                                            },
                                    };

            Instrument foundItem = null;

            MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
                .Callback(
                    (Expression<Func<Instrument, bool>> pred) =>
                    foundItem = itemList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundItem);

            const int PlantAreaIdToUse = 2;
            const string ItemNameToFind = "Test Instrument 1";
            const int NewItemId = new int();

            var service = this.SubjectUnderTest();

            // ACT 
            var result = service.InstrumentExists(ItemNameToFind, NewItemId, PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.False);

            MockInstrumentRepository.Verify();
        }

        [Test]
        public void ReturnsTrueIfUpdatingInstrumentFoundByName()
        {
            var itemList = new List<Instrument>
                                    {
                                        new Instrument
                                            {
                                                Id = 1,
                                                PlantAreaId = 1,
                                                Name = "Test Instrument 1"
                                            },
                                        new Instrument
                                            {
                                                Id = 2,
                                                PlantAreaId = 1,
                                                Name = "Test Instrument 2"
                                            },
                                        new Instrument
                                            {
                                                Id = 3,
                                                PlantAreaId = 2,
                                                Name = "Test Instrument 1"
                                            },
                                        new Instrument
                                            {
                                                Id = 4,
                                                PlantAreaId = 2,
                                                Name = "Test Instrument 2"
                                            }
                                    };

            Instrument foundItem = null;

            MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
                .Callback(
                    (Expression<Func<Instrument, bool>> pred) =>
                    foundItem = itemList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundItem);

            const int PlantAreaIdToUse = 2;
            const int ItemIdToUse = 2;
            const string ItemNameToFind = "Test Instrument 1";

            var service = this.SubjectUnderTest();

            // ACT 
            var result = service.InstrumentExists(ItemNameToFind, ItemIdToUse, PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.True);

            MockInstrumentRepository.Verify();
        }

        [Test]
        public void ReturnsFalseIfUpdatingInstrumentNotFoundByName()
        {
            var itemList = new List<Instrument>
                                    {
                                        new Instrument
                                            {
                                                Id = 1,
                                                PlantAreaId = 1,
                                                Name = "Test Instrument 1"
                                            },
                                        new Instrument
                                            {
                                                Id = 2,
                                                PlantAreaId = 1,
                                                Name = "Test Instrument 2"
                                            },
                                        new Instrument
                                            {
                                                Id = 3,
                                                PlantAreaId = 2,
                                                Name = "Test Instrument 1"
                                            },
                                        new Instrument
                                            {
                                                Id = 4,
                                                PlantAreaId = 2,
                                                Name = "Test Instrument 2"
                                            }
                                    };

            Instrument foundItem = null;

            MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
                .Callback(
                    (Expression<Func<Instrument, bool>> pred) =>
                    foundItem = itemList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundItem);

            const int ItemIdToUse = 2;
            const int PlantAreaIdToUse = 2;
            const string ItemNameToFind = "Test Instrument 3";

            var service = this.SubjectUnderTest();

            // ACT 
            var result = service.InstrumentExists(ItemNameToFind, ItemIdToUse, PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.False);
        }

        // TODO: The below set of Tests are commented out until decide if we are keeping the Unique Instrument by Serial Number attribute

        ////[Test]
        ////public void ReturnsTrueIfAddingInstrumentFoundBySerialNumberAndMake()
        ////{
        ////    // ARRANGE
        ////    var itemList = new List<Instrument>
        ////                            {
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 1,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000001"
        ////                                    },
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 2,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000002"
        ////                                    },
        ////                            };

        ////    Instrument foundInstrument = null;

        ////    var MockInstrumentRepository = new Mock<IRepository<Instrument>>();
        ////    var mockTypeRepository = new Mock<IRepository<InstrumentType>>();
        ////    var MockFrequencyRepository = new Mock<IRepository<CalibrationFrequency>>();
        ////    MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
        ////        .Callback(
        ////            (Expression<Func<Instrument, bool>> pred) =>
        ////            foundInstrument = itemList.AsQueryable().SingleOrDefault(pred))
        ////        .Returns(() => foundInstrument);

        ////    const string MakeToUse = "TestMake";
        ////    const string SerialNumberToUse = "TM000001";
        ////    const int NewItemId = new int();

        ////    var service = new InstrumentService(MockInstrumentRepository.Object, mockTypeRepository.Object, MockFrequencyRepository.Object);

        ////    // ACT 
        ////    var result = service.InstrumentExists(MakeToUse, SerialNumberToUse, NewItemId);

        ////    // ASSERT
        ////    Assert.That(result, Is.True);

        ////    MockInstrumentRepository.Verify();
        ////}

        ////[Test]
        ////public void ReturnsFalseIfAddingInstrumentNotFoundByMakeAndSerialNumber()
        ////{
        ////    // ARRANGE
        ////    var itemList = new List<Instrument>
        ////                            {
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 1,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000001"
        ////                                    },
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 2,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000002"
        ////                                    },
        ////                            };

        ////    Instrument foundItem = null;

        ////    var MockInstrumentRepository = new Mock<IRepository<Instrument>>();
        ////    var mockTypeRepository = new Mock<IRepository<InstrumentType>>();
        ////    var MockFrequencyRepository = new Mock<IRepository<CalibrationFrequency>>();
        ////    MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
        ////        .Callback(
        ////            (Expression<Func<Instrument, bool>> pred) =>
        ////            foundItem = itemList.AsQueryable().SingleOrDefault(pred))
        ////        .Returns(() => foundItem);

        ////    const string MakeToUse = "TestMake";
        ////    const string SerialNumberToUse = "TM000003";
        ////    const int NewItemId = new int();

        ////    var service = new InstrumentService(MockInstrumentRepository.Object, mockTypeRepository.Object, MockFrequencyRepository.Object);

        ////    // ACT 
        ////    var result = service.InstrumentExists(MakeToUse, SerialNumberToUse, NewItemId);

        ////    // ASSERT
        ////    Assert.That(result, Is.False);

        ////    MockInstrumentRepository.Verify();
        ////}

        ////[Test]
        ////public void ReturnsFalseIfAddingInstrumentNotFoundBySerialNumberWhereMakeDiffers()
        ////{
        ////    // ARRANGE
        ////    var itemList = new List<Instrument>
        ////                            {
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 1,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000001"
        ////                                    },
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 2,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000002"
        ////                                    },
        ////                            };

        ////    Instrument foundItem = null;

        ////    var MockInstrumentRepository = new Mock<IRepository<Instrument>>();
        ////    var mockTypeRepository = new Mock<IRepository<InstrumentType>>();
        ////    var MockFrequencyRepository = new Mock<IRepository<CalibrationFrequency>>();
        ////    MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
        ////        .Callback(
        ////            (Expression<Func<Instrument, bool>> pred) =>
        ////            foundItem = itemList.AsQueryable().SingleOrDefault(pred))
        ////        .Returns(() => foundItem);

        ////    const string MakeToUse = "TestMake2";
        ////    const string SerialNumberToUse = "TM000001";
        ////    const int NewItemId = new int();

        ////    var service = new InstrumentService(MockInstrumentRepository.Object, mockTypeRepository.Object, MockFrequencyRepository.Object);

        ////    // ACT 
        ////    var result = service.InstrumentExists(MakeToUse, SerialNumberToUse, NewItemId);

        ////    // ASSERT
        ////    Assert.That(result, Is.False);

        ////    MockInstrumentRepository.Verify();
        ////}

        ////[Test]
        ////public void ReturnsTrueIfUpdatingInstrumentFoundByMakeAndSerialNumber()
        ////{
        ////    var itemList = new List<Instrument>
        ////                            {
        ////                                new Instrument 
        ////                                    {
        ////                                        Id = 1,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000001"
        ////                                    },
        ////                                new Instrument 
        ////                                    {
        ////                                        Id = 2,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000002"
        ////                                    },
        ////                            };

        ////    Instrument foundItem = null;

        ////    var MockInstrumentRepository = new Mock<IRepository<Instrument>>();
        ////    var mockTypeRepository = new Mock<IRepository<InstrumentType>>();
        ////    var MockFrequencyRepository = new Mock<IRepository<CalibrationFrequency>>();
        ////    MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
        ////        .Callback(
        ////            (Expression<Func<Instrument, bool>> pred) =>
        ////            foundItem = itemList.AsQueryable().SingleOrDefault(pred))
        ////        .Returns(() => foundItem);

        ////    const string MakeToUse = "TestMake";
        ////    const string SerialNumberToUse = "TM000001";
        ////    const int UpdateItemId = 2;

        ////    var service = new InstrumentService(MockInstrumentRepository.Object, mockTypeRepository.Object, MockFrequencyRepository.Object);

        ////    // ACT 
        ////    var result = service.InstrumentExists(MakeToUse, SerialNumberToUse, UpdateItemId);

        ////    // ASSERT
        ////    Assert.That(result, Is.True);

        ////    MockInstrumentRepository.Verify();
        ////}

        ////[Test]
        ////public void ReturnsFalseIfUpdatingInstrumentNotFoundByMakeAndSerialNumber()
        ////{
        ////    var itemList = new List<Instrument>
        ////                            {
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 1,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000001"
        ////                                    },
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 2,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000002"
        ////                                    },
        ////                            };

        ////    Instrument foundItem = null;

        ////    var MockInstrumentRepository = new Mock<IRepository<Instrument>>();
        ////    var mockTypeRepository = new Mock<IRepository<InstrumentType>>();
        ////    var MockFrequencyRepository = new Mock<IRepository<CalibrationFrequency>>();
        ////    MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
        ////        .Callback(
        ////            (Expression<Func<Instrument, bool>> pred) =>
        ////            foundItem = itemList.AsQueryable().SingleOrDefault(pred))
        ////        .Returns(() => foundItem);

        ////    const string MakeToUse = "TestMake";
        ////    const string SerialNumberToUse = "TM000003";
        ////    const int UpdateItemId = 1;

        ////    var service = new InstrumentService(MockInstrumentRepository.Object, mockTypeRepository.Object, MockFrequencyRepository.Object);

        ////    // ACT 
        ////    var result = service.InstrumentExists(MakeToUse, SerialNumberToUse, UpdateItemId);

        ////    // ASSERT
        ////    Assert.That(result, Is.False);
        ////}

        ////[Test]
        ////public void ReturnsFalseIfUpdatingInstrumentNotFoundBySerialNumberWhereMakeDiffers()
        ////{
        ////    // ARRANGE
        ////    var itemList = new List<Instrument>
        ////                            {
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 1,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000001"
        ////                                    },
        ////                                new Instrument
        ////                                    {
        ////                                        Id = 2,
        ////                                        Make = "TestMake",
        ////                                        SerialNumber = "TM000002"
        ////                                    },
        ////                            };

        ////    Instrument foundItem = null;

        ////    var MockInstrumentRepository = new Mock<IRepository<Instrument>>();
        ////    var mockTypeRepository = new Mock<IRepository<InstrumentType>>();
        ////    var MockFrequencyRepository = new Mock<IRepository<CalibrationFrequency>>();
        ////    MockInstrumentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Instrument, bool>>>()))
        ////        .Callback(
        ////            (Expression<Func<Instrument, bool>> pred) =>
        ////            foundItem = itemList.AsQueryable().SingleOrDefault(pred))
        ////        .Returns(() => foundItem);

        ////    const string MakeToUse = "TestMake2";
        ////    const string SerialNumberToUse = "TM000002";
        ////    const int UpdateItemId = 1;

        ////    var service = new InstrumentService(MockInstrumentRepository.Object, mockTypeRepository.Object, MockFrequencyRepository.Object);

        ////    // ACT 
        ////    var result = service.InstrumentExists(MakeToUse, SerialNumberToUse, UpdateItemId);

        ////    // ASSERT
        ////    Assert.That(result, Is.False);

        ////    MockInstrumentRepository.Verify();
        ////}
    }

    [TestFixture]
    public class GetEquipmentAttachedTo : InstrumentServiceTestsBase
    {
        [Test]
        public void ReturnsFoundEquipments()
        {
            // ARRANGE
            var channelList = new List<Channel>
                                    {
                                        new Channel
                                            {
                                                InstrumentId = 1,
                                                ConnectedToEquipment = new Equipment
                                                                            {
                                                                                Id = 1
                                                                            },
                                            },
                                        new Channel
                                            {
                                                InstrumentId = 1,
                                                ConnectedToEquipment = new Equipment
                                                                            {
                                                                                Id = 2
                                                                            },
                                            },
                                        new Channel
                                            {
                                                InstrumentId = 2,
                                                ConnectedToEquipment = new Equipment
                                                                            {
                                                                                Id = 3
                                                                            },
                                            }
                                    };

            var foundItems = Enumerable.Empty<Channel>();

            MockChannelRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                        {
                            foundItems = channelList.AsQueryable().Where(pred);;
                        })
                .Returns(() => foundItems.ToList());

            const int InstrumentIdToUse = 1;

            var instrumentService = this.SubjectUnderTest();

            // ACT 
            var result = instrumentService.GetEquipmentAttachedTo(InstrumentIdToUse);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.FirstOrDefault().Id, Is.EqualTo(1));
        }

        [Test]
        public void ReturnsEmptyWhenNoEquipmentsFound()
        {
            // ARRANGE
            var channelList = new List<Channel>
                                    {
                                        new Channel
                                            {
                                                InstrumentId = 1,
                                                ConnectedToEquipment = new Equipment
                                                                            {
                                                                                Id = 1
                                                                            },
                                            },
                                        new Channel
                                            {
                                                InstrumentId = 1,
                                                ConnectedToEquipment = new Equipment
                                                                            {
                                                                                Id = 2
                                                                            },
                                            },
                                        new Channel
                                            {
                                                InstrumentId = 2,
                                                ConnectedToEquipment = new Equipment
                                                                            {
                                                                                Id = 3
                                                                            },
                                            }
                                    };

            var foundItems = Enumerable.Empty<Channel>();

            MockChannelRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                        {
                            foundItems = channelList.AsQueryable().Where(pred);
                        })
                .Returns(() => foundItems.ToList());

            const int InstrumentIdToUse = 4;

            var instrumentService = this.SubjectUnderTest();

            // ACT 
            var result = instrumentService.GetEquipmentAttachedTo(InstrumentIdToUse);

            // ASSERT
            Assert.That(result, Is.Empty);
        }
    }
}