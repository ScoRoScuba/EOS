namespace EOS2.Web.Areas.Organizations.Builders.PlantArea
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.PlantAreas;
    using EOS2.Web.Builders;

    using Common = EOS2.Web.Areas.Organizations.ViewModels.Common;

    public class PlantAreaEditViewModelBuilder : IEditViewPartialModelBuilder<PlantAreaEditViewModel>
    {
        private readonly IPlantAreaService plantAreaService;

        // TODO: Remove this once it is used.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Temporary suppression")]
        private readonly IEquipmentService equipmentService;

        // TODO: Remove this once it is used.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Temporary suppression")]
        private readonly IInstrumentService instrumentService;

        public PlantAreaEditViewModelBuilder(IPlantAreaService plantAreaService, IEquipmentService equipmentService, IInstrumentService instrumentService)
        {
            if (plantAreaService == null) throw new ArgumentNullException("plantAreaService");
            if (equipmentService == null) throw new ArgumentNullException("equipmentService");
            if (instrumentService == null) throw new ArgumentNullException("instrumentService");

            this.plantAreaService = plantAreaService;
            this.equipmentService = equipmentService;
            this.instrumentService = instrumentService;
        }

        public PlantAreaEditViewModel Build(int? id)
        {
            var viewModel = new PlantAreaEditViewModel();

            if (id.HasValue) viewModel = Mapper.Map<PlantAreaEditViewModel>(plantAreaService.GetPlantArea(id.Value));

            return viewModel;
        }

        public PlantAreaEditViewModel Rebuild(PlantAreaEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (viewModel.Equipments == null) viewModel.Equipments = Mapper.Map<IEnumerable<Common.EquipmentViewModel>>(plantAreaService.GetEquipmentFor(viewModel.Id));
            if (viewModel.Instruments == null) viewModel.Instruments = Mapper.Map<IEnumerable<Common.InstrumentViewModel>>(plantAreaService.GetInstrumentsFor(viewModel.Id));

            return viewModel;
        }
    }
}