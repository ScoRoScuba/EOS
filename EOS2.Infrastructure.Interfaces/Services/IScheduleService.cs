namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Model;

    public interface IScheduleService
    {
        ServiceResultDictionary SaveSchedule(Schedule schedule);

        Schedule GetSchedule(int scheduleId);

        IEnumerable<Schedule> GetSchedulesForEquipment(int equipmentId);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This should remain a method as it makes a db call")]
        IEnumerable<FurnaceClass> GetFurnaceClasses();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This should remain a method as it makes a db call")]
        IEnumerable<ScheduleFrequency> GetFrequencies();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This should remain a method as it makes a db call")]
        IEnumerable<ScheduleType> GetScheduleTypes();

        bool ScheduleExists(string name, int equipmentId, int id);

        bool ScheduleExists(int scheduleTypeId, int furnaceClassId, int equipmentId, int id);
    }
}
