namespace EOS2.Web.Areas.Organizations.Builders.PortalAgents
{
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.PortalAgents;
    using EOS2.Web.Builders;

    public class PortalAgentOrganizationViewModelBuilder : IEditViewModelBuilder<PortalAgentEditViewModel>
    {
        private readonly IOrganizationsService organizationsService;

        public PortalAgentOrganizationViewModelBuilder(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        public PortalAgentEditViewModel Build(int? id)
        {
            var viewModel = new PortalAgentEditViewModel();

            if (id.HasValue)
            {
                var portalAgent = organizationsService.GetPortalAgentOrganization(id.Value);
                viewModel = new PortalAgentEditViewModel
                       {
                           Id = portalAgent.Id,
                           Address = portalAgent.Address,
                           PostalCode = portalAgent.PostalCode,
                           Name = portalAgent.Name
                       };
            }

            return viewModel;
        }
    }
}