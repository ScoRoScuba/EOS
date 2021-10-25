namespace EOS2.Web.Areas.Organizations.ViewModels.Equipments
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Areas.Organizations.ViewModels.Schedule;
    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels;

    [UniqueEquipment]
    public class EquipmentEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string StoredName { get; set; }

        [Required(ErrorMessage = "[[[Please enter the name for your equipment]]]")]
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Equipment]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "[[[Description]]]", Prompt = "[[[Description of Equipment]]]")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "[[[Make]]]", Prompt = "[[[Make of Equipment]]]")]
        [DataType(DataType.Text)]
        public string Make { get; set; }

        [Display(Name = "[[[Model]]]", Prompt = "[[[Model of Equipment]]]")]
        [DataType(DataType.Text)]
        public string Model { get; set; }

        [Display(Name = "[[[Serial Number]]]", Prompt = "[[[Serial Number of Equipment]]]")]
        [DataType(DataType.Text)]
        public string SerialNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "There is no real confusion between EquipmentType & object.GetType()")]
        [Display(Name = "[[[Equipment Type]]]", Prompt = "[[[Type of Equipment]]]")]
        public EquipmentTypeViewModel Type { get; set; }

        [Display(Name = "[[[Notes]]]", Prompt = "[[[Any Notes for Equipment]]]")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        public IEnumerable<EquipmentTypeViewModel> EquipmentTypes { get; set; }

        public IEnumerable<InstrumentViewModel> AttachedInstruments { get; set; }

        public IEnumerable<InstrumentViewModel> AllInstruments { get; set; }

        public int AttachInstrumentId { get; set; }

        public EquipmentScheduleViewModel Schedules { get; set; }
    }
}