namespace EOS2.Web.Controllers
{
    using System.Web.Mvc;

    using EOS2.Web.Attributes;
    using EOS2.Web.Code;
    using EOS2.Web.ViewModels.Home;

    [IgnoreSessionExpired]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            var indexViewModel = new IndexViewModel();

            return View(indexViewModel);
        }
    }
}