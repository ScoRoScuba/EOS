namespace EOS2.Services.Tests.ScheduleServiceTests
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

    public abstract class ScheduleServiceTestsBase
    {
        protected Mock<IRepository<Schedule>> MockScheduleRepository { get; set; }
        
        protected Mock<IRepository<ScheduleType>> MockScheduleTypeRepository { get; set; }
        
        protected Mock<IRepository<ScheduleFrequency>> MockScheduleFrequencyRepository { get; set; }
        
        protected Mock<IRepository<FurnaceClass>> MockFurnaceClassRepository { get; set; }

        [SetUp]
        public void FixtureSetup()
        {
            MockScheduleRepository = new Mock<IRepository<Schedule>>();
            MockScheduleFrequencyRepository = new Mock<IRepository<ScheduleFrequency>>();
            MockFurnaceClassRepository = new Mock<IRepository<FurnaceClass>>();
        }

        public IScheduleService ServiceUnderTest()
        {
            return new ScheduleService(MockScheduleRepository.Object, MockFurnaceClassRepository.Object, MockScheduleFrequencyRepository.Object);
        }        
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class SaveScheduleMethod : ScheduleServiceTestsBase
    {
        [Test]
        public void SaveScheduleToRepositoryWithNullModel()
        {
            var scheduleService = ServiceUnderTest();

            // ACT ( Run the actual Test ) &  Assert ( Test its worked )
            Assert.Throws<ArgumentNullException>(() => scheduleService.SaveSchedule(null));
        }

        [Test]
        public void AddsScheduleToRepository()
        {
            var scheduleList = new List<Schedule>();
            var scheduleToAdd = new Schedule();

            MockScheduleRepository.Setup(a => a.Add(It.IsAny<Schedule>())).Callback((Schedule schedule) => scheduleList.Add(schedule));

            MockScheduleRepository.Setup(a => a.GetAll()).Returns(scheduleList);

            var scheduleService = ServiceUnderTest();

            // ACT ( Run the actual Test )
            scheduleService.SaveSchedule(scheduleToAdd);

            // Assert ( Test its worked )
            Assert.That(scheduleList, Is.Not.Empty);
        }

        [Test]
        public void SaveScheduleToRepositoryWithCorrectEquipmentId()
        {
            const int NewEquipmentId = 1;

            var scheduleList = new List<Schedule>();
            var scheduleToAdd = new Schedule { EquipmentId = NewEquipmentId };

            MockScheduleRepository.Setup(a => a.Add(It.IsAny<Schedule>())).Callback((Schedule schedule) => scheduleList.Add(schedule));

            MockScheduleRepository.Setup(a => a.GetAll()).Returns(scheduleList);

            var scheduleService = ServiceUnderTest();

            // ACT ( Run the actual Test )
            scheduleService.SaveSchedule(scheduleToAdd);

            // Assert ( Test its worked )
            Assert.That(scheduleList.Count(), Is.EqualTo(1));
            Assert.That(scheduleList.Count(i => i.EquipmentId == NewEquipmentId), Is.EqualTo(1));
        }

        [Test]
        public void SaveScheduleUpdatesExisting()
        {
            const int OwningEquipmentId = 1;

            var scheduleList = new List<Schedule>
                                {
                                    new Schedule { Id = 1, EquipmentId = OwningEquipmentId, Name = "Schedule Name" },
                                    new Schedule { Id = 2, EquipmentId = OwningEquipmentId, Name = "Schedule Name" }
                                };

            MockScheduleRepository.Setup(a => a.Update(It.IsAny<Schedule>())).Callback(
                (Schedule schedule) =>
                {
                    scheduleList.Single(i => i.Id == schedule.Id).Name = schedule.Name;
                });

            var scheduleService = ServiceUnderTest();

            var scheduleToUpdate = new Schedule { Id = 1, EquipmentId = OwningEquipmentId, Name = "Schedule New Name" };

            // ACT ( Run the actual Test )
            scheduleService.SaveSchedule(scheduleToUpdate);

            Assert.That(scheduleList.Single(e => e.Id == 1).Name, Is.EqualTo(scheduleToUpdate.Name));
            Assert.That(scheduleList.Single(e => e.Id == 2).Name, Is.Not.EqualTo(scheduleToUpdate.Name));
        }
    }

    [TestFixture]
    public class GetScheduleMethod : ScheduleServiceTestsBase
    {
        [Test]
        public void ReturnsFoundSchedule()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                {
                                    new Schedule { Id = 1 },
                                    new Schedule { Id = 2 },
                                };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) => foundSchedule = scheduleList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundSchedule);

            const int ScheduleIdToUse = 1;
            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.GetSchedule(ScheduleIdToUse);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public void ReturnsNullWhenNoScheduleFound()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                {
                                    new Schedule { Id = 1 },
                                    new Schedule { Id = 2 },
                                };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) => foundSchedule = scheduleList.AsQueryable()
                        .SingleOrDefault(pred)).Returns(() => foundSchedule);

            const int ScheduleIdToUse = 3;
            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.GetSchedule(ScheduleIdToUse);

            // ASSERT
            Assert.That(result, Is.Null);
        }
    }

    [TestFixture]
    public class GetSchedulesByEquipmentMethod : ScheduleServiceTestsBase
    {
        [Test]
        public void ReturnsFoundSchedules()
        {
            // ARRANGE
            var itemList = new List<Schedule>
                                {
                                    new Schedule { Id = 1, EquipmentId = 1 },
                                    new Schedule { Id = 2, EquipmentId = 1 },
                                    new Schedule { Id = 3, EquipmentId = 2 },
                                };

            var foundItems = new List<Schedule>();

            MockScheduleRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback((Expression<Func<Schedule, bool>> pred) => foundItems = itemList.AsQueryable().Where(pred).ToList()).Returns(() => foundItems);

            const int EquipmentIdToUse = 1;
            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.GetSchedulesForEquipment(EquipmentIdToUse);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ReturnsEmptyWhenNoSchedulesFound()
        {
            // ARRANGE
            var itemList = new List<Schedule>
                                {
                                    new Schedule { Id = 1, EquipmentId = 1 },
                                    new Schedule { Id = 2, EquipmentId = 1 },
                                    new Schedule { Id = 3, EquipmentId = 2 },
                                };

            var foundItems = new List<Schedule>();

            MockScheduleRepository.Setup(m => m.FindAll(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback((Expression<Func<Schedule, bool>> pred) => foundItems = itemList.AsQueryable().Where(pred).ToList()).Returns(() => foundItems);

            const int EquipmentIdToUse = 3;
            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.GetSchedulesForEquipment(EquipmentIdToUse);

            // ASSERT
            Assert.That(result, Is.Empty);
        }
    }

    [TestFixture]
    public class ScheduleExists : ScheduleServiceTestsBase
    {
        [Test]
        public void ReturnsTrueIfAddingScheduleFoundByName()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                    {
                                        new Schedule
                                            {
                                                Id = 1,
                                                EquipmentId = 1,
                                                Name = "Test Schedule 1"
                                            },
                                        new Schedule
                                            {
                                                Id = 2,
                                                EquipmentId = 2,
                                                Name = "Test Schedule 2"
                                            },
                                    };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 1;
            const string ScheduleNameToFind = "Test Schedule 1";
            const int NewScheduleId = new int();

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(ScheduleNameToFind, EquipmentIdToUse, NewScheduleId);

            // ASSERT
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReturnsFalseIfAddingScheduleNotFoundByName()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                    {
                                        new Schedule
                                            {
                                                Id = 1,
                                                EquipmentId = 1,
                                                Name = "Test Schedule 1"
                                            },
                                        new Schedule
                                            {
                                                Id = 2,
                                                EquipmentId = 2,
                                                Name = "Test Schedule 2"
                                            },
                                    };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 2;
            const string ScheduleNameToFind = "Test Schedule 1";
            const int NewScheduleId = new int();

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(ScheduleNameToFind, NewScheduleId, EquipmentIdToUse);

            // ASSERT
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnsTrueIfUpdatingScheduleFoundByName()
        {
            var scheduleList = new List<Schedule>
                                    {
                                        new Schedule
                                            {
                                                Id = 1,
                                                EquipmentId = 1,
                                                Name = "Test Schedule 1"
                                            },
                                        new Schedule
                                            {
                                                Id = 2,
                                                EquipmentId = 1,
                                                Name = "Test Schedule 2"
                                            },
                                        new Schedule
                                            {
                                                Id = 3,
                                                EquipmentId = 2,
                                                Name = "Test Schedule 1"
                                            },
                                        new Schedule
                                            {
                                                Id = 4,
                                                EquipmentId = 2,
                                                Name = "Test Schedule 2"
                                            }
                                    };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 2;
            const int ScheduleIdToUse = 2;
            const string ScheduleNameToFind = "Test Schedule 1";

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(ScheduleNameToFind, ScheduleIdToUse, EquipmentIdToUse);

            // ASSERT
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReturnsFalseIfUpdatingScheduleNotFoundByName()
        {
            var scheduleList = new List<Schedule>
                                    {
                                        new Schedule
                                            {
                                                Id = 1,
                                                EquipmentId = 1,
                                                Name = "Test Schedule 1"
                                            },
                                        new Schedule
                                            {
                                                Id = 2,
                                                EquipmentId = 1,
                                                Name = "Test Schedule 2"
                                            },
                                        new Schedule
                                            {
                                                Id = 3,
                                                EquipmentId = 2,
                                                Name = "Test Schedule 1"
                                            },
                                        new Schedule
                                            {
                                                Id = 4,
                                                EquipmentId = 2,
                                                Name = "Test Schedule 2"
                                            }
                                    };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int ScheduleIdToUse = 2;
            const int EquipmentIdToUse = 2;
            const string ScheduleNameToFind = "Test Schedule 3";

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(ScheduleNameToFind, ScheduleIdToUse, EquipmentIdToUse);

            // ASSERT
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnsTrueIfAddingScheduleFoundByEquipmentTypeAndClass()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                        {
                                            new Schedule
                                                {
                                                    Id = 1,
                                                    EquipmentId = 10,
                                                    TypeId = 11,
                                                    FurnaceClassId = 12
                                                },
                                            new Schedule
                                                {
                                                    Id = 2,
                                                    EquipmentId = 10,
                                                    TypeId = 13,
                                                    FurnaceClassId = 12
                                                },
                                        };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 10;
            const int TypeIdToUse = 11;
            const int FurnaceClassIdToUse = 12;
            const int NewScheduleId = new int();

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(TypeIdToUse, FurnaceClassIdToUse, EquipmentIdToUse, NewScheduleId);

            // ASSERT
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReturnsFalseIfAddingScheduleNotFoundByEquipmentTypeAndClassWhereClassDiffers()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                        {
                                            new Schedule
                                                {
                                                    Id = 1,
                                                    EquipmentId = 10,
                                                    TypeId = 11,
                                                    FurnaceClassId = 12
                                                },
                                            new Schedule
                                                {
                                                    Id = 2,
                                                    EquipmentId = 10,
                                                    TypeId = 13,
                                                    FurnaceClassId = 12
                                                },
                                        };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 10;
            const int TypeIdToUse = 11;
            const int FurnaceClassIdToUse = 5;
            const int NewScheduleId = new int();

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(TypeIdToUse, FurnaceClassIdToUse, EquipmentIdToUse, NewScheduleId);

            // ASSERT
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnsFalseIfAddingScheduleNotFoundByEquipmentTypeAndClassWhereTypeDiffers()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                        {
                                            new Schedule
                                                {
                                                    Id = 1,
                                                    EquipmentId = 10,
                                                    TypeId = 11,
                                                    FurnaceClassId = 12
                                                },
                                            new Schedule
                                                {
                                                    Id = 2,
                                                    EquipmentId = 10,
                                                    TypeId = 13,
                                                    FurnaceClassId = 12
                                                },
                                        };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 10;
            const int TypeIdToUse = 12;
            const int FurnaceClassIdToUse = 12;
            const int NewScheduleId = new int();

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(TypeIdToUse, FurnaceClassIdToUse, EquipmentIdToUse, NewScheduleId);

            // ASSERT
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnsFalseIfAddingScheduleNotFoundByEquipmentTypeAndClassWhereEquipmentDiffers()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                        {
                                            new Schedule
                                                {
                                                    Id = 1,
                                                    EquipmentId = 10,
                                                    TypeId = 11,
                                                    FurnaceClassId = 12
                                                },
                                            new Schedule
                                                {
                                                    Id = 2,
                                                    EquipmentId = 10,
                                                    TypeId = 13,
                                                    FurnaceClassId = 12
                                                },
                                        };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 11;
            const int TypeIdToUse = 11;
            const int FurnaceClassIdToUse = 12;
            const int NewScheduleId = new int();

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(TypeIdToUse, FurnaceClassIdToUse, EquipmentIdToUse, NewScheduleId);

            // ASSERT
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnsTrueIfUpdatingScheduleFoundByEquipmentTypeAndClass()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                        {
                                            new Schedule
                                                {
                                                    Id = 1,
                                                    EquipmentId = 10,
                                                    TypeId = 11,
                                                    FurnaceClassId = 12
                                                },
                                            new Schedule
                                                {
                                                    Id = 2,
                                                    EquipmentId = 10,
                                                    TypeId = 13,
                                                    FurnaceClassId = 12
                                                },
                                        };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 10;
            const int TypeIdToUse = 13;
            const int FurnaceClassIdToUse = 12;
            const int NewScheduleId = 1;

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(TypeIdToUse, FurnaceClassIdToUse, EquipmentIdToUse, NewScheduleId);

            // ASSERT
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReturnsFalseIfUpdatingScheduleNotFoundByEquipmentTypeAndClassWhereClassDiffers()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                        {
                                            new Schedule
                                                {
                                                    Id = 1,
                                                    EquipmentId = 10,
                                                    TypeId = 11,
                                                    FurnaceClassId = 12
                                                },
                                            new Schedule
                                                {
                                                    Id = 2,
                                                    EquipmentId = 10,
                                                    TypeId = 13,
                                                    FurnaceClassId = 12
                                                },
                                        };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 10;
            const int TypeIdToUse = 11;
            const int FurnaceClassIdToUse = 5;
            const int NewScheduleId = 1;

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(TypeIdToUse, FurnaceClassIdToUse, EquipmentIdToUse, NewScheduleId);

            // ASSERT
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnsFalseIfUpdatingScheduleNotFoundByEquipmentTypeAndClassWhereTypeDiffers()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                        {
                                            new Schedule
                                                {
                                                    Id = 1,
                                                    EquipmentId = 10,
                                                    TypeId = 11,
                                                    FurnaceClassId = 12
                                                },
                                            new Schedule
                                                {
                                                    Id = 2,
                                                    EquipmentId = 10,
                                                    TypeId = 13,
                                                    FurnaceClassId = 12
                                                },
                                        };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 10;
            const int TypeIdToUse = 12;
            const int FurnaceClassIdToUse = 12;
            const int NewScheduleId = 1;

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(TypeIdToUse, FurnaceClassIdToUse, EquipmentIdToUse, NewScheduleId);

            // ASSERT
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnsFalseIfUpdatingScheduleNotFoundByEquipmentTypeAndClassWhereEquipmentDiffers()
        {
            // ARRANGE
            var scheduleList = new List<Schedule>
                                        {
                                            new Schedule
                                                {
                                                    Id = 1,
                                                    EquipmentId = 10,
                                                    TypeId = 11,
                                                    FurnaceClassId = 12
                                                },
                                            new Schedule
                                                {
                                                    Id = 2,
                                                    EquipmentId = 10,
                                                    TypeId = 13,
                                                    FurnaceClassId = 12
                                                },
                                        };

            Schedule foundSchedule = null;

            MockScheduleRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Schedule, bool>>>()))
                .Callback(
                    (Expression<Func<Schedule, bool>> pred) =>
                    foundSchedule = scheduleList.AsQueryable().SingleOrDefault(pred))
                .Returns(() => foundSchedule);

            const int EquipmentIdToUse = 11;
            const int TypeIdToUse = 11;
            const int FurnaceClassIdToUse = 12;
            const int NewScheduleId = 1;

            var scheduleService = ServiceUnderTest();

            // ACT 
            var result = scheduleService.ScheduleExists(TypeIdToUse, FurnaceClassIdToUse, EquipmentIdToUse, NewScheduleId);

            // ASSERT
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class GetAllScheduleTypesMethod : ScheduleServiceTestsBase
    {
        [Test]
        public void ReturnsAllScheduleTypes()
        {
            // ARRANGE
            var scheduleService = ServiceUnderTest();

            // ACT 
            var results = scheduleService.GetScheduleTypes();

            // ASSERT
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count(), Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class GetAllScheduleFrequenciesMethod : ScheduleServiceTestsBase
    {
        [Test]
        public void ReturnsAllScheduleFrequencies()
        {
            // ARRANGE
            var scheduleFrequencyList = new List<ScheduleFrequency>
                            {
                                new ScheduleFrequency { Id = 1, Name = "ScheduleFrequency 1" },
                                new ScheduleFrequency { Id = 2, Name = "ScheduleFrequency 2" },
                                new ScheduleFrequency { Id = 3, Name = "ScheduleFrequency 3" },
                                new ScheduleFrequency { Id = 4, Name = "ScheduleFrequency 4" },
                                new ScheduleFrequency { Id = 5, Name = "ScheduleFrequency 5" },
                            };

            MockScheduleFrequencyRepository.Setup(m => m.GetAll()).Returns(scheduleFrequencyList);

            var scheduleService = ServiceUnderTest();

            // ACT 
            var results = scheduleService.GetFrequencies();

            // ASSERT
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count(), Is.EqualTo(5));               
        }
    }

    [TestFixture]
    public class GetAllFurnaceClassesMethod : ScheduleServiceTestsBase
    {
        [Test]
        public void ReturnsAllScheduleFurnaceClasses()
        {
            // ARRANGE
            var furnaceClassList = new List<FurnaceClass>
                            {
                                new FurnaceClass { Id = 1, Name = "FurnaceClass 1" },
                                new FurnaceClass { Id = 2, Name = "FurnaceClass 2" },
                                new FurnaceClass { Id = 3, Name = "FurnaceClass 3" },
                                new FurnaceClass { Id = 4, Name = "FurnaceClass 4" },
                                new FurnaceClass { Id = 5, Name = "FurnaceClass 5" },
                            };

            MockFurnaceClassRepository.Setup(m => m.GetAll()).Returns(furnaceClassList);

            var scheduleService = ServiceUnderTest();

            // ACT 
            var results = scheduleService.GetFurnaceClasses();

            // ASSERT
            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count(), Is.EqualTo(5));                
        }
    }
}
