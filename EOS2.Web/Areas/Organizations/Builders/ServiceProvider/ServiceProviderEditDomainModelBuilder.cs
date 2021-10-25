namespace EOS2.Web.Areas.Organizations.Builders.ServiceProviders
{
    using System;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.ServiceProviders;
    using EOS2.Web.Builders;

    public class ServiceProviderEditDomainModelBuilder : IDomainModelBuilder<Organization, ServiceProviderEditViewModel>
    {
        public Organization Build(ServiceProviderEditViewModel viewModel)
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