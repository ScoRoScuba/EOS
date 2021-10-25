namespace EOS2.Services.BusinessDomain
{
    using System;
    using System.Reflection;
    using System.Text;

    using EOS2.Infrastructure.Interfaces.Services;

    using log4net;

    public class LoggerService : ILoggerService
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Log(string message)
        {
            Logger.Info(message);
        }

        public void Log(string controllerName, string controllerAction, string userName, DateTime timestamp, string message)
        {
            var logMessage = new StringBuilder();
            logMessage.AppendFormat("UserName={0}|", userName);
            logMessage.AppendFormat("Controller={0}|", controllerName);
            logMessage.AppendFormat("Action={0}|", controllerAction);
            logMessage.AppendFormat("TimeStamp={0}|", timestamp);

            logMessage.Append(message);

            this.Log(logMessage.ToString());
        }

        public void LogFatal(string message, Exception exception)
        {
            Logger.Fatal(message, exception);
        }
    }
}
