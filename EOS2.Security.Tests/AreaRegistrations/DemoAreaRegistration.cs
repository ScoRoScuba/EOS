namespace Eurotherm.Security.Tests.AreaRegistrations
{
    using System;
    using System.Web.Mvc;

    public class DemoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Demo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            context.MapRoute(
                "Customers_default",
                "Customers/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}