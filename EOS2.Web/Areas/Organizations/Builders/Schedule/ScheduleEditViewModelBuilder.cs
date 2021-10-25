namespace EOS2.Web.Areas.Organizations.Builders.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Schedules;
    using EOS2.Web.Builders;

    using Common = ViewModels.Common;

    public class ScheduleEditViewModelBuilder : IEditViewPartialModelBuilder<ScheduleEditViewModel>
    {
        private readonly IScheduleService scheduleService;

        public ScheduleEditViewModelBuilder(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        public ScheduleEditViewModel Build(int? id)
        {
            var viewModel = new ScheduleEditViewModel();

            if (id.HasValue) viewModel = Mapper.Map<ScheduleEditViewModel>(scheduleService.GetSchedule(id.Value));

            // Add in Reference Data
            this.GetReferenceData(viewModel);

            return viewModel;
        }

        public ScheduleEditViewModel Rebuild(ScheduleEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (viewModel.ScheduleTypes == null) viewModel.ScheduleTypes = this.GetScheduleTypes();
            if (viewModel.Frequencies == null) viewModel.Frequencies = this.GetFrequencies();
            if (viewModel.FurnaceClasses == null) viewModel.FurnaceClasses = this.GetFurnaceClasses();

            return viewModel;
        }

        private void GetReferenceData(ScheduleEditViewModel viewModel)
        {
            viewModel.FurnaceClasses = GetFurnaceClasses();
            viewModel.Frequencies = GetFrequencies();
            viewModel.ScheduleTypes = GetScheduleTypes();
        }

        private IEnumerable<Common.FurnaceClassViewModel> GetFurnaceClasses()
        {
            var furnaceClasses = Mapper.Map<IEnumerable<Common.FurnaceClassViewModel>>(scheduleService.GetFurnaceClasses());
            return furnaceClasses.OrderBy(fc => fc.Name);
        }

        private IEnumerable<Common.FrequencyViewModel> GetFrequencies()
        {
            var frequencies = Mapper.Map<IEnumerable<Common.FrequencyViewModel>>(scheduleService.GetFrequencies());
            return frequencies.OrderBy(f => f.DurationPosition);
        }

        private IEnumerable<Common.ScheduleTypeViewModel> GetScheduleTypes()
        {
            var scheduleTypes = Mapper.Map<IEnumerable<Common.ScheduleTypeViewModel>>(scheduleService.GetScheduleTypes());
            return scheduleTypes.OrderBy(st => st.Name);
        }
    }
}