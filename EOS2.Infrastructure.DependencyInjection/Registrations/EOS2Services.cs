namespace EOS2.Infrastructure.DependencyInjection.Registrations
{
    using EOS2.Identity.Services;
    using EOS2.Infrastructure.Interfaces.Security;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Security.OAuth2;
    using EOS2.Services.Authentication;
    using EOS2.Services.BusinessDomain;

    using Microsoft.Practices.Unity;

    public static class EOS2Services
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IdentityRoleService, IdentityRoleService>();
            container.RegisterType<IdentityUserService, IdentityUserService>();
                        
            // EOS2Services
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IOAuth2Client, EOSOAuth2Client>();

            container.RegisterType<IClaimsBuilderService, ClaimsBuilderService>();
            container.RegisterType<IOrganizationsService, OrganizationsService>();
            container.RegisterType<ISiteService, SiteService>();
            container.RegisterType<IPlantAreaService, PlantAreaService>();
            container.RegisterType<IEquipmentService, EquipmentService>();
            container.RegisterType<IInstrumentService, InstrumentService>();
            container.RegisterType<IScheduleService, ScheduleService>();
            container.RegisterType<IChannelService, ChannelService>();
            container.RegisterType<ICertificateService, CertificateService>();
            container.RegisterType<IReferenceDataService, ReferenceDataService>();
        }
    }
}
