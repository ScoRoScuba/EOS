namespace EOS2.Web.Areas.Organizations.ViewModels.PlantAreas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels;

    [UniquePlantArea]
    public class PlantAreaEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string StoredName { get; set; }

        [Required(ErrorMessage = "[[[Please enter the name for your plant area]]]")]
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Plant Area]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "[[[Please enter the Description of your plant area]]]")]
        [Display(Name = "[[[Description]]]", Prompt = "[[[Description of Plant Area]]]")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public IEnumerable<EquipmentViewModel> Equipments { get; set; }

        public IEnumerable<InstrumentViewModel> Instruments { get; set; }
    }
}