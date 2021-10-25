namespace Eurotherm.Security.Tests.AreaRegistrations
{
    using System;
    using System.Web.Mvc;

    public class ServiceProviderAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ServiceProvider";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            context.MapRoute(
                "ServiceProvider_Customers_Site_Create_Equipment",
                "ServiceProvider/{controller}/{CustomerId}/{action}/{SiteId}/Equipment/Create",
                new
                    {
                        controller = "Customers",
                        action = "Site"
            });

            context.MapRoute(
                "ServiceProvider_default",
                "ServiceProvider/{controller}/{action}/{id}",
                new
                    {
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
            });
        }
    }
}