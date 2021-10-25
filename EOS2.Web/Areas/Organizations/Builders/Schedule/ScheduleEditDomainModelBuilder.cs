namespace EOS2.Web.Areas.Organizations.Builders.Schedule
{
    using AutoMapper;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Schedules;
    using EOS2.Web.Builders;

    public class ScheduleEditDomainModelBuilder : IDomainModelBuilder<Schedule, ScheduleEditViewModel>
    {
        public Schedule Build(ScheduleEditViewModel viewModel)
        {
            return Mapper.Map<Schedule>(viewModel);
        }
    }
}