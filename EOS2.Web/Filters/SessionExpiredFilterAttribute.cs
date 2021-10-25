namespace EOS2.Web.Filters
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Web.Attributes;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            var ctx = HttpContext.Current;

            if (!SkipExpirationCheck(filterContext))
            {
                // check if session is supported
                if (ctx.Session != null)
                {
                    // check if a new session id was generated
                    if (ctx.Session.IsNewSession)
                    {
                        // If it says it is a new session, but an existing cookie exists, then it must
                        // have timed out
                        var sessionCookie = ctx.Request.Headers["Cookie"];

                        if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId", StringComparison.Ordinal) >= 0))
                        {
                            var redirectResult = new RedirectResult("~/Account/SessionExpired");
                            filterContext.Result = redirectResult;
                        }
                    }
                    else
                    {    
                        var userAppSession = DependencyResolver.Current.GetService<IUserAppSession>();

                        if (userAppSession == null || userAppSession.CurrentUser == null)
                        {
                            var redirectResult = new RedirectResult("~/Account/SessionExpired");
                            filterContext.Result = redirectResult;                            
                        }
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private static bool SkipExpirationCheck(ActionExecutingContext actionContext)
        {
            Contract.Assert(actionContext != null);

            var controllerDescriptor = GetControllerDescriptor(actionContext.Controller);

            return actionContext.ActionDescriptor.GetCustomAttributes(typeof(IgnoreSessionExpiredAttribute), true).Any()
                   || controllerDescriptor.GetCustomAttributes(typeof(IgnoreSessionExpiredAttribute), true).Any();
        }

        private static ControllerDescriptor GetControllerDescriptor(ControllerBase controllerBase)
        {
            var controllerType = controllerBase.GetType();
            return new ReflectedControllerDescriptor(controllerType);
        }
    }
}