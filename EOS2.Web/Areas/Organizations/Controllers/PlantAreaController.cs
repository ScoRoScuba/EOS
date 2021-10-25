namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.PlantAreas;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class PlantAreaController : BaseController
    {
        private readonly IPlantAreaService plantAreaService;
        private readonly IEditViewPartialModelBuilder<PlantAreaEditViewModel> editPlantAreaViewModelBuilder;
        private readonly IDomainModelBuilder<PlantArea, PlantAreaEditViewModel> editPlantAreaDomainModelBuilder;

        public PlantAreaController(IPlantAreaService plantAreaService, IEditViewPartialModelBuilder<PlantAreaEditViewModel> editPlantAreaViewModelBuilder, IDomainModelBuilder<PlantArea, PlantAreaEditViewModel> editPlantAreaDomainModelBuilder)
        {
            if (plantAreaService == null) throw new ArgumentNullException("plantAreaService");
            if (editPlantAreaViewModelBuilder == null) throw new ArgumentNullException("editPlantAreaViewModelBuilder");
            if (editPlantAreaDomainModelBuilder == null) throw new ArgumentNullException("editPlantAreaDomainModelBuilder");

            this.plantAreaService = plantAreaService;
            this.editPlantAreaViewModelBuilder = editPlantAreaViewModelBuilder;
            this.editPlantAreaDomainModelBuilder = editPlantAreaDomainModelBuilder;
        }

        public ActionResult New(int customerId, int siteId)
        {
            var viewModel = new PlantAreaEditViewModel
                                {
                                    CustomerId = customerId, 
                                    SiteId = siteId
                                };

            return View("EditPlantArea", viewModel);
        }

        [HttpPost]
        public ActionResult Save(PlantAreaEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (!this.ModelState.IsValid)
            {
                if (viewModel.Id != 0) viewModel = editPlantAreaViewModelBuilder.Rebuild(viewModel);
                return View("EditPlantArea", viewModel);
            }

            var plantArea = this.editPlantAreaDomainModelBuilder.Build(viewModel);
            this.plantAreaService.SavePlantArea(plantArea);

            this.TempData["ControllerActionMessagePlantArea"] = string.Format(
                CultureInfo.CurrentUICulture,
                "[[[Plant Area %0 saved successfully|||{0}]]]",
                plantArea.Name);

            return RedirectToRoute(
                "CustomerOrganization_SitePlantArea",
                new { CustomerId = viewModel.CustomerId, SiteId = viewModel.SiteId, PlantAreaId = plantArea.Id });
        }

        public ActionResult View(int plantAreaId)
        {
            var viewModel = this.editPlantAreaViewModelBuilder.Build(plantAreaId);

            return View("EditPlantArea", viewModel);
        }
    }
}