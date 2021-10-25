namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class CustomerController : BaseController
    {
        private readonly IOrganizationsService organizationsService;
        private readonly IViewModelBuilder<CustomerIndexViewModel> customersViewModelBuilder;
        private readonly IEditViewModelBuilder<CustomerEditViewModel> editCustomerOrganizationViewModelBuilder;
        private readonly IDomainModelBuilder<Organization, CustomerEditViewModel> editCustomerOrganizationDomainModelBuilder;
        private readonly IUserAppSession userSession;

        public CustomerController(
            IOrganizationsService organizationsService, 
            IViewModelBuilder<CustomerIndexViewModel> customersViewModelBuilder,
            IEditViewModelBuilder<CustomerEditViewModel> editCustomerOrganizationViewModelBuilder,
            IDomainModelBuilder<Organization, CustomerEditViewModel> editCustomerOrganizationDomainModelBuilder,
            IUserAppSession userSession)
        {
            if (organizationsService == null) throw new ArgumentNullException("organizationsService");
            if (customersViewModelBuilder == null) throw new ArgumentNullException("customersViewModelBuilder");
            if (editCustomerOrganizationViewModelBuilder == null) throw new ArgumentNullException("editCustomerOrganizationViewModelBuilder");
            if (editCustomerOrganizationDomainModelBuilder == null) throw new ArgumentNullException("editCustomerOrganizationDomainModelBuilder");
            if (userSession == null) throw new ArgumentNullException("userSession");

            this.organizationsService = organizationsService;
            this.customersViewModelBuilder = customersViewModelBuilder;
            this.editCustomerOrganizationViewModelBuilder = editCustomerOrganizationViewModelBuilder;
            this.editCustomerOrganizationDomainModelBuilder = editCustomerOrganizationDomainModelBuilder;
            this.userSession = userSession;
        }

        public ActionResult SingleCustomerIndex()
        {
            var organizationRoles = organizationsService.GetUsersOrganizationalRoles(userSession.CurrentUser.Id);

            return RedirectToAction("Index", "Site", new { customerId = organizationRoles.First().OrganizationId });
        }

        public ActionResult Index()
        {
            var viewModel = this.customersViewModelBuilder.Build();

            return View(viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new CustomerEditViewModel();

            return View("EditOrganization", viewModel);
        }

        [HttpPost]
        public ActionResult SaveOrganization(CustomerEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (ModelState.IsValid)
            {
                var customerOrganization = this.editCustomerOrganizationDomainModelBuilder.Build(viewModel);

                organizationsService.SaveCustomerOrganization(customerOrganization);

                TempData["ControllerActionMessage"] = "[[[Customer saved]]]";

                return RedirectToAction("View", new { @id = customerOrganization.Id });
            }

            if (viewModel.Id > 0)
            {
                return this.View(viewModel.Id);
            }

            return View("EditOrganization", viewModel);            
        }

        public ActionResult View(int? id)
        {
            var viewModel = this.editCustomerOrganizationViewModelBuilder.Build(id);
            
            return View("EditOrganization", viewModel);            
        }
    }
}