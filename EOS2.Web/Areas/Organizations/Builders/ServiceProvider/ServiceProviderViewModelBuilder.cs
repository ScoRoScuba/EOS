namespace EOS2.Web.Areas.Organizations.Builders.ServiceProviders
{
    using System;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model.Enums;
    using EOS2.Web.Areas.Organizations.ViewModels.ServiceProviders;
    using EOS2.Web.Builders;

    public class ServiceProviderViewModelBuilder : IViewModelBuilder<ServiceProviderIndexViewModel>, IViewModelWithQueryBuilder<ServiceProviderIndexViewModel>
    {
        private readonly IOrganizationsService organizationsService;

        public ServiceProviderViewModelBuilder(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        public ServiceProviderIndexViewModel Build()
        {
            return new ServiceProviderIndexViewModel
                       {
                           Organizations = organizationsService.GetAllOrganizationsOfType(OrganizationType.ServiceProvider),
                           OrganizationType = OrganizationType.ServiceProvider
                       };
        }

        public ServiceProviderIndexViewModel Build(IBuilderCriteria criteria)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");

            var portalAgentCriteria = criteria as ServiceProvidersForPortalAgentCriteria;
            if (portalAgentCriteria == null) throw new InvalidCastException("[[[wrong criteria type]]]");

            return new ServiceProviderIndexViewModel
                       {
                           Organizations = organizationsService.GetAllOrganizationsOfType(OrganizationType.ServiceProvider, portalAgentCriteria.PortalAgentId),
                           OrganizationType = OrganizationType.ServiceProvider
                       };
        }
    }
}