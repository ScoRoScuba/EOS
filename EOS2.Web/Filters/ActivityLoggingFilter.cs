namespace EOS2.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using EOS2.Identity.Model;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.IoC;

    using log4net;

    using Microsoft.Practices.Unity;

    public class ActivityLoggingFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext == null) throw new ArgumentNullException("actionContext");

            var actionDescriptor = actionContext.ActionDescriptor;
            string controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = actionDescriptor.ActionName;
            string userName = actionContext.HttpContext.User.Identity.Name;
            DateTime timeStamp = actionContext.HttpContext.Timestamp;

            string routeId = string.Empty;

            // TODO : we need to be more specific in here
            if (actionContext.RouteData.Values["id"] != null)
            {
                routeId = actionContext.RouteData.Values["id"].ToString();
            }

            StringBuilder message = new StringBuilder();

            if (!string.IsNullOrEmpty(routeId))
            {
                message.Append("RouteId=");
                message.Append(routeId);
            }

            var logger = UnityConfig.GetConfiguredContainer().Resolve<ILoggerService>();
            logger.Log(controllerName, actionName, userName, timeStamp, message.ToString());

            base.OnActionExecuting(actionContext);
        }
    }
}