[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EOS2.Web.UnityWebDependencies), "RegisterWebTypes", Order = 10)]

namespace EOS2.Web
{
    using EOS2.Identity.Model;
    using EOS2.Infrastructure.DependencyInjection;
    using EOS2.Infrastructure.DependencyInjection.Lifetime;
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.Builders.Customer;
    using EOS2.Web.Areas.Organizations.Builders.Equipment;
    using EOS2.Web.Areas.Organizations.Builders.EquipmentChannels;
    using EOS2.Web.Areas.Organizations.Builders.Instrument;
    using EOS2.Web.Areas.Organizations.Builders.InstrumentChannels;
    using EOS2.Web.Areas.Organizations.Builders.PlantArea;
    using EOS2.Web.Areas.Organizations.Builders.PortalAgents;
    using EOS2.Web.Areas.Organizations.Builders.Schedule;
    using EOS2.Web.Areas.Organizations.Builders.ServiceProviders;
    using EOS2.Web.Areas.Organizations.Builders.Site;
    using EOS2.Web.Areas.Organizations.Builders.Users;
    using EOS2.Web.Areas.Organizations.ViewModels.Certificate;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Areas.Organizations.ViewModels.EquipmentChannels;
    using EOS2.Web.Areas.Organizations.ViewModels.Equipments;
    using EOS2.Web.Areas.Organizations.ViewModels.InstrumentChannels;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.Areas.Organizations.ViewModels.PlantAreas;
    using EOS2.Web.Areas.Organizations.ViewModels.PortalAgents;
    using EOS2.Web.Areas.Organizations.ViewModels.Schedules;
    using EOS2.Web.Areas.Organizations.ViewModels.ServiceProviders;
    using EOS2.Web.Areas.Organizations.ViewModels.Site;
    using EOS2.Web.Areas.Organizations.ViewModels.Users;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;

    using Microsoft.Practices.Unity;

    public static class UnityWebDependencies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "To be left in one place with a view to splitting out at a later date if required")]
        public static void RegisterWebTypes()
        {
            var container = UnityConfig.ConfiguredContainer;

            if (container == null) return;
 
            container.RegisterType<IUserAppSession, UserAppSession>(new UnityPerSessionLifetimeManager());

            // Customer ViewModels
            container.RegisterType<IViewModelBuilder<CustomerIndexViewModel>, CustomersViewModelBuilder>();

            // Customer EditViewModels
            container.RegisterType<IEditViewModelBuilder<CustomerEditViewModel>, CustomerOrganizationEditViewModelBuilder>();
            container.RegisterType<IEditViewPartialModelBuilder<CustomerSiteEditViewModel>, SiteEditViewModelBuilder>();
            container.RegisterType<IEditViewPartialModelBuilder<PlantAreaEditViewModel>, PlantAreaEditViewModelBuilder>();
            container.RegisterType<IEditViewPartialModelBuilder<EquipmentEditViewModel>, EquipmentEditViewModelBuilder>();
            container.RegisterType<IEditViewPartialModelBuilder<InstrumentEditViewModel>, InstrumentEditViewModelBuilder>();
            container.RegisterType<IEditViewPartialModelBuilder<ScheduleEditViewModel>, ScheduleEditViewModelBuilder>();
            container.RegisterType<IEditViewPartialModelBuilder<CertificateEditViewModel>, CertificateEditViewModelBuilder>();
            container.RegisterType<IViewModelWithQueryBuilder<CertificateEditViewModel>, CertificateEditViewModelBuilder>();

            // Customer DomainModels
            container.RegisterType<IDomainModelBuilder<Organization, CustomerEditViewModel>, CustomerOrganizationEditDomainModelBuilder>();
            container.RegisterType<IDomainModelBuilder<Site, CustomerSiteEditViewModel>, SiteEditDomainModelBuilder>();
            container.RegisterType<IDomainModelBuilder<PlantArea, PlantAreaEditViewModel>, PlantAreaEditDomainModelBuilder>();
            container.RegisterType<IDomainModelBuilder<Equipment, EquipmentEditViewModel>, EquipmentEditDomainModelBuilder>();
            container.RegisterType<IDomainModelBuilder<Instrument, InstrumentEditViewModel>, InstrumentEditDomainModelBuilder>();
            container.RegisterType<IDomainModelBuilder<Schedule, ScheduleEditViewModel>, ScheduleEditDomainModelBuilder>();
            container.RegisterType<IDomainModelBuilder<CertificateHeader, CertificateEditViewModel>, CertificateEditDomainModelBuilder>();

            container.RegisterType<IViewModelBuilder<EquipmentViewModel>, EquipmentViewModelBuilder>();

            // Portal-Agent ViewModels
            container.RegisterType<IViewModelBuilder<PortalAgentIndexViewModel>, PortalAgentsViewModelBuilder>();

            // Portal-Agent EditViewModels
            container.RegisterType<IEditViewModelBuilder<PortalAgentEditViewModel>, PortalAgentOrganizationViewModelBuilder>();

            // Portal-Agent DomainModels
            container.RegisterType<IDomainModelBuilder<Organization, PortalAgentEditViewModel>, PortalAgentEditDomainModelBuilder>();

            // Service Provider ViewModels
            container.RegisterType<IViewModelBuilder<ServiceProviderIndexViewModel>, ServiceProviderViewModelBuilder>();            
            container.RegisterType<IViewModelWithQueryBuilder<ServiceProviderIndexViewModel>, ServiceProviderViewModelBuilder>();                        

            // Service Provider EditViewModels
            container.RegisterType<IEditViewModelBuilder<ServiceProviderEditViewModel>, ServiceProviderOrganizationViewModelBuilder>();

            // Service Provider DomainModels
            container.RegisterType<IDomainModelBuilder<Organization, ServiceProviderEditViewModel>, ServiceProviderEditDomainModelBuilder>();

            // Users
            container.RegisterType<IViewModelBuilder<UsersIndexViewModel>, UsersForOrganizationTypeListViewModelBuilder>();
            container.RegisterType<IDomainModelBuilder<User, UserEditViewModel>, UserForOrganizationEditDomainModelBuilder>();
            container.RegisterType<IEditViewModelBuilder<UserEditViewModel>, UserForOrganizationViewModelBuilder>();
            container.RegisterType<IEditViewModelBuilder<UserPasswordViewModel>, PasswordForUserViewModelBuilder>();

            container.RegisterType<IViewModelWithQueryBuilder<UsersIndexViewModel>, UsersForOrganizationTypeListViewModelBuilder>();

            // Channels 
            container.RegisterType<IEditViewPartialModelBuilder<ChannelsViewModel>, InstrumentsChannelsViewModel>();
            container.RegisterType<IDomainModelBuilder<Channel, ChannelViewModel>, ChannelEditDomainModelBuilder>();

            container.RegisterType<IEditViewModelBuilder<AvailableInstrumentsViewModel>, AvailableInstrumentsInPlantAreaViewModelBuilder>();

            container.RegisterType<IViewModelWithQueryBuilder<UnallocatedChannelsForInstrumentCriteria, EquipmentChannelsViewModel>, UnattachedChannelsViewModelBuilder>();
           
            container.RegisterType<IViewModelWithQueryBuilder<ChannelsAllocatedToEquipmentCriteria, EquipmentChannelsViewModel>, InstrumentChannelsAllocatedToEquipmentBuilder>();
        }
    }
}