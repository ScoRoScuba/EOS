namespace EOS2.Services.Tests.ChannelServiceTests
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices.WindowsRuntime;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Services.BusinessDomain;

    using Moq;

    using NUnit.Framework;

    public abstract class ChannelServiceTestsBase
    {
        protected Mock<IRepository<Channel>> MockChannelRepository { get; set; }

        protected Mock<IInstrumentService> MockInstrumentService { get; set; }

        protected Mock<IReferenceDataService> MockReferenceDataService { get; set; }

        [SetUp]
        public void FixtureSetup()
        {
            MockChannelRepository = new Mock<IRepository<Channel>>();
            MockInstrumentService = new Mock<IInstrumentService>();
            MockReferenceDataService = new Mock<IReferenceDataService>();
        }

        public IChannelService ServiceUnderTest()
        {
            return new ChannelService(MockChannelRepository.Object, MockInstrumentService.Object, MockReferenceDataService.Object);
        }        
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class GetChannelsForInstrumentMethod : ChannelServiceTestsBase
    {
        [Test]
        public void ReturnsListOfItemsForSelectedChannel()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 1 },
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 2 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 3 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 4 },
                                  };

            IList<Channel> resultList = null;

            MockChannelRepository.Setup(s => s.FindAll(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                    resultList = channelList.AsQueryable().Where(pred).ToList())
                .Returns(() => resultList);

            const int InstrumentIdToUse = 1;

            var result = this.ServiceUnderTest().GetChannelsForInstrument(InstrumentIdToUse);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ReturnsEmptyListWhenNoChannelsFound()
        {
            var channelList = new List<Channel>();
            IList<Channel> resultList = null;

            MockChannelRepository.Setup(s => s.FindAll(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                    resultList = channelList.AsQueryable().Where(pred).ToList())
                .Returns(() => resultList);

            const int InstrumentIdToUse = 1;

            var result = this.ServiceUnderTest().GetChannelsForInstrument(InstrumentIdToUse);

            Assert.That(result, Is.Empty);
        }
    }

    [TestFixture]
    public class GetChannelsForEquipment : ChannelServiceTestsBase
    {
        [Test]
        public void ReturnsListOfChannelsForSelectedEquipment()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel
                                          {
                                              InstrumentId = 1,
                                              ConnectedToEquipmentId = 1,
                                              Name = "Channel",
                                              Id = 1
                                          },
                                      new Channel
                                          {
                                              InstrumentId = 1,
                                              ConnectedToEquipmentId = 1,
                                              Name = "Channel",
                                              Id = 2
                                          },
                                      new Channel
                                          {
                                              InstrumentId = 2,
                                              ConnectedToEquipmentId = 2,
                                              Name = "Channel",
                                              Id = 3
                                          },
                                      new Channel
                                          {
                                              InstrumentId = 2,
                                              ConnectedToEquipmentId = 3,
                                              Name = "Channel",
                                              Id = 4
                                          },
                                  };

            IList<Channel> resultList = null;

            MockChannelRepository.Setup(s => s.FindAll(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                    resultList = channelList.AsQueryable().Where(pred).ToList())
                .Returns(() => resultList);

            const int EquipmentIdToUse = 1;

            var result = this.ServiceUnderTest().GetChannelsForEquipment(EquipmentIdToUse);

            const int ExpectedQuantity = 2;

            Assert.That(result, Is.InstanceOf<IEnumerable<Channel>>());
            Assert.That(result.Count(), Is.EqualTo(ExpectedQuantity));
        }

        [Test]
        public void ReturnsEmptyListWhenNoChannelsFound()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel
                                          {
                                              InstrumentId = 1,
                                              ConnectedToEquipmentId = 1,
                                              Name = "Channel",
                                              Id = 1
                                          },
                                      new Channel
                                          {
                                              InstrumentId = 1,
                                              ConnectedToEquipmentId = 1,
                                              Name = "Channel",
                                              Id = 2
                                          },
                                      new Channel
                                          {
                                              InstrumentId = 2,
                                              ConnectedToEquipmentId = 2,
                                              Name = "Channel",
                                              Id = 3
                                          },
                                      new Channel
                                          {
                                              InstrumentId = 2,
                                              ConnectedToEquipmentId = 3,
                                              Name = "Channel",
                                              Id = 4
                                          },
                                  };

            IList<Channel> resultList = null;

            MockChannelRepository.Setup(s => s.FindAll(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                    resultList = channelList.AsQueryable().Where(pred).ToList())
                .Returns(() => resultList);

            const int EquipmentIdToUse = 5;

            var result = this.ServiceUnderTest().GetChannelsForEquipment(EquipmentIdToUse);

            Assert.That(result, Is.InstanceOf<IEnumerable<Channel>>());
            Assert.That(result, Is.Empty);      
        }
    }   

    [TestFixture]
    public class GetChannelByIdMethod : ChannelServiceTestsBase
    {
        [Test]
        public void ReturnsChannel()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 1 },
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 2 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 3 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 4 },
                                  };

            Channel foundChannel = null;

            MockChannelRepository.Setup(s => s.Find(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                    foundChannel = channelList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundChannel);

            const int ChannelIdToUse = 1;

            var result = this.ServiceUnderTest().GetChannelById(ChannelIdToUse);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));            
        }

        [Test]
        public void ReturnsNullWhenNotFound()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 1 },
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 2 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 3 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 4 },
                                  };

            Channel foundChannel = null;

            MockChannelRepository.Setup(s => s.Find(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                    foundChannel = channelList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundChannel);

            const int ChannelIdToUse = 5;

            var result = this.ServiceUnderTest().GetChannelById(ChannelIdToUse);

            Assert.That(result, Is.Null);
        }
    }

    [TestFixture]
    public class GetUnallocatedChannelForMethod : ChannelServiceTestsBase
    {
        [Test]
        public void ReturnsListOfUnallocatedChannels()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 1 },
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 2 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 3, ConnectedToEquipmentId = 1 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 4, ConnectedToEquipmentId = 1 },
                                  };

            IList<Channel> resultList = null;

            MockChannelRepository.Setup(s => s.FindAll(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                    resultList = channelList.AsQueryable().Where(pred).ToList())
                .Returns(() => resultList);

            const int InstrumentIdToUse = 1;

            var result = this.ServiceUnderTest().GetUnallocatedChannelsFor(InstrumentIdToUse);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ReturnsEmptyListAllChannelsHaveBeenAllocated()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 1 },
                                      new Channel { InstrumentId = 1, Name = "Channel", Id = 2 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 3, ConnectedToEquipmentId = 1 },
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 4, ConnectedToEquipmentId = 1 },
                                  };

            IList<Channel> resultList = null;

            MockChannelRepository.Setup(s => s.FindAll(It.IsAny<Expression<Func<Channel, bool>>>()))
                .Callback(
                    (Expression<Func<Channel, bool>> pred) =>
                    resultList = channelList.AsQueryable().Where(pred).ToList())
                .Returns(() => resultList);

            const int InstrumentIdToUse = 2;

            var result = this.ServiceUnderTest().GetUnallocatedChannelsFor(InstrumentIdToUse);

            Assert.That(result, Is.Empty);
        }
    }

    [TestFixture]
    public class SaveChannelMethod : ChannelServiceTestsBase
    {
        [Test]
        public void SavesChannelToStoreChannel()
        {
            var channelList = new List<Channel>();

            MockChannelRepository.Setup(s => s.Add(It.IsAny<Channel>()))
                .Callback((Channel newItem) => channelList.Add(newItem));

            var channelToAdd = new Channel { InstrumentId = 2, Name = "Channel",  ConnectedToEquipmentId = 1 };

            this.ServiceUnderTest().SaveChannel(channelToAdd);

            // Assert ( Test its worked )
            Assert.That(channelList, Is.Not.Empty);            
            Assert.That(channelList[0].Name, Is.EqualTo("Channel"));
        }

        [Test]
        public void UpdatesChannelInStore()
        {
            var channelList = new List<Channel>
                                  {
                                      new Channel { InstrumentId = 2, Name = "Channel", Id = 3, ConnectedToEquipmentId = 1 }
                                  };

            MockChannelRepository.Setup(s => s.Update(It.IsAny<Channel>()))
                .Callback(
                    (Channel updatedItem) =>
                        {
                            channelList.Single(c => c.Id == updatedItem.Id).Name = updatedItem.Name;
                        });

            var channelToUpdate = new Channel { InstrumentId = 2, Name = "Updatad Channel", Id = 3, ConnectedToEquipmentId = 1 };

            this.ServiceUnderTest().SaveChannel(channelToUpdate);

            // Assert ( Test its worked )
            Assert.That(channelList[0].Name, Is.EqualTo("Updatad Channel"));            
        }

        [Test]
        public void ThrowsArgumentNullExceptionWhenNullChannel()
        {
            Assert.Throws<ArgumentNullException>(() => this.ServiceUnderTest().SaveChannel(null));
        }
    }

    [TestFixture]
    public class CreateDefaultSetOfChannelsForInstrument : ChannelServiceTestsBase
    {
        [Test]
        public void CreatesChannelsCorrectly()
        {
            var channelStore = new List<Channel>();

            MockInstrumentService.Setup(i => i.GetInstrument(It.IsAny<int>()))
                .Returns(new Instrument { Id = 1, Name = "Instrument" });

            MockChannelRepository.Setup(s => s.Add(It.IsAny<Channel>()))
                .Callback((Channel newItem) => channelStore.Add(newItem));

            MockReferenceDataService.Setup(r => r.GetChannelTypes())
                .Returns(new List<ChannelType> { new ChannelType { Id = 1, Name = "Analogue" } });

            const int NumberOfChannelsToUse = 5;
            const int InstrumentIdToUse = 1;
            const int ChannelTypeToUse = 1;
            const int ConnectedToEquipmentToUse = 0;
            const int ScheduleTypeIdToUse = 1;

            this.ServiceUnderTest()
                .CreateDefaultSetOfChannelsForInstrument(
                    NumberOfChannelsToUse,
                    InstrumentIdToUse,
                    ChannelTypeToUse,
                    ConnectedToEquipmentToUse,
                    ScheduleTypeIdToUse);

            Assert.That(channelStore, Is.Not.Empty);
            Assert.That(channelStore.Count(), Is.EqualTo(5));
        }

        [Test]
        public void ChannelNameIsCreatedCorrectly()
        {
            var channelStore = new List<Channel>();

            MockInstrumentService.Setup(i => i.GetInstrument(It.IsAny<int>()))
                .Returns(new Instrument { Id = 1, Name = "Instrument" });

            MockChannelRepository.Setup(s => s.Add(It.IsAny<Channel>()))
                .Callback((Channel newItem) => channelStore.Add(newItem));

            MockReferenceDataService.Setup(r => r.GetChannelTypes())
                .Returns(new List<ChannelType> { new ChannelType { Id = 1, Name = "Analogue" } });

            const int NumberOfChannelsToUse = 1;
            const int InstrumentIdToUse = 1;
            const int ChannelTypeToUse = 1;
            const int ConnectedToEquipmentToUse = 0;
            const int ScheduleTypeIdToUse = 1;

            this.ServiceUnderTest()
                .CreateDefaultSetOfChannelsForInstrument(
                    NumberOfChannelsToUse,
                    InstrumentIdToUse,
                    ChannelTypeToUse,
                    ConnectedToEquipmentToUse,
                    ScheduleTypeIdToUse);

            Assert.That(channelStore, Is.Not.Empty);
            Assert.That(channelStore[0].Name, Is.EqualTo("0000-Instrument"));            
        }

        [Test]
        public void ChannelNumberIsCreatedCorrectly()
        {
            var channelStore = new List<Channel>();

            MockInstrumentService.Setup(i => i.GetInstrument(It.IsAny<int>()))
                .Returns(new Instrument { Id = 1, Name = "Instrument" });

            MockChannelRepository.Setup(s => s.Add(It.IsAny<Channel>()))
                .Callback((Channel newItem) => channelStore.Add(newItem));

            MockReferenceDataService.Setup(r => r.GetChannelTypes())
                .Returns(new List<ChannelType> { new ChannelType { Id = 1, Name = "Analogue" } });

            const int NumberOfChannelsToUse = 1;
            const int InstrumentIdToUse = 1;
            const int ChannelTypeToUse = 1;
            const int ConnectedToEquipmentToUse = 0;
            const int ScheduleTypeIdToUse = 1;

            this.ServiceUnderTest()
                .CreateDefaultSetOfChannelsForInstrument(
                    NumberOfChannelsToUse,
                    InstrumentIdToUse,
                    ChannelTypeToUse,
                    ConnectedToEquipmentToUse,
                    ScheduleTypeIdToUse);

            Assert.That(channelStore, Is.Not.Empty);
            Assert.That(channelStore[0].Number, Is.EqualTo("0000"));            
        }

        [Test]
        public void EquipmentIdIsSet()
        {
            var channelStore = new List<Channel>();

            MockInstrumentService.Setup(i => i.GetInstrument(It.IsAny<int>()))
                .Returns(new Instrument { Id = 1, Name = "Instrument" });

            MockChannelRepository.Setup(s => s.Add(It.IsAny<Channel>()))
                .Callback((Channel newItem) => channelStore.Add(newItem));

            MockReferenceDataService.Setup(r => r.GetChannelTypes())
                .Returns(new List<ChannelType> { new ChannelType { Id = 1, Name = "Analogue" } });

            const int NumberOfChannelsToUse = 1;
            const int InstrumentIdToUse = 1;
            const int ChannelTypeToUse = 1;
            const int ConnectedToEquipmentToUse = 1;
            const int ScheduleTypeIdToUse = 1;

            this.ServiceUnderTest()
                .CreateDefaultSetOfChannelsForInstrument(
                    NumberOfChannelsToUse,
                    InstrumentIdToUse,
                    ChannelTypeToUse,
                    ConnectedToEquipmentToUse,
                    ScheduleTypeIdToUse);

            const int ExpectedEquipmentId = 1;

            Assert.That(channelStore, Is.Not.Empty);
            Assert.That(channelStore[0].ConnectedToEquipmentId, Is.EqualTo(ExpectedEquipmentId));            
        }

        [Test]
        public void WhenNoInstrumentIdServiceResultContainsArgumentOutOfRangeException()
        {
            const int NumberOfChannelsToUse = 1;
            const int ChannelTypeToUse = 1;
            const int ConnectedToEquipmentToUse = 1;
            const int ScheduleTypeIdToUse = 1;

            const int InstrumentIdToUse = 0;

            var result = this.ServiceUnderTest()
                .CreateDefaultSetOfChannelsForInstrument(
                    NumberOfChannelsToUse,
                    InstrumentIdToUse,
                    ChannelTypeToUse,
                    ConnectedToEquipmentToUse,
                    ScheduleTypeIdToUse);

            Assert.That(result, Is.InstanceOf<ServiceResultDictionary>());
            Assert.That(result.HasErrors, Is.True);
            Assert.That(result.ContainsKey("InstrumentId"), Is.True);

            ServiceState savedValue;

            result.TryGetValue("InstrumentId", out savedValue);
            Assert.That(savedValue.Errors[0].Exception, Is.InstanceOf<ArgumentOutOfRangeException>());
        }
    }
}
