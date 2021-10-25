namespace EOS2.Web.Tests.TestStubs
{
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class StubScheduleService : IScheduleService
    {
        private readonly bool scheduleExistsReturnValue;

        public StubScheduleService(bool scheduleExistsReturnValue)
        {
            this.scheduleExistsReturnValue = scheduleExistsReturnValue;
        }

        public ServiceResultDictionary SaveSchedule(Schedule schedule)
        {
            throw new System.NotImplementedException();
        }

        public Schedule GetSchedule(int scheduleId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Schedule> GetSchedulesForEquipment(int equipmentId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FurnaceClass> GetFurnaceClasses()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ScheduleFrequency> GetFrequencies()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ScheduleType> GetScheduleTypes()
        {
            throw new System.NotImplementedException();
        }

        public bool ScheduleExists(string name, int equipmentId, int id)
        {
            return scheduleExistsReturnValue;
        }

        public bool ScheduleExists(int scheduleTypeId, int furnaceClassId, int equipmentId, int id)
        {
            return scheduleExistsReturnValue;
        }
    }
}
