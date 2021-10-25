namespace EOS2.Web.Areas.Organizations.Builders.Customer
{
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model.Enums;

    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Builders;

    public class CustomersViewModelBuilder : IViewModelBuilder<CustomerIndexViewModel>
    {
        private readonly IOrganizationsService organizationsService;

        public CustomersViewModelBuilder(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        public CustomerIndexViewModel Build()
        {
            return new CustomerIndexViewModel
            {
                Organizations = organizationsService.GetAllOrganizationsOfType(OrganizationType.Customer),
                OrganizationType = OrganizationType.Customer
            };
        }
    }
}