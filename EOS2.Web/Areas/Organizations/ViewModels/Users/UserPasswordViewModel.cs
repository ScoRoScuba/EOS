namespace EOS2.Web.Areas.Organizations.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using EOS2.Web.Attributes;
    using EOS2.Web.ViewModels;

    public class UserPasswordViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "[[[Please enter a password]]]")]
        [Display(Name = "[[[Password]]]", Prompt = "[[[Users Logon Password]]]")]
        [StringLengthI18n(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "[[[Please enter a confirmation password]]]")]
        [Display(Name = "[[[Confirm Password]]]", Prompt = "[[[Users Logon Password]]]")]
        [StringLengthI18n(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "[[[Passwords do not match]]]")]
        public string ConfirmationPassword { get; set; }
    }
}