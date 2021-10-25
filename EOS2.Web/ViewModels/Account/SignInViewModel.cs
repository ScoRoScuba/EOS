namespace EOS2.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class SignInViewModel : BaseViewModel
    {
        [Display(Name = "[[[User Name]]]")]
        [Required(ErrorMessage = "[[[Please enter your User Name]]]")]
        public string UserName { get; set; }

        [Display(Name = "[[[Password]]]")]
        [Required(ErrorMessage = "[[[Please enter a password]]]")]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "This value is passed on query string from external source and is only used within controller")]
        public string ReturnUrl { get; set; }
    }
}