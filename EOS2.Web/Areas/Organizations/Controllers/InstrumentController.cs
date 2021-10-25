namespace EOS2.Web.Areas.Organizations.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.Builders;
    using EOS2.Web.Code;
    using EOS2.Web.Filters;

    public class InstrumentController : BaseController
    {
       private readonly IInstrumentService instrumentService;

       private readonly IEditViewPartialModelBuilder<InstrumentEditViewModel> editInstrumentViewModelBuilder;
       private readonly IDomainModelBuilder<Instrument, InstrumentEditViewModel> editInstrumentDomainModelBuilder;
       private readonly IEditViewModelBuilder<AvailableInstrumentsViewModel> availableInstrumentsViewModelBuilder;

        public InstrumentController(
                                IInstrumentService instrumentService, 
                                IEditViewPartialModelBuilder<InstrumentEditViewModel> editInstrumentViewModelBuilder, 
                                IDomainModelBuilder<Instrument, InstrumentEditViewModel> editInstrumentDomainModelBuilder, 
                                IEditViewModelBuilder<AvailableInstrumentsViewModel> availableInstrumentsViewModelBuilder)
        {
            if (instrumentService == null) throw new ArgumentNullException("instrumentService");
            if (editInstrumentViewModelBuilder == null) throw new ArgumentNullException("editInstrumentViewModelBuilder");
            if (editInstrumentDomainModelBuilder == null) throw new ArgumentNullException("editInstrumentDomainModelBuilder");
            if (availableInstrumentsViewModelBuilder == null) throw new ArgumentNullException("availableInstrumentsViewModelBuilder");

            this.instrumentService = instrumentService;
            this.editInstrumentViewModelBuilder = editInstrumentViewModelBuilder;
            this.editInstrumentDomainModelBuilder = editInstrumentDomainModelBuilder;
            this.availableInstrumentsViewModelBuilder = availableInstrumentsViewModelBuilder;
        }

        public ActionResult New()
        {
            var viewModel = editInstrumentViewModelBuilder.Build(null);

            return View("View", viewModel);
        }

        [HttpPost]
        public ActionResult Save(InstrumentEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (!this.ModelState.IsValid)
            {
                viewModel = editInstrumentViewModelBuilder.Rebuild(viewModel);

                return View("View", viewModel);
            }

            var instrument = editInstrumentDomainModelBuilder.Build(viewModel); 
            instrumentService.SaveInstrument(instrument);

            TempData["ControllerActionMessageInstrument"] = string.Format(CultureInfo.CurrentCulture, "[[[Instrument %0 saved successfully|||{0}]]]", instrument.Name);

            return RedirectToAction("View", "Instrument", new { @instrumentId = instrument.Id });
        }

        public ActionResult View(int instrumentId)
        {
            var viewModel = editInstrumentViewModelBuilder.Build(instrumentId); 

            return View("View", viewModel);
        }

        public ActionResult AvailableInstruments(int plantAreaId)
        {
            var viewModel = availableInstrumentsViewModelBuilder.Build(plantAreaId);

            return this.View("AvailableInstruments", viewModel);
        }

        [HttpPost]
        public ActionResult AvailableInstruments(AvailableInstrumentsViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (!this.ModelState.IsValid)
            {
                viewModel = availableInstrumentsViewModelBuilder.Build(viewModel.PlantAreaId);
                return this.View("AvailableInstruments", viewModel);
            }

            viewModel.InstrumentId = viewModel.SelectedInstrumentId;

            return RedirectToRoute(
                "CustomerOrganization_SitePlantAreaEquipmentAvailableChannels",
                new
                    {
                        viewModel.CustomerId,
                        viewModel.SiteId,
                        viewModel.PlantAreaId,
                        viewModel.EquipmentId,
                        viewModel.InstrumentId
                    });
        }
    }
}