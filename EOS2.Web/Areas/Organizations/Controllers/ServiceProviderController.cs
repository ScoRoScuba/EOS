namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Model;
    using EOS2.Model.Enums;
    using EOS2.Web.Areas.Organizations.Builders.ServiceProviders;
    using EOS2.Web.Areas.Organizations.ViewModels.ServiceProviders;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class ServiceProviderController : BaseController
    {
        private readonly IUserAppSession userSession;

        private readonly IOrganizationsService organizationsService;

        private readonly IViewModelBuilder<ServiceProviderIndexViewModel> serviceProviderViewModelBuilder;

        private readonly IViewModelWithQueryBuilder<ServiceProviderIndexViewModel> serviceProviderForPortalAgentViewModelBuilder;

        private readonly IDomainModelBuilder<Organization, ServiceProviderEditViewModel> editServiceProviderOrganizationDomainModelBuilder;

        private readonly IEditViewModelBuilder<ServiceProviderEditViewModel> editServiceProviderOrganizationViewModelBuilder;
       
        public ServiceProviderController(
            IUserAppSession userSession, 
            IOrganizationsService organizationsService, 
            IViewModelBuilder<ServiceProviderIndexViewModel> serviceProviderViewModelBuilder, 
            IViewModelWithQueryBuilder<ServiceProviderIndexViewModel> serviceProviderForPortalAgentViewModelBuilder,
            IDomainModelBuilder<Organization, ServiceProviderEditViewModel> editServiceProviderOrganizationDomainModelBuilder, 
            IEditViewModelBuilder<ServiceProviderEditViewModel> editServiceProviderOrganizationViewModelBuilder)
        {
            if (userSession == null) throw new ArgumentNullException("userSession");
            if (organizationsService == null) throw new ArgumentNullException("organizationsService");

            if (serviceProviderViewModelBuilder == null) throw new ArgumentNullException("serviceProviderViewModelBuilder");
            if (serviceProviderForPortalAgentViewModelBuilder == null) throw new ArgumentNullException("serviceProviderForPortalAgentViewModelBuilder");
            if (editServiceProviderOrganizationDomainModelBuilder == null) throw new ArgumentNullException("editServiceProviderOrganizationDomainModelBuilder");
            if (editServiceProviderOrganizationViewModelBuilder == null) throw new ArgumentNullException("editServiceProviderOrganizationViewModelBuilder");

            this.userSession = userSession;
            this.organizationsService = organizationsService;
            this.serviceProviderViewModelBuilder = serviceProviderViewModelBuilder;
            this.serviceProviderForPortalAgentViewModelBuilder = serviceProviderForPortalAgentViewModelBuilder;
            this.editServiceProviderOrganizationDomainModelBuilder = editServiceProviderOrganizationDomainModelBuilder;
            this.editServiceProviderOrganizationViewModelBuilder = editServiceProviderOrganizationViewModelBuilder;        
        }

        public ActionResult Index()
        {
            if (userSession.CurrentOrganizationType == OrganizationType.PortalAgent)
            {
                return View(serviceProviderForPortalAgentViewModelBuilder.Build(new ServiceProvidersForPortalAgentCriteria(userSession.CurrentOrganization.Id)));                
            }

            return View(serviceProviderViewModelBuilder.Build());
        }

        public ActionResult New()
        {
            var viewModel = new ServiceProviderEditViewModel()
                                {
                                    PortalAgentId = userSession.CurrentOrganizationType == OrganizationType.PortalAgent ? userSession.CurrentOrganization.Id : (int?)null
                                };

            return View("View", viewModel);
        }

        [HttpPost]
        public ActionResult Save(ServiceProviderEditViewModel editViewModel)
        {
            if (editViewModel == null) throw new ArgumentNullException("editViewModel");

            if (ModelState.IsValid)
            {
                var portalAgentOrganization = editServiceProviderOrganizationDomainModelBuilder.Build(editViewModel);

                if (editViewModel.PortalAgentId.HasValue)
                {
                    organizationsService.SaveServiceProviderOrganization(portalAgentOrganization, editViewModel.PortalAgentId.Value);
                }
                else
                {
                    organizationsService.SaveServiceProviderOrganization(portalAgentOrganization);
                }

                TempData["ControllerActionMessage"] = "[[[Service Provider Saved]]]";

                return RedirectToAction("View", new { @id = portalAgentOrganization.Id });
            }

            if (editViewModel.Id > 0)
            {
                return this.View(editViewModel.Id);
            }

            return this.View("View", editViewModel);
        }

        public ActionResult View(int? id)
        {
            if (!id.HasValue) throw new ArgumentNullException("id");

            var viewModel = this.editServiceProviderOrganizationViewModelBuilder.Build(id);

            return View("View", viewModel);             
        }
    }
}