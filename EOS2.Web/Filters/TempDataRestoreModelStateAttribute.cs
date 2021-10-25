namespace EOS2.Web.Filters
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class TempDataRestoreModelStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            base.OnActionExecuting(filterContext);
            if (filterContext.Controller.TempData.ContainsKey("ModelState"))
            {
                filterContext.Controller.ViewData.ModelState.Merge((ModelStateDictionary)filterContext.Controller.TempData["ModelState"]);
            }
        }
    }
}