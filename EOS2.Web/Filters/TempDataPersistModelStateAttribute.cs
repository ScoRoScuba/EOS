namespace EOS2.Web.Attributes.Filters
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class TempDataPersistModelStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            base.OnActionExecuted(filterContext);         
            filterContext.Controller.TempData["ModelState"] = filterContext.Controller.ViewData.ModelState;
        }
    }    
}
