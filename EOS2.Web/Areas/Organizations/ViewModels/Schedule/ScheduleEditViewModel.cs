namespace EOS2.Web.Areas.Organizations.ViewModels.Schedules
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels;

    [UniqueSchedule]
    public class ScheduleEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string StoredName { get; set; }

        [Required(ErrorMessage = "[[[Please enter the name for your schedule]]]")]
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Schedule]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "[[[Furnace Class]]]")]
        public FurnaceClassViewModel FurnaceClass { get; set; }

        [Display(Name = "[[[Description]]]", Prompt = "[[[Description of Schedule]]]")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "[[[Frequency]]]", Prompt = "[[[Frequency of Schedule]]]")]
        public FrequencyViewModel Frequency { get; set; }

        [Display(Name = "[[[Special Conditions]]]", Prompt = "[[[Special Conditions of Schedule]]]")]
        [DataType(DataType.MultilineText)]
        public string SpecialConditions { get; set; }

        [Required(ErrorMessage = "[[[Please select a Type]]]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between ScheduleType & object.GetType()")]
        [Display(Name = "[[[Schedule Type]]]", Prompt = "[[[Type of Schedule]]]")]
        public ScheduleTypeViewModel Type { get; set; }

        // Reference Data
        public IEnumerable<ScheduleTypeViewModel> ScheduleTypes { get; set; }

        public IEnumerable<FurnaceClassViewModel> FurnaceClasses { get; set; }

        public IEnumerable<FrequencyViewModel> Frequencies { get; set; }
    }
}