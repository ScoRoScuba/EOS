namespace EOS2.Web
{
    using System;
    using System.Web.Mvc;

    using Elmah.Contrib.Mvc;

    using EOS2.Web.Attributes;
    using EOS2.Web.Filters;

    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            if (filters == null) throw new ArgumentNullException("filters");

            filters.Add(new EventLogExceptionLoggerAttribute());

            // we do this to make sure we show our Error page
            var errorAttribute = new ElmahHandleErrorAttribute
                                     {
                                        View = "/Views/Error/Error.cshtml"
                                     };

            filters.Add(errorAttribute);

            filters.Add(new ActivityLoggingFilterAttribute());

            filters.Add(new SessionExpireFilterAttribute());

            filters.Add(new EOSClaimsAuthorizeAttribute());        
        }
    }
}