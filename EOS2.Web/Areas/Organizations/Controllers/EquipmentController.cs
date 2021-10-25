namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Equipments;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class EquipmentController : BaseController
    {
        private readonly IEquipmentService equipmentService;

        private readonly IEditViewPartialModelBuilder<EquipmentEditViewModel> editEquipmentViewModelBuilder;
        private readonly IDomainModelBuilder<Equipment, EquipmentEditViewModel> editEquipmentDomainModelBuilder;

        public EquipmentController(IEquipmentService equipmentService, IEditViewPartialModelBuilder<EquipmentEditViewModel> editEquipmentViewModelBuilder, IDomainModelBuilder<Equipment, EquipmentEditViewModel> editEquipmentDomainModelBuilder)
        {
            if (equipmentService == null) throw new ArgumentNullException("equipmentService");
            if (editEquipmentViewModelBuilder == null) throw new ArgumentNullException("editEquipmentViewModelBuilder");
            if (editEquipmentDomainModelBuilder == null) throw new ArgumentNullException("editEquipmentDomainModelBuilder");

            this.equipmentService = equipmentService;
            this.editEquipmentViewModelBuilder = editEquipmentViewModelBuilder;
            this.editEquipmentDomainModelBuilder = editEquipmentDomainModelBuilder;
        }

        public ActionResult New()
        {
            var viewModel = editEquipmentViewModelBuilder.Build(null);

            return View("View", viewModel);
        }

        [HttpPost]
        public ActionResult Save(EquipmentEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (!this.ModelState.IsValid)
            {
                viewModel = editEquipmentViewModelBuilder.Rebuild(viewModel);

                return View("View", viewModel);
            }

            var equipment = editEquipmentDomainModelBuilder.Build(viewModel); 
            equipmentService.SaveEquipment(equipment);

            TempData["ControllerActionMessageEquipment"] = string.Format(CultureInfo.CurrentCulture, "[[[Equipment %0 saved successfully|||{0}]]]", equipment.Name);

            return RedirectToAction("View", "Equipment", new { @equipmentId = equipment.Id });
        }

        public ActionResult View(int equipmentId)
        {
            var viewModel = editEquipmentViewModelBuilder.Build(equipmentId);

            return View("View", viewModel);
        }
    }
}