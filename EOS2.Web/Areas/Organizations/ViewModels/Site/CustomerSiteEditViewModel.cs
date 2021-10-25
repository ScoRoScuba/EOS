namespace EOS2.Web.Areas.Organizations.ViewModels.Site
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels;

    [UniqueSite]
    public class CustomerSiteEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string StoredName { get; set; }

        [Required(ErrorMessage = "[[[Please enter the name for your site]]]")]
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of site]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "[[[Address]]]", Prompt = "[[[Company address]]]")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "[[[Please enter the ZIP Code of your site]]]")]
        [Display(Name = "[[[ZIP Code]]]")]
        [DataType(DataType.Text)]
        public string PostalCode { get; set; }

        public IEnumerable<Common.PlantAreaViewModel> PlantAreas { get; set; }
    }
}