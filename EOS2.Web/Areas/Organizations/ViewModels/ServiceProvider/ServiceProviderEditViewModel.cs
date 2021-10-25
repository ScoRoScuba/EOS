namespace EOS2.Web.Areas.Organizations.ViewModels.ServiceProviders
{
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels;

    [UniqueOrganization]
    public class ServiceProviderEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public int? PortalAgentId { get; set; }

        [Required(ErrorMessage = "[[[Please enter a name for the Service Provider]]]")]
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Service Provider]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "[[[Address]]]", Prompt = "[[[Service Provider address]]]")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "[[[Please enter the ZIP Code of your main site]]]")]
        [Display(Name = "[[[ZIP Code]]]")]
        [DataType(DataType.Text)]
        public string PostalCode { get; set; }
    }
}