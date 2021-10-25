namespace EOS2.Web.Areas.Organizations.ViewModels.Schedule
{
    using System.Collections.Generic;

    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.ViewModels;

    public class EquipmentScheduleViewModel : BaseViewModel
    {
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
    }
}