namespace EOS2.WebAPI.Areas.Help
{
    using System;
    using System.Web.Http;
    using System.Web.Mvc;

    public class HelpAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Help";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            context.MapRoute(
                "Help_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}