namespace EOS2.Web.Filters
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class RequireHttpsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");                

            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                if (filterContext.HttpContext.Request.Url != null)
                {
                    filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.Url.ToString().Replace("http:", "https:"));
                }

                filterContext.Result.ExecuteResult(filterContext);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}