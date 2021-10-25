namespace EOS2.Web
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            if (routes == null) throw new ArgumentNullException("routes");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Opps",
                url: "Opps/{action}",
                defaults: new { controller = "Error", action = "ServerError" },
                namespaces: new[] { "EOS2.Web.Controllers" });

            routes.MapRoute(
                name: "NotFound",
                url: "NotFound/{action}",
                defaults: new { controller = "Error", action = "NotFound" },
                namespaces: new[] { "EOS2.Web.Controllers" });

            routes.MapRoute(
                name: "Security",
                url: "Security/{action}",
                defaults: new { controller = "Security", action = "Login" },
                namespaces: new[] { "EOS2.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "EOS2.Web.Controllers" });
        }
    }
}
