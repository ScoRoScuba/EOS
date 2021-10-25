namespace EOS2.Web.Areas.Organizations.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.ViewModels;

    public class UserEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "[[[Please enter a User Name for the user]]]")]
        [Display(Name = "[[[User Name]]]", Prompt = "[[[Users Logon Name]]]")]
        [StringLength(60, ErrorMessage = "[[[Maximum Length is 60 Characters]]]")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "[[[Please enter a name for the user]]]")]
        [Display(Name = "[[[Name]]]", Prompt = "[[[Name of User]]]")]
        [StringLength(60, ErrorMessage = "[[[Maximum Length is 60 Characters]]]")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "[[[Middle Name/Initial]]]", Prompt = "[[[Users Middle Name or Intial/s]]]")]
        [StringLength(120, ErrorMessage = "[[[Maximum Length is 120 Characters]]]")]
        [DataType(DataType.Text)]
        public string MiddleName { get; set; }

        [Display(Name = "[[[Family Name]]]", Prompt = "[[[Family Name of the User]]]")]
        [StringLength(60, ErrorMessage = "[[[Maximum Length is 60 Characters]]]")]
        [DataType(DataType.Text)]
        public string FamilyName { get; set; }

        [Required(ErrorMessage = "[[[Please enter an email address]]]")]
        [Display(Name = "[[[Email Address]]]", Prompt = "[[[Users Email Address]]]")]        
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "[[[Please enter a valid email address]]]")]
        public string Email { get; set; }

        [Required(ErrorMessage = "[[[Please enter an email address]]]")]
        [Display(Name = "[[[Comparison Email Address]]]", Prompt = "[[[Compare Email Address]]]")]        
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "[[[Please enter a valid email address]]]")]
        [Compare("Email", ErrorMessage = "[[[Email addresses do not match]]]")]
        public string ConfirmationEmail { get; set; }

        [Display(Name = "[[[Phone Number]]]", Prompt = "[[[Users Phone Number]]]")]
        [StringLength(60, ErrorMessage = "[[[Maximum Length is 60 Characters]]]")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "[[[Please enter a valid phone number]]]")]
        public string PhoneNumber { get; set; }
    }
}