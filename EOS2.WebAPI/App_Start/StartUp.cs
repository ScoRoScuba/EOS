using Microsoft.Owin;

[assembly: OwinStartup(typeof(EOS2.WebAPI.Startup))]

namespace EOS2.WebAPI
{
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Owin;

    using Thinktecture.IdentityServer.AccessTokenValidation;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Owin standard name.")]
    public class Startup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "No!"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "app", Justification = "Parameter is required.")]
        public void Configuration(IAppBuilder app)
        {
            ////TODO : Commented out the below untill thinktecture bits ready
            ////JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            ////app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            ////    {
            ////    Authority = "https://localhost:44302",    // the serve rthat issues the token 
            ////        RequiredScopes = new[] { "EOSAPI2" }      // the Scopes needed to access this site or resource
            ////   });

            ////app.UseWebApi(WebApiConfig.Register());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register3);
            ////app.UseWebApi(WebApiConfig.Register());
            ////RouteConfig.RegisterRoutes(RouteTable.Routes);
            MapperConfig.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(GlobalConfiguration.Configuration));
        }
    }
}