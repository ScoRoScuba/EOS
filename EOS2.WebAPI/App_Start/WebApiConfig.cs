namespace EOS2.WebAPI
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Microsoft.Owin.Security.OAuth;
    using Microsoft.Practices.Unity.WebApi;

    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            // Web API configuration and services
            var config = new HttpConfiguration();

            try
            {
                // TODO: Remove this if we don't want to support XML (Is there a reason we wouldnt??)
                // config.Formatters.Remove(config.Formatters.XmlFormatter);

                // Web API routes
                config.MapHttpAttributeRoutes();

                config.EnableCors(new EnableCorsAttribute("https://localhost:44302", "accept, authorization", "GET"));

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional });

                config.Routes.MapHttpRoute(
                    name: "Default",
                    routeTemplate: "{controller}/{action}/{id}",
                    defaults: new { controller = "Help", action = "Index", id = RouteParameter.Optional });

                config.DependencyResolver = new UnityDependencyResolver(Infrastructure.DependencyInjection.UnityConfig.ConfiguredContainer);
            }
            catch (Exception)
            {
                config.Dispose();
                throw;
            }

            return config;
        }

        public static void Register2(HttpConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }

        public static void Register3(HttpConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            ////config.SuppressDefaultHostAuthentication();
            ////config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            
            // Web API configuration and services
           // var config = new HttpConfiguration();

            // TODO: Remove this if we want to support XML (Is there a reason we wouldnt??)
            // config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.EnableCors(new EnableCorsAttribute("https://localhost:44302", "accept, authorization", "GET"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "VersionedApi",
                routeTemplate: "api/{namespace}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            ////config.Routes.MapHttpRoute(
            ////    name: "Default",
            ////    routeTemplate: "{controller}/{action}/{id}",
            ////    defaults: new { controller = "Help", action = "Index", id = RouteParameter.Optional });

            config.DependencyResolver = new UnityDependencyResolver(Infrastructure.DependencyInjection.UnityConfig.ConfiguredContainer);

            return;
        }    
    }
}