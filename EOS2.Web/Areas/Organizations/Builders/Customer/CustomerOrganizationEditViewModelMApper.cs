namespace EOS2.Web.Areas.Organizations.Mappers.Customer
{
    using System.Collections.Generic;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Mappers;

    public class CustomerOrganizationEditViewModelMapper : IEditViewModelMapper<CustomerEditViewModel>
    {
        private readonly IOrganizationsService organizationsService;

        public CustomerOrganizationEditViewModelMapper(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        public CustomerEditViewModel Build(int id)
        {
            var result = organizationsService.GetCustomerOrganization(id);

            return new CustomerEditViewModel()
                        {
                            Id = result.Id,
                            Name = result.Name,
                            Address = result.Address,
                            PostalCode = result.PostalCode,
                            Sites = result.Sites ?? new List<Site>()
                        };
        }
    }
}