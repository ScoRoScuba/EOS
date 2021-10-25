namespace EOS2.Web.Areas.Organizations.Builders.ServiceProviders
{
    using EOS2.Web.Builders;

    public class ServiceProvidersForPortalAgentCriteria : IBuilderCriteria
    {
        public ServiceProvidersForPortalAgentCriteria(int portalAgentId)
        {
            PortalAgentId = portalAgentId;
        }
        
        public int PortalAgentId { get; private set; }
    }
}