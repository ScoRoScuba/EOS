namespace EOS2.Web.Code
{
    using System;

    using EOS2.Infrastructure.Interfaces.Services;

    public class LoggingBaseController : BaseController
    {
        private readonly ILoggerService logger;

        public LoggingBaseController(ILoggerService logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");

            this.logger = logger;
        }

        protected ILoggerService Logger
        {
            get
            {
                return logger;
            }
        }

        protected void LogInfo(string message)
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            Logger.Log(
                controllerName,
                actionName,
                ControllerContext.HttpContext.User.Identity.Name,
                ControllerContext.HttpContext.Timestamp,
                message);
        }
    }
}