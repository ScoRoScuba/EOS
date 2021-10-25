namespace EOS2.Web.Filters
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;

    using EOS2.Common.Exceptions;
    using EOS2.Infrastructure.DependencyInjection;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Attributes;
    using EOS2.Web.Extensions;

    using Microsoft.Practices.Unity;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class ActivityLoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            if (!SkipLogging(filterContext))
            {
                var actionDescriptor = filterContext.ActionDescriptor;
                var controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
                var actionName = actionDescriptor.ActionName;
                var userName = filterContext.HttpContext.User.Identity.Name;
                var timeStamp = filterContext.HttpContext.Timestamp;

                var routeDataString = filterContext.RouteData.ToLoggingString();

                var message = new StringBuilder();

                if (!string.IsNullOrEmpty(routeDataString))
                {
                    message.Append(routeDataString);
                }

                var logger = UnityConfig.ConfiguredContainer.Resolve<ILoggerService>();
                if (logger == null)
                    throw new DependencyResolutionException(
                        typeof(ILoggerService),
                        "logger");

                logger.Log(
                    controllerName,
                    actionName,
                    userName,
                    timeStamp,
                    message.ToString());
            }

            base.OnActionExecuting(filterContext);
        }

        private static ControllerDescriptor GetControllerDescriptor(ControllerBase controllerBase)
        {
            var controllerType = controllerBase.GetType();
            return new ReflectedControllerDescriptor(controllerType);
        }

        private static bool SkipLogging(ActionExecutingContext actionContext)
        {
            Contract.Assert(actionContext != null);

            var controllerDescriptor = GetControllerDescriptor(actionContext.Controller);

            return actionContext.ActionDescriptor.GetCustomAttributes(typeof(NoActivityLoggingAttribute), true).Any()
                   || controllerDescriptor.GetCustomAttributes(typeof(NoActivityLoggingAttribute), true).Any();
        }
    }
}