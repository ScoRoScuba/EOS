namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Areas.Organizations.ViewModels.Site;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class SiteController : BaseController
    {
        private readonly ISiteService siteService;
        private readonly IEditViewPartialModelBuilder<CustomerSiteEditViewModel> editSiteViewModelBuilder;
        private readonly IDomainModelBuilder<Site, CustomerSiteEditViewModel> editSiteDomainModelBuilder;
        private readonly IEditViewModelBuilder<CustomerEditViewModel> sitesViewModelBuilder;

        public SiteController(
            ISiteService siteService,
            IEditViewPartialModelBuilder<CustomerSiteEditViewModel> editSiteViewModelBuilder,
            IDomainModelBuilder<Site, CustomerSiteEditViewModel> editSiteDomainModelBuilder,
            IEditViewModelBuilder<CustomerEditViewModel> sitesViewModelBuilder)
        {
            if (siteService == null) throw new ArgumentNullException("siteService");
            if (editSiteViewModelBuilder == null) throw new ArgumentNullException("editSiteViewModelBuilder");
            if (editSiteDomainModelBuilder == null) throw new ArgumentNullException("editSiteDomainModelBuilder");
            if (sitesViewModelBuilder == null) throw new ArgumentNullException("sitesViewModelBuilder");

            this.siteService = siteService;
            this.editSiteViewModelBuilder = editSiteViewModelBuilder;
            this.editSiteDomainModelBuilder = editSiteDomainModelBuilder;
            this.sitesViewModelBuilder = sitesViewModelBuilder;
        }

        public ActionResult Index(int customerId)
        {
            var viewModel = this.sitesViewModelBuilder.Build(customerId);

            return View("Index", viewModel);
        }

        public ActionResult New(int customerId)
        {
            var viewModel = new CustomerSiteEditViewModel { CustomerId = customerId };

            return View("View", viewModel);
        }

        public ActionResult View(int siteId)
        {
            var viewModel = editSiteViewModelBuilder.Build(siteId);

            return View("View", viewModel);
        }

        [HttpPost]
        public ActionResult Save(CustomerSiteEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (ModelState.IsValid)
            {
                var site = this.editSiteDomainModelBuilder.Build(viewModel);

                siteService.Save(site);

                TempData["ControllerActionMessageSite"] = string.Format(CultureInfo.CurrentUICulture, "[[[Site %0 saved successfully|||{0}]]]", site.Name);

                return RedirectToRoute("CustomerOrganization_Site", new { CustomerId = viewModel.CustomerId, SiteId = site.Id });
            }

            if (viewModel.Id != 0) viewModel = editSiteViewModelBuilder.Rebuild(viewModel);

            return View("View", viewModel);
        }
    }
}