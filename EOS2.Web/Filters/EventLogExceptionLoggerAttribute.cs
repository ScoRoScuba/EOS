namespace EOS2.Web.Filters
{
    using System;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "It is convention to name filters with the word Attribute")]
    public class EventLogExceptionLoggerAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            var logger = DependencyResolver.Current.GetService<ILoggerService>();
            var ex = filterContext.Exception;

            logger.LogFatal("Application_OnError", ex);
        }
    }
}