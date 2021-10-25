namespace EOS2.Web.Areas.Organizations.ViewModels.Shared
{
    using System.ComponentModel.DataAnnotations;

    public class Schedule
    {
        public int Id { get; set; }

        [Display(Name = "[[[Name]]]")]
        public string Name { get; set; }

        [Display(Name = "[[[Furnace Class]]]")]
        public FurnaceClass FurnaceClass { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between ScheduleType & object.GetType()")]
        [Display(Name = "[[[Type]]]")]
        public ScheduleType Type { get; set; }

        [Display(Name = "[[[Frequency]]]")]
        public Frequency Frequency { get; set; }
    }
}