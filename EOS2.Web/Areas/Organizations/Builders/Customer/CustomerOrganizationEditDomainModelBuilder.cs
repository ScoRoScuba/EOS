namespace EOS2.Web.Areas.Organizations.Builders.Customer
{
    using System;

    using AutoMapper;

    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Builders;

    public class CustomerOrganizationEditDomainModelBuilder : IDomainModelBuilder<Organization, CustomerEditViewModel>
    {
        public Organization Build(CustomerEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            return Mapper.Map<Organization>(viewModel);
        }
    }
}