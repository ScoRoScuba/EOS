namespace EOS2.Web.Areas.Organizations.Builders.Site
{
    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Builders;

    public class SitesViewModelBuilder : IEditViewModelBuilder<CustomerEditViewModel>
    {
         private readonly IOrganizationsService organizationsService;

        public SitesViewModelBuilder(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        public CustomerEditViewModel Build(int? id)
        {
            var viewModel = new CustomerEditViewModel();

            if (id.HasValue) viewModel = Mapper.Map<CustomerEditViewModel>(organizationsService.GetCustomerOrganization(id.Value));

            return viewModel;
        }
    }
}