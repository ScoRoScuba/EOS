namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Infrastructure.Interfaces.SessionManagement;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.PortalAgents;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class PortalAgentController : BaseController
    {
        // TODO: Remove this when userSession is used.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "This will be used later in the project.")]
        private readonly IUserAppSession userSession;

        private readonly IOrganizationsService organizationsService;

        private readonly IViewModelBuilder<PortalAgentIndexViewModel> portalAgentViewModelBuilder;

        private readonly IDomainModelBuilder<Organization, PortalAgentEditViewModel> editPortalOrganizationDomainModelBuilder;

        private readonly IEditViewModelBuilder<PortalAgentEditViewModel> editPortalAgentOrganizationViewModelBuilder;

        public PortalAgentController(IUserAppSession userSession, IOrganizationsService organizationsService, IViewModelBuilder<PortalAgentIndexViewModel> portalAgentViewModelBuilder, IDomainModelBuilder<Organization, PortalAgentEditViewModel> editPortalOrganizationDomainModelBuilder, IEditViewModelBuilder<PortalAgentEditViewModel> editPortalAgentOrganizationViewModelBuilder)
        {
            if (userSession == null) throw new ArgumentNullException("userSession");
            if (organizationsService == null) throw new ArgumentNullException("organizationsService");
            if (portalAgentViewModelBuilder == null) throw new ArgumentNullException("portalAgentViewModelBuilder");
            if (editPortalOrganizationDomainModelBuilder == null) throw new ArgumentNullException("editPortalOrganizationDomainModelBuilder");
            if (editPortalAgentOrganizationViewModelBuilder == null) throw new ArgumentNullException("editPortalAgentOrganizationViewModelBuilder");

            this.userSession = userSession;
            this.organizationsService = organizationsService;
            this.portalAgentViewModelBuilder = portalAgentViewModelBuilder;
            this.editPortalOrganizationDomainModelBuilder = editPortalOrganizationDomainModelBuilder;
            this.editPortalAgentOrganizationViewModelBuilder = editPortalAgentOrganizationViewModelBuilder;
        }

        // GET: Organizations/PortalAgents
        public ActionResult Index()
        {
            var viewModel = portalAgentViewModelBuilder.Build();

            return View(viewModel);
        }

        public ActionResult New()
        {
            return View("View", new PortalAgentEditViewModel());
        }

        [HttpPost]
        public ActionResult Save(PortalAgentEditViewModel editViewModel)
        {
            if (editViewModel == null) throw new ArgumentNullException("editViewModel");

            if (ModelState.IsValid)
            {
                var portalAgentOrganization = editPortalOrganizationDomainModelBuilder.Build(editViewModel);

                organizationsService.SavePortalAgentOrganization(portalAgentOrganization);

                TempData["ControllerActionMessage"] = "[[[Portal Agent saved]]]";

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

            var viewModel = this.editPortalAgentOrganizationViewModelBuilder.Build(id);

            return View("View", viewModel);         
        }
    }
}