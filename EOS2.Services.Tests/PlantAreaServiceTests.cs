namespace EOS2.Services.Tests.PlantAreaServiceTests
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

    public abstract class PlantAreaServiceTestsBase
    {
        protected Mock<IRepository<PlantArea>> MockPlantAreaRepository { get; set; }

        [SetUp]
        public void FixtureSetup()
        {
            MockPlantAreaRepository = new Mock<IRepository<PlantArea>>();
        }

        public IPlantAreaService ServiceUnderTest()
        {
            return new PlantAreaService(MockPlantAreaRepository.Object);
        }        
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class SavePlantAreaMethod : PlantAreaServiceTestsBase
    {
        [Test]
        public void AddsPlantAreaToRepository() 
        {
            var plantAreaList = new List<PlantArea>();
            var plantAreaToAdd = new PlantArea();

            MockPlantAreaRepository.Setup(a => a.Add(It.IsAny<PlantArea>())).Callback((PlantArea plantArea) => plantAreaList.Add(plantArea));

            MockPlantAreaRepository.Setup(a => a.GetAll()).Returns(plantAreaList);

            var plantAreaService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            plantAreaService.SavePlantArea(plantAreaToAdd);

            // Assert ( Test its worked )
            Assert.That(plantAreaList, Is.Not.Empty);
        }

        [Test]
        public void SavePlantAreaToRepositoryWithCorrectSiteId()
        {
            const int NewSiteId = 1;

            var plantAreaList = new List<PlantArea>();
            var plantAreaToAdd = new PlantArea() { SiteId = NewSiteId };

            MockPlantAreaRepository.Setup(a => a.Add(It.IsAny<PlantArea>())).Callback((PlantArea plantArea) => plantAreaList.Add(plantArea));

            MockPlantAreaRepository.Setup(a => a.GetAll()).Returns(plantAreaList);

            var plantAreaService = this.ServiceUnderTest();

            // ACT ( Run the actual Test )
            plantAreaService.SavePlantArea(plantAreaToAdd);

            Assert.That(plantAreaList.Count(), Is.EqualTo(1));
            Assert.That(plantAreaList.Count(pa => pa.SiteId == NewSiteId), Is.EqualTo(1));
        }

        [Test]
        public void SavePlantAreaUpdatesExisting()
        {
            const int OwningSiteId = 1;

            var plantAreaList = new List<PlantArea>()
                                {
                                    new PlantArea() { Id = 1, SiteId = OwningSiteId, Name = "Plant Area Name", Description = "Test Description" },
                                    new PlantArea() { Id = 2, SiteId = OwningSiteId, Name = "Plant Area Name", Description = "Test Description" }
                                };

            MockPlantAreaRepository.Setup(a => a.Update(It.IsAny<PlantArea>())).Callback(
                (PlantArea plantArea) =>
                {
                    plantAreaList.Single(pa => pa.Id == plantArea.Id).Name = plantArea.Name;
                });

            var plantAreaService = this.ServiceUnderTest();

            var plantAreaToUpdate = new PlantArea() { Id = 1, SiteId = OwningSiteId, Name = "Plant Area New Name", Description = "Test Description" };

            // ACT ( Run the actual Test )
            plantAreaService.SavePlantArea(plantAreaToUpdate);

            Assert.That(plantAreaList.Single(pa => pa.Id == 1).Name, Is.EqualTo(plantAreaToUpdate.Name));
            Assert.That(plantAreaList.Single(pa => pa.Id == 2).Name, Is.Not.EqualTo(plantAreaToUpdate.Name));
        }
    }

    [TestFixture]
    public class GetPlantAreaMethod : PlantAreaServiceTestsBase
    {
        [Test]
        public void ReturnsFoundPlantArea()
        {
            // ARRANGE
            var plantAreaList = new List<PlantArea>()
                                {
                                    new PlantArea() { Id = 1 },
                                    new PlantArea() { Id = 2 },
                                };

            PlantArea foundPlantArea = null;

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = plantAreaList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundPlantArea);

            const int PlantAreaIdToUse = 1;
            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.GetPlantArea(PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));

            MockPlantAreaRepository.Verify();
        }

        [Test]
        public void ReturnsNullWhenNoSiteFound()
        {
            // ARRANGE
            var plantAreaList = new List<PlantArea>()
                                {
                                    new PlantArea() { Id = 1 },
                                    new PlantArea() { Id = 2 },
                                };

            PlantArea foundPlantArea = null;

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = plantAreaList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundPlantArea);

            const int PlantAreaIdToUse = 3;
            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.GetPlantArea(PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.Null);

            MockPlantAreaRepository.Verify();
        }
    }

    [TestFixture]
    public class GetPlantAreasBySiteMethod : PlantAreaServiceTestsBase
    {
        [Test]
        public void ReturnsFoundPlantAreas()
        {
            // ARRANGE
            var itemList = new List<PlantArea>
                                {
                                    new PlantArea { Id = 1, SiteId = 1 },
                                    new PlantArea { Id = 2, SiteId = 1 },
                                    new PlantArea { Id = 3, SiteId = 2 },
                                };

            var foundItems = new List<PlantArea>();

            MockPlantAreaRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback((Expression<Func<PlantArea, bool>> pred) => foundItems = itemList.AsQueryable().Where(pred).ToList()).Returns(() => foundItems);

            const int SiteIdToUse = 1;
            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.GetPlantAreasForSite(SiteIdToUse);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ReturnsEmptyWhenNoPlantAreasFound()
        {
            // ARRANGE
            var itemList = new List<PlantArea>
                                {
                                    new PlantArea { Id = 1, SiteId = 1 },
                                    new PlantArea { Id = 2, SiteId = 1 },
                                    new PlantArea { Id = 3, SiteId = 2 },
                                };

            var foundItems = new List<PlantArea>();

            MockPlantAreaRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback((Expression<Func<PlantArea, bool>> pred) => foundItems = itemList.AsQueryable().Where(pred).ToList()).Returns(() => foundItems);

            const int SiteIdToUse = 3;
            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.GetPlantAreasForSite(SiteIdToUse);

            // ASSERT
            Assert.That(result, Is.Empty);
        }
    }

    [TestFixture]
    public class PlantAreaExists : PlantAreaServiceTestsBase
    {
        [Test]
        public void ReturnsTrueIfAddingPlantAreaFound()
        {
            // ARRANGE
            var plantAreaList = new List<PlantArea>()
                                {
                                    new PlantArea() { Id = 1, SiteId = 1, Name = "Test Plant Area 1" },
                                    new PlantArea() { Id = 2, SiteId = 2, Name = "Test Plant Area 2" },
                                };

            PlantArea foundPlantArea = null;

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = plantAreaList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundPlantArea);

            const int SiteIdToUse = 1;
            const string PlantAreaNameToFind = "Test Plant Area 1";
            const int NewPlantAreaId = new int();

            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.PlantAreaExists(PlantAreaNameToFind, NewPlantAreaId, SiteIdToUse);

            // ASSERT
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReturnsFalseIfAddingPlantAreaNotFound()
        {
            // ARRANGE
            var plantAreaList = new List<PlantArea>()
                                {
                                    new PlantArea() { Id = 1, SiteId = 1, Name = "Test Plant Area 1" },
                                    new PlantArea() { Id = 2, SiteId = 2, Name = "Test Plant Area 2" },
                                };

            PlantArea foundPlantArea = null;

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = plantAreaList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundPlantArea);

            const int SiteIdToUse = 2;
            const string PlantAreaNameToFind = "Test Plant Area 1";
            const int NewPlantAreaId = new int();

            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.PlantAreaExists(PlantAreaNameToFind, NewPlantAreaId, SiteIdToUse);

            // ASSERT
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnsTrueIfUpdatingPlantAreaFound()
        {
            var plantAreaList = new List<PlantArea>()
                                {
                                    new PlantArea() { Id = 1, SiteId = 1, Name = "Test Plant Area 1" },
                                    new PlantArea() { Id = 2, SiteId = 1, Name = "Test Plant Area 2" },
                                    new PlantArea() { Id = 3, SiteId = 2, Name = "Test Plant Area 1" },
                                    new PlantArea() { Id = 4, SiteId = 2, Name = "Test Plant Area 2" }
                                };

            PlantArea foundPlantArea = null;

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = plantAreaList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundPlantArea);

            const int SiteIdToUse = 2;
            const int PlantAreaIdToUse = 2;
            const string PlantAreaNameToFind = "Test Plant Area 1";

            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.PlantAreaExists(PlantAreaNameToFind, PlantAreaIdToUse, SiteIdToUse);

            // ASSERT
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReturnsFalseIfUpdatingPlantAreaNotFound()
        {
            var plantAreaList = new List<PlantArea>()
                                {
                                    new PlantArea() { Id = 1, SiteId = 1, Name = "Test Plant Area 1" },
                                    new PlantArea() { Id = 2, SiteId = 1, Name = "Test Plant Area 2" },
                                    new PlantArea() { Id = 3, SiteId = 2, Name = "Test Plant Area 1" },
                                    new PlantArea() { Id = 4, SiteId = 2, Name = "Test Plant Area 2" }
                                };

            PlantArea foundPlantArea = null;

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = plantAreaList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundPlantArea);

            const int PlantAreaIdToUse = 2;
            const int SiteIdToUse = 2;
            const string PlantAreaNameToFind = "Test Plant Area 3";

            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.PlantAreaExists(PlantAreaNameToFind, PlantAreaIdToUse, SiteIdToUse);

            // ASSERT
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class GetEquipmentsByPlantAreaMethod : PlantAreaServiceTestsBase
    {
        [Test]
        public void ReturnsFoundEquipments()
        {
            // ARRANGE
            var itemList = new List<PlantArea>
                                {
                                    new PlantArea 
                                    { 
                                        Id = 1,
                                        Equipments = new List<Equipment>
                                            {
                                                new Equipment { Id = 1 },
                                                new Equipment { Id = 2 }
                                            } 
                                    },
                                    new PlantArea 
                                    { 
                                        Id = 2,
                                        Equipments = new List<Equipment>
                                            {
                                                new Equipment { Id = 3 },
                                                new Equipment { Id = 4 }
                                            } 
                                    },
                                    new PlantArea 
                                    { 
                                        Id = 3,
                                        Equipments = new List<Equipment>
                                            {
                                                new Equipment { Id = 5 },
                                                new Equipment { Id = 6 }
                                            } 
                                    }
                                };

            var foundPlantArea = new PlantArea();

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = itemList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundPlantArea);

            const int PlantAreaIdToUse = 1;

            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.GetEquipmentFor(PlantAreaIdToUse);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ReturnsEmptyListWhenNoEquipmentsFound()
        {
            // ARRANGE
            var itemList = new List<PlantArea>
                                {
                                    new PlantArea 
                                    { 
                                        Id = 1,
                                        Equipments = new List<Equipment>
                                            {
                                                new Equipment { Id = 1 },
                                                new Equipment { Id = 2 }
                                            } 
                                    },
                                    new PlantArea 
                                    { 
                                        Id = 2,
                                        Equipments = new List<Equipment>
                                            {
                                                new Equipment { Id = 3 },
                                                new Equipment { Id = 4 }
                                            } 
                                    },
                                    new PlantArea 
                                    { 
                                        Id = 3,
                                        Equipments = new List<Equipment>
                                            {
                                                new Equipment { Id = 5 },
                                                new Equipment { Id = 6 }
                                            } 
                                    }
                                };

            var foundPlantArea = new PlantArea();

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = itemList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundPlantArea);

            const int PlantAreaIdToUse = 4;

            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.GetEquipmentFor(PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.Empty);
        }
    }

    [TestFixture]
    public class GetInstrumentsByPlantAreaMethod : PlantAreaServiceTestsBase
    {
        [Test]
        public void ReturnsFoundInstruments()
        {
            // ARRANGE
            var itemList = new List<PlantArea>
                                {
                                    new PlantArea 
                                    { 
                                        Id = 1,
                                        Instruments = new List<Instrument>
                                            {
                                                new Instrument { Id = 1 },
                                                new Instrument { Id = 2 }
                                            } 
                                    },
                                    new PlantArea 
                                    { 
                                        Id = 2,
                                        Instruments = new List<Instrument>
                                            {
                                                new Instrument { Id = 3 },
                                                new Instrument { Id = 4 }
                                            } 
                                    },
                                    new PlantArea 
                                    { 
                                        Id = 3,
                                        Instruments = new List<Instrument>
                                            {
                                                new Instrument { Id = 5 },
                                                new Instrument { Id = 6 }
                                            } 
                                    }
                                };

            var foundPlantArea = new PlantArea();

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = itemList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundPlantArea);

            const int PlantAreaIdToUse = 2;

            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.GetInstrumentsFor(PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ReturnsEmptyCollectionWhenNoInstrumentFounds()
        {
            // ARRANGE
            var itemList = new List<PlantArea>
                                {
                                    new PlantArea 
                                    { 
                                        Id = 1,
                                        Instruments = new List<Instrument>
                                            {
                                                new Instrument { Id = 1 },
                                                new Instrument { Id = 2 }
                                            } 
                                    },
                                    new PlantArea 
                                    { 
                                        Id = 2,
                                        Instruments = new List<Instrument>
                                            {
                                                new Instrument { Id = 3 },
                                                new Instrument { Id = 4 }
                                            } 
                                    },
                                    new PlantArea 
                                    { 
                                        Id = 3,
                                        Instruments = new List<Instrument>
                                            {
                                                new Instrument { Id = 5 },
                                                new Instrument { Id = 6 }
                                            } 
                                    }
                                };

            var foundPlantArea = new PlantArea();

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = itemList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundPlantArea);

            const int PlantAreaIdToUse = 4;

            var plantAreaService = this.ServiceUnderTest();

            // ACT 
            var result = plantAreaService.GetInstrumentsFor(PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.Empty);
        }
    }

    [TestFixture]
    public class GetInstrumentsWithAvailableChannelsInMethod : PlantAreaServiceTestsBase
    {
        [Test]
        public void ReturnsListOfInstrumentsWhoseChannelsAreNotAssignedToAnyEquipment()
        {
            var plantAreas = new List<PlantArea>
                                 {
                                     new PlantArea
                                         {
                                             Id = 1,
                                             Instruments =
                                                 new List<Instrument>
                                                     {
                                                         new Instrument
                                                             {
                                                                Id = 1,
                                                                Channels = new List<Channel> { new Channel { Id = 1 } }
                                                             },
                                                         new Instrument
                                                             {
                                                                Id = 2,
                                                                Channels = new List<Channel> { new Channel { Id = 2, ConnectedToEquipmentId = 1 } }
                                                             },
                                                         new Instrument
                                                             {
                                                                Id = 2,
                                                                Channels = new List<Channel> { new Channel { Id = 2, ConnectedToEquipmentId = 2 } }
                                                             }
                                                     }
                                         },
                                     new PlantArea { Id = 2 }
                                 };

            var foundPlantArea = new PlantArea();

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = plantAreas.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundPlantArea);

            const int PlantAreaIdToUse = 1;

            var results = this.ServiceUnderTest().GetInstrumentsWithAvailableChannelsIn(PlantAreaIdToUse);

            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ReturnsEmptyListWhenAtLeastAllInstrumentChannelIsAreAssignedToEquipment()
        {            
            var plantAreas = new List<PlantArea>
                                 {
                                     new PlantArea
                                         {
                                             Id = 1,
                                             Instruments =
                                                 new List<Instrument>
                                                     {
                                                         new Instrument
                                                             {
                                                                Id = 2,
                                                                Channels = new List<Channel>
                                                                               {
                                                                                   new Channel { Id = 2, ConnectedToEquipmentId = 1 },
                                                                                   new Channel { Id = 3, ConnectedToEquipmentId = 1 }
                                                                               }
                                                             },
                                                         new Instrument
                                                             {
                                                                Id = 2,
                                                                Channels = new List<Channel> 
                                                                                { 
                                                                                    new Channel { Id = 4, ConnectedToEquipmentId = 2 },
                                                                                    new Channel { Id = 5, ConnectedToEquipmentId = 1 }, 
                                                                                    new Channel { Id = 6, ConnectedToEquipmentId = 1 } 
                                                                                }
                                                             }
                                                     }
                                         },
                                     new PlantArea { Id = 2 }
                                 };

            var foundPlantArea = new PlantArea();

            MockPlantAreaRepository.Setup(m => m.Find(It.IsAny<Expression<Func<PlantArea, bool>>>()))
                .Callback(
                    (Expression<Func<PlantArea, bool>> pred) => foundPlantArea = plantAreas.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundPlantArea);

            const int PlantAreaIdToUse = 1;

            var results = this.ServiceUnderTest().GetInstrumentsWithAvailableChannelsIn(PlantAreaIdToUse);

            Assert.That(results, Is.Empty);
        }
    }
}