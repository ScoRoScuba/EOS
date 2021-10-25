namespace EOS2.Web.Areas.Organizations.Builders.InstrumentChannels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Areas.Organizations.ViewModels.InstrumentChannels;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.Builders;
    using EOS2.Web.ViewModels.Common;

    public class InstrumentsChannelsViewModel : IEditViewPartialModelBuilder<ChannelsViewModel>
    {
        private readonly IInstrumentService instrumentService;
        private readonly IPlantAreaService plantAreaService;
        private readonly IChannelService channelService;
        private readonly IReferenceDataService referenceDataService;

        public InstrumentsChannelsViewModel(
            IInstrumentService instrumentService,
            IChannelService channelService,
            IReferenceDataService referenceDataService, 
            IPlantAreaService plantAreaService)
        {
            if (instrumentService == null) throw new ArgumentNullException("instrumentService");            
            if (channelService == null) throw new ArgumentNullException("channelService");
            if (referenceDataService == null) throw new ArgumentNullException("referenceDataService");
            if (plantAreaService == null) throw new ArgumentNullException("plantAreaService");

            this.instrumentService = instrumentService;
            this.channelService = channelService;
            this.referenceDataService = referenceDataService;
            this.plantAreaService = plantAreaService;
        }

        public ChannelsViewModel Rebuild(ChannelsViewModel viewModel)
        {
           if (viewModel == null) throw new ArgumentNullException("viewModel");

            var instrument = instrumentService.GetInstrument(viewModel.InstrumentId);

            viewModel.Instrument = Mapper.Map<InstrumentEditViewModel>(instrument);
            viewModel.Equipment = GetPlantAreaEquipmentAsReferenceData(instrument.PlantAreaId);
            viewModel.ChannelTypes = referenceDataService.GetChannelTypes();
            viewModel.ScheduleType = referenceDataService.GetScheduleFrequencies().OrderBy(o => o.DurationPosition).ToList();
            viewModel.Channels = Mapper.Map<IEnumerable<ChannelViewModel>>(channelService.GetChannelsForInstrument(instrument.Id));

            return viewModel;
        }

        public ChannelsViewModel Build(int? id)
        {            
            if (!id.HasValue) throw new ArgumentNullException("id");

            var instrument = instrumentService.GetInstrument(id.Value);

            var viewModel = new ChannelsViewModel
                                {
                                    InstrumentId = id.Value,
                                    Instrument = Mapper.Map<InstrumentEditViewModel>(instrument),
                                    Equipment = GetPlantAreaEquipmentAsReferenceData(instrument.PlantAreaId),
                                    ChannelTypes = referenceDataService.GetChannelTypes(),
                                    ScheduleType = referenceDataService.GetScheduleFrequencies().OrderBy(o => o.DurationPosition).ToList(),
                                    Channels = Mapper.Map<IEnumerable<ChannelViewModel>>(channelService.GetChannelsForInstrument(id.Value)),
                                };

            return viewModel;
        }

        private IEnumerable<ReferenceDataType> GetPlantAreaEquipmentAsReferenceData(int plantAreaId)
        {
            var equipment = plantAreaService.GetEquipmentFor(plantAreaId).ToList();

            equipment.Insert(0, new Equipment() { Id = 0, Name = "None" });

            return equipment.Select(e => new ReferenceDataViewModel { Id = e.Id, Name = e.Name });
        }
    }
}