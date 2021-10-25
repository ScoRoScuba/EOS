using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(EOS2.Web.IdentityServicesConfig))]

namespace EOS2.Web
{
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;

    public class IdentityServicesConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Called by OWIN")]
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "Cookies"
                });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "TempState",
                AuthenticationMode = AuthenticationMode.Passive
            });    
        
            ////app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            ////{
            ////    Authority = "https://identity.thinktecture.com",
            ////    RequiredScopes = new[] { "api1", "api2" }
            ////});
        }
   }
}
