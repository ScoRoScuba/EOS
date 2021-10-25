namespace EOS2.Web.Controllers
{
    using System.Web.Mvc;

    using EOS2.Web.Code;

    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}