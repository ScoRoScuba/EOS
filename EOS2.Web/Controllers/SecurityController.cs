namespace EOS2.Web.Controllers
{
    using System.Web.Mvc;

    using EOS2.Web.Attributes;
    using EOS2.Web.Code;
    using EOS2.Web.ViewModels.Security;

    [AllowAnonymous]
    [IgnoreSessionExpired]
    public class SecurityController : BaseController
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "This value is passed on query string from external source and is only used within controller")]
        public ActionResult AccessDenied(string returnUrl)
        {
            var viewModel = new AccessDeniedViewModel
                                {
                                    ReturnUrl = returnUrl
                                };

            return View(viewModel);
        }
    }
}