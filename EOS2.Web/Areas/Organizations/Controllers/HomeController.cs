namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System.Web.Mvc;
    using EOS2.Web.Code;

    public class HomeController : BaseController
    {
        // GET: Organizations/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}