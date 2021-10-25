namespace EOS2.Web.Code
{
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;

    using EOS2.Web.Filters;

    [ViewModel]
    ////[SessionExpireFilter]
    public class BaseController : Controller
    {
        public void SetActionMessage(string key, string message)
        {
            TempData[key] = message; 
        }
    }
}