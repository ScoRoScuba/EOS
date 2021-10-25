namespace EOS2.Web.ViewModels.Security
{
    using Newtonsoft.Json.Linq;

    public class AccessDeniedViewModel : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "This value is passed on query string from external source and is only used within controller")]
        public string ReturnUrl { get; set; }
    }
}