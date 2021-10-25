namespace EOS2.Web.Areas.Organizations.Builders.PortalAgents
{
    using System;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.PortalAgents;
    using EOS2.Web.Builders;

    public class PortalAgentEditDomainModelBuilder : IDomainModelBuilder<Organization, PortalAgentEditViewModel>
    {
        public Organization Build(PortalAgentEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            return new Organization
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Address = viewModel.Address,
                        PostalCode = viewModel.PostalCode
                    };
        }
    }
}