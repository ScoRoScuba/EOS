namespace EOS2.IdentityServer.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    using EOS2.Identity.Model;
    using EOS2.Identity.Repository;
    using EOS2.Identity.Services;
    using EOS2.Infrastructure.DependencyInjection;
    using EOS2.Infrastructure.Security.Configuration;

    using Microsoft.Practices.Unity;

    using Thinktecture.IdentityServer.AspNetIdentity;
    using Thinktecture.IdentityServer.Core.Configuration;
    using Thinktecture.IdentityServer.Core.Services;
    using Thinktecture.IdentityServer.EntityFramework;

    public static class IdentityServerServiceFactoryBuilder
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        public static IdentityServerServiceFactory Create()
        {
            var efConfig = new EntityFrameworkServiceOptions
            {                
                ConnectionString = IdentityServerConfigurationSectionManager.Configuration.ConnectionString.Value,  // this is the default
            };

            var factory = new IdentityServerServiceFactory();

            factory.RegisterConfigurationServices(efConfig);
            factory.RegisterOperationalServices(efConfig);
            
            var mgr  = UnityConfig.ConfiguredContainer.Resolve<IdentityUserService>(new ResolverOverride[0]);

            var userService  = new AspNetIdentityUserService<User, int>(mgr);
            factory.UserService = new Registration<IUserService>(resolver => userService);

            return factory;                
        }
    }
}