namespace EOS2.Web.Areas.Organizations.ViewModels.Customers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EOS2.Model;
    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels;

    [UniqueOrganization]
    public class CustomerEditViewModel : BaseViewModel
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "[[[Please enter the name for your company]]]")]
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Company]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "[[[Address]]]", Prompt = "[[[Company address]]]")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "[[[Please enter the ZIP Code of your main site]]]")]
        [Display(Name = "[[[ZIP Code]]]")]
        [DataType(DataType.Text)]
        public string PostalCode { get; set; }

        public IEnumerable<Site> Sites { get; set; }
    }
}