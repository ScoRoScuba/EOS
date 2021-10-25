namespace EOS2.Web.Areas.Organizations.Builders.PortalAgents
{
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model.Enums;
    using EOS2.Web.Areas.Organizations.ViewModels.PortalAgents;
    using EOS2.Web.Builders;

    public class PortalAgentsViewModelBuilder : IViewModelBuilder<PortalAgentIndexViewModel>
    {
        private readonly IOrganizationsService organizationsService;

        public PortalAgentsViewModelBuilder(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        public PortalAgentIndexViewModel Build()
        {
            return new PortalAgentIndexViewModel
                       {
                           Organizations =
                               organizationsService.GetAllOrganizationsOfType(
                                   OrganizationType.PortalAgent),
                            OrganizationType = OrganizationType.PortalAgent
                       };
        }
    }
}