namespace EOS2.Web.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Elmah;

    using ApplicationException = System.ApplicationException;

    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();        
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;

            return View();
        }

        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;

            return View();        
        }

        public ActionResult BadRequest()
        {
            Response.StatusCode = 400;
            Response.TrySkipIisCustomErrors = true;

            return View();                
        }
    }
}