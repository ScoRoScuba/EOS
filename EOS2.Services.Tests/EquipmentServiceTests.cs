namespace EOS2.Services.Tests.EquipmentServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    using EOS2.Common.Exceptions;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Services.BusinessDomain;

    using Moq;

    using NUnit.Framework;

    public abstract class EquipmentServiceTestsBase
    {
        protected Mock<IRepository<Equipment>> MockEquipmentRepository { get; set; }

        protected Mock<IRepository<EquipmentType>> MockEquipmentTypeRepository { get; set; }

        protected Mock<IRepository<Channel>> MockChannelRepository { get; set; }

        protected Mock<IChannelService> MockChannelService { get; set; }
        
        [SetUp]
        public void FixtureSetup()
        {
            MockEquipmentRepository = new Mock<IRepository<Equipment>>();
            MockEquipmentTypeRepository = new Mock<IRepository<EquipmentType>>();
            MockChannelService = new Mock<IChannelService>();
        }

        public IEquipmentService SubjectUnderTest()
        {
            return new EquipmentService(MockChannelService.Object, MockEquipmentRepository.Object);            
        }        
    }

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ctor", Justification = "Ctor is valid name with Tests")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class CtorExceptions : EquipmentServiceTestsBase
    {
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnreMethodResults", MessageId = "EOS2.Services.BusinessDomain.EquipmentService", Justification = "Unit testing Expected")]
        [Test]
        public void ThrowsArgumentNullExceptionForNoChannelService()
        {
            Assert.Throws<ArgumentNullException>(() => new EquipmentService(null, null));                        
        }

        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "EOS2.Services.BusinessDomain.EquipmentService", Justification = "Unit testing Expected")]
        [Test]
        public void ThrowsArgumentNullExceptionForNoEquipmentRepository()
        {
            Assert.Throws<ArgumentNullException>(() => new EquipmentService(MockChannelService.Object, null));                        
        }
    
        [Test]
        public void DoesNotThrowAnyException()
        {
            Assert.IsNotNull(SubjectUnderTest());                        
        }
    }
    
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class SaveEquipmentMethod : EquipmentServiceTestsBase
    {
        [Test]
        public void SaveEquipmentToRepositoryWithNullModel()
        {
            var equipmentService = this.SubjectUnderTest();

            // ACT ( Run the actual Test ) &  Assert ( Test its worked )
            Assert.Throws<ArgumentNullException>(() => equipmentService.SaveEquipment(null));
        }

        [Test]
        public void AddsEquipmentToRepository()
        {
            var equipmentList = new List<Equipment>();
            var equipmentToAdd = new Equipment();

            MockEquipmentRepository.Setup(a => a.Add(It.IsAny<Equipment>())).Callback((Equipment equipment) => equipmentList.Add(equipment));
            MockEquipmentRepository.Setup(a => a.GetAll()).Returns(equipmentList);

            var equipmentService = this.SubjectUnderTest();

            // ACT ( Run the actual Test )
            equipmentService.SaveEquipment(equipmentToAdd);

            // Assert ( Test its worked )
            Assert.That(equipmentList, Is.Not.Empty);
        }

        [Test]
        public void SaveEquipmentToRepositoryWithCorrectPlantAreaId()
        {
            const int NewPlantAreaId = 1;

            var equipmentList = new List<Equipment>();
            var equipmentToAdd = new Equipment { PlantAreaId = NewPlantAreaId };

            MockEquipmentRepository.Setup(a => a.Add(It.IsAny<Equipment>()))
                .Callback((Equipment equipment) => equipmentList.Add(equipment));
            MockEquipmentRepository.Setup(a => a.GetAll()).Returns(equipmentList);

            var equipmentService = this.SubjectUnderTest();

            // ACT ( Run the actual Test )
            equipmentService.SaveEquipment(equipmentToAdd);

            // Assert ( Test its worked )
            // ReSharper disable PossibleMultipleEnumeration
            Assert.That(equipmentList.Count(), Is.EqualTo(1));
            Assert.That(equipmentList.Count(pa => pa.PlantAreaId == NewPlantAreaId), Is.EqualTo(1));
            // ReSharper restore PossibleMultipleEnumeration
        }

        [Test]
        public void SaveEquipmentUpdatesExisting()
        {
            const int OwningPlantAreaId = 1;

            var equipmentList = new List<Equipment>
                                    {
                                        new Equipment
                                            {
                                                Id = 1,
                                                PlantAreaId = OwningPlantAreaId,
                                                Name = "Equipment Name"
                                            },
                                        new Equipment
                                            {
                                                Id = 2,
                                                PlantAreaId = OwningPlantAreaId,
                                                Name = "Equipment Name"
                                            }
                                    };

            MockEquipmentRepository.Setup(a => a.Update(It.IsAny<Equipment>())).Callback(
                (Equipment equipment) =>
                    {
                        equipmentList.Single(e => e.Id == equipment.Id).Name = equipment.Name;
                    });

            var equipmentService = this.SubjectUnderTest();

            var equipmentToUpdate = new Equipment
                                        {
                                            Id = 1,
                                            PlantAreaId = OwningPlantAreaId,
                                            Name = "Equipment New Name"
                                        };

            // ACT ( Run the actual Test )
            equipmentService.SaveEquipment(equipmentToUpdate);

            Assert.That(equipmentList.Single(e => e.Id == 1).Name, Is.EqualTo(equipmentToUpdate.Name));
            Assert.That(equipmentList.Single(e => e.Id == 2).Name, Is.Not.EqualTo(equipmentToUpdate.Name));
        }
    }

    [TestFixture]
    public class GetEquipmentMethod : EquipmentServiceTestsBase
    {
        [Test]
        public void ReturnsFoundEquipment()
        {
            // ARRANGE
            var equipmentList = new List<Equipment> { new Equipment { Id = 1 }, new Equipment { Id = 2 }, };

            Equipment foundEquipment = null;

            MockEquipmentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Equipment, bool>>>()))
                .Callback(
                    (Expression<Func<Equipment, bool>> pred) =>
                    foundEquipment = equipmentList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundEquipment);

            const int EquipmentIdToUse = 1;

            var equipmentService = this.SubjectUnderTest();

            // ACT 
            var result = equipmentService.GetEquipment(EquipmentIdToUse);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));

            MockEquipmentRepository.Verify();
        }

        [Test]
        public void ReturnsNullWhenNoSiteFound()
        {
            // ARRANGE
            var equipmentList = new List<Equipment> { new Equipment { Id = 1 }, new Equipment { Id = 2 }, };

            Equipment foundEquipment = null;

            MockEquipmentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Equipment, bool>>>()))
                .Callback(
                    (Expression<Func<Equipment, bool>> pred) =>
                    foundEquipment = equipmentList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundEquipment);

            const int EquipmentIdToUse = 3;

            var equipmentService = this.SubjectUnderTest();

            // ACT 
            var result = equipmentService.GetEquipment(EquipmentIdToUse);

            // ASSERT
            Assert.That(result, Is.Null);

            MockEquipmentRepository.Verify();
        }
    }

    [TestFixture]
    public class EquipmentExists : EquipmentServiceTestsBase
    {
        [Test]
        public void ReturnsTrueIfAddingEquipmentFoundByName()
        {
            // ARRANGE
            var equipmentList = new List<Equipment>
                                    {
                                        new Equipment
                                            {
                                                Id = 1,
                                                PlantAreaId = 1,
                                                Name = "Test Equipment 1"
                                            },
                                        new Equipment
                                            {
                                                Id = 2,
                                                PlantAreaId = 2,
                                                Name = "Test Equipment 2"
                                            },
                                    };

            Equipment foundEquipment = null;

            MockEquipmentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Equipment, bool>>>()))
                .Callback(
                    (Expression<Func<Equipment, bool>> pred) =>
                    foundEquipment = equipmentList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundEquipment);

            const int PlantAreaIdToUse = 1;
            const string EquipmentNameToFind = "Test Equipment 1";
            const int NewEquipmentId = new int();

            var equipmentService = this.SubjectUnderTest();

            // ACT 
            var result = equipmentService.EquipmentExists(EquipmentNameToFind, PlantAreaIdToUse, NewEquipmentId);

            // ASSERT
            Assert.That(result, Is.True);

            MockEquipmentRepository.Verify();
        }

        [Test]
        public void ReturnsFalseIfAddingEquipmentNotFoundByName()
        {
            // ARRANGE
            var equipmentList = new List<Equipment>
                                    {
                                        new Equipment
                                            {
                                                Id = 1,
                                                PlantAreaId = 1,
                                                Name = "Test Equipment 1"
                                            },
                                        new Equipment
                                            {
                                                Id = 2,
                                                PlantAreaId = 2,
                                                Name = "Test Equipment 2"
                                            },
                                    };

            Equipment foundEquipment = null;

            MockEquipmentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Equipment, bool>>>()))
                .Callback(
                    (Expression<Func<Equipment, bool>> pred) =>
                    foundEquipment = equipmentList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundEquipment);

            const int PlantAreaIdToUse = 2;
            const string EquipmentNameToFind = "Test Equipment 1";
            const int NewEquipmentId = new int();

            var equipmentService = this.SubjectUnderTest();

            // ACT 
            var result = equipmentService.EquipmentExists(EquipmentNameToFind, NewEquipmentId, PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.False);

            MockEquipmentRepository.Verify();
        }

        [Test]
        public void ReturnsTrueIfUpdatingEquipmentFoundByName()
        {
            var equipmentList = new List<Equipment>
                                    {
                                        new Equipment
                                            {
                                                Id = 1,
                                                PlantAreaId = 1,
                                                Name = "Test Equipment 1"
                                            },
                                        new Equipment
                                            {
                                                Id = 2,
                                                PlantAreaId = 1,
                                                Name = "Test Equipment 2"
                                            },
                                        new Equipment
                                            {
                                                Id = 3,
                                                PlantAreaId = 2,
                                                Name = "Test Equipment 1"
                                            },
                                        new Equipment
                                            {
                                                Id = 4,
                                                PlantAreaId = 2,
                                                Name = "Test Equipment 2"
                                            }
                                    };

            Equipment foundEquipment = null;

            MockEquipmentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Equipment, bool>>>()))
                .Callback(
                    (Expression<Func<Equipment, bool>> pred) =>
                    foundEquipment = equipmentList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundEquipment);

            const int PlantAreaIdToUse = 2;
            const int EquipmentIdToUse = 2;
            const string EquipmentNameToFind = "Test Equipment 1";

            var equipmentService = this.SubjectUnderTest();

            // ACT 
            var result = equipmentService.EquipmentExists(EquipmentNameToFind, EquipmentIdToUse, PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.True);

            MockEquipmentRepository.Verify();
        }

        [Test]
        public void ReturnsFalseIfUpdatingEquipmentNotFoundByName()
        {
            var equipmentList = new List<Equipment>
                                    {
                                        new Equipment
                                            {
                                                Id = 1,
                                                PlantAreaId = 1,
                                                Name = "Test Equipment 1"
                                            },
                                        new Equipment
                                            {
                                                Id = 2,
                                                PlantAreaId = 1,
                                                Name = "Test Equipment 2"
                                            },
                                        new Equipment
                                            {
                                                Id = 3,
                                                PlantAreaId = 2,
                                                Name = "Test Equipment 1"
                                            },
                                        new Equipment
                                            {
                                                Id = 4,
                                                PlantAreaId = 2,
                                                Name = "Test Equipment 2"
                                            }
                                    };

            Equipment foundEquipment = null;

            MockEquipmentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Equipment, bool>>>()))
                .Callback(
                    (Expression<Func<Equipment, bool>> pred) =>
                    foundEquipment = equipmentList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundEquipment);

            const int EquipmentIdToUse = 2;
            const int PlantAreaIdToUse = 2;
            const string EquipmentNameToFind = "Test Equipment 3";

            var equipmentService = this.SubjectUnderTest();

            // ACT 
            var result = equipmentService.EquipmentExists(EquipmentNameToFind, EquipmentIdToUse, PlantAreaIdToUse);

            // ASSERT
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class GetInstrumentsAttachedToMethod : EquipmentServiceTestsBase
    {
        [Test]
        public void ReturnsListOfFoundInstruments()
        {
            var channelList = new List<Channel>
                                    {
                                        new Channel
                                            {
                                                ConnectedToEquipmentId = 1,
                                                Instrument = new Instrument()
                                                                 {
                                                                    Id = 1                                                                     
                                                                 }                                                
                                            },
                                        new Channel
                                            {
                                                ConnectedToEquipmentId = 1,
                                                Instrument = new Instrument()
                                                                 {
                                                                    Id = 2                                                                     
                                                                 }                                                
                                            },
                                        new Channel
                                            {
                                                ConnectedToEquipmentId = 2,
                                                Instrument = new Instrument()
                                                                 {
                                                                    Id = 3                                                                     
                                                                 }                                                
                                            }
                                    };

            // ARRANGE
            MockChannelService.Setup(c => c.GetChannelsForEquipment(It.IsAny<int>()))
                .Returns((int equipmentId) => channelList.Where(c => c.ConnectedToEquipmentId == equipmentId));
  
            const int EquipmentIdToUse = 1;

            // ACT 
            var result = SubjectUnderTest().GetInstrumentsAttachedTo(EquipmentIdToUse);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ReturnsEmptyWhenNoEquipmentsFound()
        {
            var channelList = new List<Channel>
                                    {
                                        new Channel
                                            {
                                                ConnectedToEquipmentId = 1,
                                                Instrument = new Instrument()
                                                                 {
                                                                    Id = 1                                                                     
                                                                 }                                                
                                            },
                                        new Channel
                                            {
                                                ConnectedToEquipmentId = 1,
                                                Instrument = new Instrument()
                                                                 {
                                                                    Id = 2                                                                     
                                                                 }                                                
                                            },
                                        new Channel
                                            {
                                                ConnectedToEquipmentId = 2,
                                                Instrument = new Instrument()
                                                                 {
                                                                    Id = 3                                                                     
                                                                 }                                                
                                            }
                                    };

            // ARRANGE
            MockChannelService.Setup(c => c.GetChannelsForEquipment(It.IsAny<int>()))
                .Returns((int equipmentId) => channelList.Where(c => c.ConnectedToEquipmentId == equipmentId));

            const int EquipmentIdToUse = 4;

            var result = SubjectUnderTest().GetInstrumentsAttachedTo(EquipmentIdToUse);

            // ASSERT
            Assert.That(result, Is.Empty);
        }
    }

    [TestFixture]
    public class AllocateToChannelMethod : EquipmentServiceTestsBase
    {
        [Test]
        public void AllocatesValidEquipmentToValidChannelCorrectly()
        {
            var channelList = new List<Channel>
                                    {
                                        new Channel { Id = 1, Name = "Test Channel 1" },
                                        new Channel { Id = 2, Name = "Test Channel 2" }
                                    };

            var equipmentList = new List<Equipment>
                                    {
                                        new Equipment { Id = 1, Name = "Test Equipment 1" },
                                        new Equipment { Id = 2, Name = "Test Equipment 2" }
                                    };

            const int EquipmentIdToUse = 1;
            const int ChannelIdToAllocate = 1;

            MockChannelService.Setup(s => s.GetChannelById(It.IsAny<int>()))
                .Returns(
                    (int channelId) => channelList.AsQueryable()
                                           .SingleOrDefault(ch => ch.Id == channelId));
            
            MockEquipmentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Equipment, bool>>>()))
                .Returns((Expression<Func<Equipment, bool>> pred) => equipmentList.AsQueryable().SingleOrDefault(pred));

            MockChannelService.Setup(cs => cs.SaveChannel(It.IsAny<Channel>()))
                .Verifiable();

            SubjectUnderTest().AllocateToChannel(EquipmentIdToUse, ChannelIdToAllocate);

            MockChannelService.Verify(cs => cs.SaveChannel(It.IsAny<Channel>()));
            Assert.That(channelList[0].ConnectedToEquipmentId == EquipmentIdToUse);
        }

        [Test]
        public void ThrowsChannelAllocationExceptionWhenInvalidEquipment()
        {        
            var channelList = new List<Channel>
                                  {
                                      new Channel { Id = 1, Name = "Test Channel 1" },
                                      new Channel { Id = 2, Name = "Test Channel 2" }
                                  };

            var equipmentList = new List<Equipment>
                                    {
                                        new Equipment { Id = 1, Name = "Test Equipment 1" },
                                        new Equipment { Id = 2, Name = "Test Equipment 2" }
                                    };

            MockEquipmentRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Equipment, bool>>>()))
                .Returns((Expression<Func<Equipment, bool>> pred) => equipmentList.AsQueryable().SingleOrDefault(pred));

            const int EquipmentIdToUse = 3;
            const int ChannelIdToAllocate = 1;

            MockChannelService.Setup(s => s.GetChannelById(It.IsAny<int>()))
                .Returns(
                    (int channelId) => channelList.AsQueryable()
                                           .SingleOrDefault(ch => ch.Id == channelId));

            Assert.Throws<ChannelAllocationException>(() => SubjectUnderTest().AllocateToChannel(EquipmentIdToUse, ChannelIdToAllocate));
        }

        [Test]
        public void ThrowsChannelAllocationExceptionWhenInvalidChannel()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel { Id = 1, Name = "Test Channel 1" },
                                      new Channel { Id = 2, Name = "Test Channel 2" }
                                  };

            const int EquipmentIdToUse = 1;
            const int ChannelIdToAllocate = 3;

            MockChannelService.Setup(s => s.GetChannelById(It.IsAny<int>()))
                .Returns(
                    (int channelId) => channelList.AsQueryable()
                                           .SingleOrDefault(ch => ch.Id == channelId));

             Assert.Throws<ChannelAllocationException>(() => SubjectUnderTest().AllocateToChannel(EquipmentIdToUse, ChannelIdToAllocate));
        }
    }

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deallocate", Justification = "Valid Test class name")]
    [TestFixture]
    public class DeallocateChannelMethod : EquipmentServiceTestsBase
    {
        [Test]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deallocates", Justification = "Deallocates is valid name, describes action")]
        public void DeallocatesEquipmentFromChannelCorrectly()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel { Id = 1, Name = "Test Channel 1", ConnectedToEquipmentId = 2 },
                                      new Channel { Id = 2, Name = "Test Channel 2" }
                                  };

            const int ChannelIdToUse = 1;

            MockChannelService.Setup(s => s.GetChannelById(It.IsAny<int>()))
                .Returns(
                    (int channelId) => channelList.AsQueryable()
                                           .SingleOrDefault(ch => ch.Id == channelId));

            MockChannelService.Setup(cs => cs.SaveChannel(It.IsAny<Channel>()))
                .Verifiable();

            SubjectUnderTest().DeallocateChannel(ChannelIdToUse);            

            MockChannelService.Verify(cs => cs.SaveChannel(It.IsAny<Channel>()));
            Assert.IsFalse(channelList[1].ConnectedToEquipmentId.HasValue);
        }

        [Test]
        public void ThrowsChannelAllocationExceptionWhenInvalidEquipment()
        {        
            var channelList = new List<Channel>
                                  {
                                      new Channel { Id = 1, Name = "Test Channel 1" },
                                      new Channel { Id = 2, Name = "Test Channel 2" }
                                  };

            const int ChannelIdToUse = 3;

            MockChannelService.Setup(s => s.GetChannelById(It.IsAny<int>()))
                .Returns(
                    (int channelId) => channelList.AsQueryable()
                                           .SingleOrDefault(ch => ch.Id == channelId));

            Assert.Throws<ChannelAllocationException>(() => SubjectUnderTest().DeallocateChannel(ChannelIdToUse));
        }
    }
}
