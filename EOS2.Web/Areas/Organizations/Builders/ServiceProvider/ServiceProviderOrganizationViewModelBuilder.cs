namespace EOS2.Web.Areas.Organizations.Builders.ServiceProviders
{
    using System.Linq;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model.Enums;
    using EOS2.Web.Areas.Organizations.ViewModels.ServiceProviders;
    using EOS2.Web.Builders;

    public class ServiceProviderOrganizationViewModelBuilder : IEditViewModelBuilder<ServiceProviderEditViewModel>
    {
        private readonly IOrganizationsService organizationsService;

        public ServiceProviderOrganizationViewModelBuilder(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        public ServiceProviderEditViewModel Build(int? id)
        {
            var viewModel = new ServiceProviderEditViewModel();
            if (id.HasValue)
            {
                var serviceProvider = organizationsService.GetServiceProviderOrganization(id.Value);

                var serviceProviderRole = serviceProvider.OrganizationRole.FirstOrDefault(r => r.OrganizationType == OrganizationType.ServiceProvider);
            
                viewModel = new ServiceProviderEditViewModel
                           {
                               Id = serviceProvider.Id,
                               Address = serviceProvider.Address,
                               PostalCode = serviceProvider.PostalCode,
                               Name = serviceProvider.Name,
                               PortalAgentId = serviceProviderRole == null ? null : serviceProviderRole.ParentOrganizationId
                           };
            }

            return viewModel;
        }
    }
}