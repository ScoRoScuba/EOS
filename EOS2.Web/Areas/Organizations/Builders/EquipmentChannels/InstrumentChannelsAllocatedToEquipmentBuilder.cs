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

    public class InstrumentChannelsAllocatedToEquipmentBuilder : IViewModelWithQueryBuilder<ChannelsAllocatedToEquipmentCriteria, EquipmentChannelsViewModel>
    {
        private readonly IChannelService channelService;
        private readonly IReferenceDataService referenceDataService;

        public InstrumentChannelsAllocatedToEquipmentBuilder(IChannelService channelService, IReferenceDataService referenceDataService)
        {
            if (channelService == null) throw new ArgumentNullException("channelService");        
            if (referenceDataService == null) throw new ArgumentNullException("referenceDataService");

            this.channelService = channelService;
            this.referenceDataService = referenceDataService;
        }

        public EquipmentChannelsViewModel Build(ChannelsAllocatedToEquipmentCriteria criteria)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");

            var allocatedChanels = channelService.GetChannelsForEquipment(criteria.Equipment.Id)
                                                .Where(ch => ch.InstrumentId == criteria.Instrument.Id);

            var viewModel = new EquipmentChannelsViewModel
                                {
                                    InstrumentId = criteria.Instrument.Id,
                                    Instrument = Mapper.Map<InstrumentEditViewModel>(criteria.Instrument),
                                    Equipment = GetEquipmentAsReferenceData(criteria.Equipment),
                                    ChannelTypes = referenceDataService.GetChannelTypes(),
                                    ScheduleType =
                                        referenceDataService.GetScheduleFrequencies()
                                        .OrderBy(o => o.DurationPosition)
                                        .ToList(),
                                    Channels = Mapper.Map<IEnumerable<ChannelViewModel>>(allocatedChanels),
                                };

            viewModel.Channels.ForEach(
                ch =>
                    {
                        ch.EquipmentId = criteria.Equipment.Id;
                    });

            return viewModel;
        }

        private static IEnumerable<ReferenceDataType> GetEquipmentAsReferenceData(Equipment equipment)
        {
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