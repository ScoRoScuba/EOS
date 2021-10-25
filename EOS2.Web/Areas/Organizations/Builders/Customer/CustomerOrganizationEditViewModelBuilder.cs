namespace EOS2.Web.Areas.Organizations.Builders.Customer
{
    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Builders;

    public class CustomerOrganizationEditViewModelBuilder : IEditViewModelBuilder<CustomerEditViewModel>
    {
        private readonly IOrganizationsService organizationsService;

        public CustomerOrganizationEditViewModelBuilder(IOrganizationsService organizationsService)
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