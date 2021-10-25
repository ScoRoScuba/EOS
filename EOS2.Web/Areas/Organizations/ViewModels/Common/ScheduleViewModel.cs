namespace EOS2.Web.Areas.Organizations.ViewModels.Common
{
    using System.ComponentModel.DataAnnotations;

    public class ScheduleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "[[[Name]]]")]
        public string Name { get; set; }

        [Display(Name = "[[[Furnace Class]]]")]
        public FurnaceClassViewModel FurnaceClass { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between ScheduleType & object.GetType()")]
        [Display(Name = "[[[Type]]]")]
        public ScheduleTypeViewModel Type { get; set; }

        [Display(Name = "[[[Frequency]]]")]
        public FrequencyViewModel Frequency { get; set; }
    }
}