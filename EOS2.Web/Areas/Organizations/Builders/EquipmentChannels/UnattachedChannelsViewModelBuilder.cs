namespace EOS2.Web.Areas.Organizations.Builders.EquipmentChannels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Areas.Organizations.ViewModels.EquipmentChannels;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.Builders;
    using EOS2.Web.ViewModels.Common;

    using WebGrease.Css.Extensions;

    public class UnattachedChannelsViewModelBuilder : IViewModelWithQueryBuilder<UnallocatedChannelsForInstrumentCriteria, EquipmentChannelsViewModel>
    {
        private readonly IChannelService channelService;
        private readonly IInstrumentService instrumentService;
        private readonly IReferenceDataService referenceDataService;
        private readonly IEquipmentService equipmentService;

        public UnattachedChannelsViewModelBuilder(IChannelService channelService, IInstrumentService instrumentService, IReferenceDataService referenceDataService, IEquipmentService equipmentService)
        {
            if (channelService == null) throw new ArgumentNullException("channelService");
            if (instrumentService == null) throw new ArgumentNullException("instrumentService");
            if (referenceDataService == null) throw new ArgumentNullException("referenceDataService");
            if (equipmentService == null) throw new ArgumentNullException("equipmentService");

            this.channelService = channelService;
            this.instrumentService = instrumentService;
            this.referenceDataService = referenceDataService;
            this.equipmentService = equipmentService;
        }

        public EquipmentChannelsViewModel Build(UnallocatedChannelsForInstrumentCriteria criteria)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");

            var instrument = instrumentService.GetInstrument(criteria.InstrumentId);

            var unallocatedChanels = channelService.GetUnallocatedChannelsFor(instrument.Id);

            var viewModel = new EquipmentChannelsViewModel
                                {
                                    InstrumentId = instrument.Id,
                                    Instrument = Mapper.Map<InstrumentEditViewModel>(instrument),
                                    Equipment = GetEquipmentAsReferenceData(criteria.EquipmentToSelectId),
                                    ChannelTypes = referenceDataService.GetChannelTypes(),
                                    ScheduleType =
                                        referenceDataService.GetScheduleFrequencies()
                                        .OrderBy(o => o.DurationPosition)
                                        .ToList(),
                                    Channels = Mapper.Map<IEnumerable<ChannelViewModel>>(unallocatedChanels),
                                };

            viewModel.Channels.ForEach(
                ch =>
                    {
                        ch.EquipmentId = criteria.EquipmentToSelectId;
                    });

            return viewModel;
        }

        private IEnumerable<ReferenceDataType> GetEquipmentAsReferenceData(int equipmentId)
        {
            var equipment = equipmentService.GetEquipment(equipmentId);

            var equipmentList = new List<ReferenceDataViewModel>
                                    {
                                        new ReferenceDataViewModel { Id = 0, Name = "None" },
                                        new ReferenceDataViewModel
                                            {
                                                Id = equipment.Id,
                                                Name = equipment.Name
                                            },
                                    };

            return equipmentList;
        }
    }
}