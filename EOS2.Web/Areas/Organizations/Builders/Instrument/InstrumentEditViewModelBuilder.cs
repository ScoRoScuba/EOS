namespace EOS2.Web.Areas.Organizations.Builders.Instrument
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.Builders;

    public class InstrumentEditViewModelBuilder : IEditViewPartialModelBuilder<InstrumentEditViewModel>
    {
        private readonly IInstrumentService instrumentService;

        // TODO: Remove this once it is used.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Temporary suppression")]
        private readonly IPlantAreaService plantAreaService;
        private readonly IReferenceDataService referenceDataService;

        public InstrumentEditViewModelBuilder(
                IInstrumentService instrumentService, 
                IPlantAreaService plantAreaService, 
                IReferenceDataService referenceDataService)
        {
            if (instrumentService == null) throw new ArgumentNullException("instrumentService");
            if (referenceDataService == null) throw new ArgumentNullException("referenceDataService");
            if (plantAreaService == null) throw new ArgumentNullException("plantAreaService");
            
            this.instrumentService = instrumentService;
            this.referenceDataService = referenceDataService;
            this.plantAreaService = plantAreaService;
        }

        public InstrumentEditViewModel Build(int? id)
        {
            var viewModel = new InstrumentEditViewModel();

            if (id.HasValue)
            {
                viewModel = Mapper.Map<InstrumentEditViewModel>(instrumentService.GetInstrument(id.Value));
            }

            // Add in Reference Data
            viewModel.InstrumentTypes = this.GetInstrumentTypes();
            viewModel.CalibrationFrequencies = this.GetCalibrationFrequencies();

            return viewModel;
        }

        public InstrumentEditViewModel Rebuild(InstrumentEditViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            if (viewModel.InstrumentTypes == null) viewModel.InstrumentTypes = this.GetInstrumentTypes();
            if (viewModel.CalibrationFrequencies == null) viewModel.CalibrationFrequencies = this.GetCalibrationFrequencies();

            return viewModel;
        }

        private IEnumerable<InstrumentTypeViewModel> GetInstrumentTypes()
        {
            var instrumentTypes = Mapper.Map<IEnumerable<InstrumentTypeViewModel>>(referenceDataService.GetInstrumentTypes());

            return instrumentTypes.OrderBy(i => i.Name);            
        }

        private IEnumerable<CalibrationFrequencyViewModel> GetCalibrationFrequencies()
        {
            var calibrationFrequencies = Mapper.Map<IEnumerable<CalibrationFrequencyViewModel>>(referenceDataService.GetCalibrationFrequencies());
            return calibrationFrequencies.OrderBy(i => i.DurationPosition);
        }
    }
}