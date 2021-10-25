namespace EOS2.Web
{
    using System;
    using System.IdentityModel.Claims;
    using System.IdentityModel.Services;
    using System.IdentityModel.Tokens;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using EOS2.Infrastructure.Interfaces.Services;

    public class Global : HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            LoggingConfig.Initialize();

            var logger = DependencyResolver.Current.GetService<ILoggerService>();
            logger.Log("Web Site Start Up");

            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();

            InternationalizationConfig.Initialize();

            BinderConfig.RegisterBinders();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;                        
        }

        public void Application_End(object sender, EventArgs e)
        {
            var logger = DependencyResolver.Current.GetService<ILoggerService>();
            logger.Log("Application Shut Down");        
        }

        public void Application_OnError()
        {
            var logger = DependencyResolver.Current.GetService<ILoggerService>();

            var ex = Context.Error;
            logger.LogFatal("Application_OnError", ex);

            if (ex is SecurityTokenException)
            {
                Context.ClearError();
                if (FederatedAuthentication.SessionAuthenticationModule != null)
                {
                    FederatedAuthentication.SessionAuthenticationModule.SignOut();
                }

                Response.Redirect("~/");
            }
        }
    }
}