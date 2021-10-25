using Microsoft.Owin;

[assembly: OwinStartup(typeof(EOS2.IdentityServer.Configuration.IdentityServicesConfiguration))]

namespace EOS2.IdentityServer.Configuration
{    
    using EOS2.Infrastructure.Security;
    using EOS2.Infrastructure.Security.Configuration;

    using Owin;
    
    using Thinktecture.IdentityServer.Core.Configuration;
    using Thinktecture.IdentityServer.Core.Logging;
    using Thinktecture.IdentityServer.Core.Logging.LogProviders;

    public class IdentityServicesConfiguration
    {
        public void Configuration(IAppBuilder app)
        {
            // here we log using Log 4 Net.  Due to Elmah not working correctly in OWIN
            // we are logging to the windows event log
            LogProvider.SetCurrentLogProvider(new Log4NetLogProvider());

            var identityServerOptions = new IdentityServerOptions
                                            {
                                                SiteName = IdentityServerConfigurationSectionManager.Configuration.SiteName.Value,

                                                IssuerUri = IdentityServerConfigurationSectionManager.Configuration.IssuerUri.Value,

                                                Factory = IdentityServerServiceFactoryBuilder.Create(),                                              

                                                SigningCertificate = CertificatesManager.LoadCertificateByThumbprint(IdentityServerConfigurationSectionManager.Configuration.CertThumbprint.Value),

                                                CorsPolicy = CorsPolicy.AllowAll,

                                                LoggingOptions = new LoggingOptions
                                                                        {
                                                                        EnableHttpLogging = true,
                                                                        EnableWebApiDiagnostics = true,
                                                                        IncludeSensitiveDataInLogs = true
                                                                        },
                                                EventsOptions = new EventsOptions
                                                                    {
                                                                        RaiseFailureEvents = true,
                                                                        RaiseInformationEvents = true,
                                                                        RaiseSuccessEvents = true,
                                                                        RaiseErrorEvents = true
                                                                    },
                                                RequireSsl = true
                                            };

            app.UseIdentityServer(identityServerOptions);
        }
    }
}