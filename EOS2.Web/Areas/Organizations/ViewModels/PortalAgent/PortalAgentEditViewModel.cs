namespace EOS2.Web.Areas.Organizations.ViewModels.PortalAgents
{
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels;

    [UniqueOrganization]
    public class PortalAgentEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "[[[Please enter a name for the Portal Agent]]]")]
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of Portal Agent]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "[[[Address]]]", Prompt = "[[[Portal Agents address ]]]")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "[[[Please enter the ZIP Code of your main site]]]")]
        [Display(Name = "[[[ZIP Code]]]")]
        [DataType(DataType.Text)]
        public string PostalCode { get; set; }
    }
}