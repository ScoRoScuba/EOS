namespace EOS2.Infrastructure.Interfaces.Services
{
    using System;

    public interface ILoggerService
    {
        void Log(string message);

        void Log(string controllerName, string controllerAction, string userName, DateTime timestamp, string message);

        void LogFatal(string message, Exception exception);
    }
}
