namespace EOS2.Web
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Cors;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Web API routes
////            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "returned value")]
        public static HttpConfiguration Register()
        {
            //// Web API configuration and services
            var config = new HttpConfiguration();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //// Web API configuration and services
////            config.Filters.Add(new Elmah.Contrib.WebApi.ElmahHandleErrorApiAttribute());

            //// Web API routes
            config.MapHttpAttributeRoutes();

////            config.EnableCors(new EnableCorsAttribute("http://localhost:21575", "accept, authorization", "GET"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            return config;
        }
    }
}
