namespace EOS2.Services.BusinessDomain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class ScheduleService : IScheduleService
    {
        private readonly IRepository<Schedule> repository;

        private readonly IRepository<FurnaceClass> classRepository;

        private readonly IRepository<ScheduleFrequency> frequencyRepository;

        public ScheduleService(IRepository<Schedule> repository, IRepository<FurnaceClass> classRepository, IRepository<ScheduleFrequency> frequencyRepository)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (classRepository == null) throw new ArgumentNullException("classRepository");
            if (frequencyRepository == null) throw new ArgumentNullException("frequencyRepository");

            this.repository = repository;
            this.classRepository = classRepository;
            this.frequencyRepository = frequencyRepository;
        }

        public ServiceResultDictionary SaveSchedule(Schedule schedule)
        {
            if (schedule == null) throw new ArgumentNullException("schedule");

            var serviceResult = new ServiceResultDictionary();

            if (schedule.Id > 0)
            {
                repository.Update(schedule);
            }
            else
            {
                repository.Add(schedule);
            }

            return serviceResult;
        }

        public Schedule GetSchedule(int scheduleId)
        {
            var schedule = repository.Find(s => s.Id == scheduleId);

            return schedule;
        }

        public IEnumerable<Schedule> GetSchedulesForEquipment(int equipmentId)
        {
            return repository.FindAll(s => s.EquipmentId == equipmentId);
        }

        public IEnumerable<FurnaceClass> GetFurnaceClasses()
        {
            return classRepository.GetAll();
        }

        public IEnumerable<ScheduleFrequency> GetFrequencies()
        {
            return frequencyRepository.GetAll();
        }

        public IEnumerable<ScheduleType> GetScheduleTypes()
        {
            return (from int i in Enum.GetValues(typeof(Model.Enums.ScheduleType)) 
                    let name = typeof(Model.Enums.ScheduleType).GetMember(Enum.GetName(typeof(Model.Enums.ScheduleType), i)).First().GetCustomAttribute<DescriptionAttribute>().Description 
                    select new ScheduleType { Id = i, Name = name }).ToList();
        }

        public bool ScheduleExists(string name, int equipmentId, int id)
        {
            return repository.Find(s => s.Name.ToLower().Trim() == name.ToLower().Trim() && s.EquipmentId == equipmentId && s.Id != id) != null;
        }

        public bool ScheduleExists(int scheduleTypeId, int furnaceClassId, int equipmentId, int id)
        {
            return repository.Find(s => s.TypeId == scheduleTypeId && s.FurnaceClassId == furnaceClassId && s.EquipmentId == equipmentId && s.Id != id) != null;
        }
    }
}
