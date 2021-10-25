namespace EOS2.Services.BusinessDomain
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class ChannelService : IChannelService                                
    {
        private readonly IRepository<Channel> channelRepository;
        private readonly IInstrumentService instrumentService;
        private readonly IReferenceDataService referenceDataService;

        public ChannelService(IRepository<Channel> channelRepository, IInstrumentService instrumentService, IReferenceDataService referenceDataService)
        {
            if (channelRepository == null) throw new ArgumentNullException("channelRepository");
            if (instrumentService == null) throw new ArgumentNullException("instrumentService");
            if (referenceDataService == null) throw new ArgumentNullException("referenceDataService");

            this.channelRepository = channelRepository;
            this.instrumentService = instrumentService;
            this.referenceDataService = referenceDataService;
        }

        public ServiceResultDictionary SaveChannel(Channel channel)
        {
            if (channel == null) throw new ArgumentNullException("channel");

            if (channel.Id > 0)
            {
                channelRepository.Update(channel);
            }
            else
            {
                channelRepository.Add(channel);
            }

            return new ServiceResultDictionary();
        }

        public IEnumerable<Channel> GetChannelsForInstrument(int instrumentId)
        {
            return channelRepository.FindAll(c => c.InstrumentId == instrumentId).ToList();
        }

        public IEnumerable<Channel> GetChannelsForEquipment(int equipmentId)
        {
            return channelRepository.FindAll(ch => ch.ConnectedToEquipmentId == equipmentId).ToList();
        }

        public ServiceResultDictionary CreateDefaultSetOfChannelsForInstrument(int numberOfChannelsToCreate, int parentInstrumentId, int channelTypeId, int connectedToEquipmentId, int scheduleTypeId)
        {
            var serviceResult = new ServiceResultDictionary();

            if (parentInstrumentId == 0)
            {
                serviceResult.AddModelError("InstrumentId", new ArgumentOutOfRangeException("parentInstrumentId", "Id of the Instrument is required"));
                return serviceResult;
            }

            var instrument = instrumentService.GetInstrument(parentInstrumentId);

            var channelType = referenceDataService.GetChannelTypes().SingleOrDefault(t => t.Id == channelTypeId);

            for (int i = 0; i < numberOfChannelsToCreate; i++)
            {
                int decimalLength = i.ToString("D", CultureInfo.InvariantCulture).Length + 3;

                SaveChannel(
                    new Channel
                        {
                            InstrumentId = instrument.Id,
                            Name = GetDefaultChannelName(i, instrument.Name),
                            Number = i.ToString("D" + decimalLength, CultureInfo.InvariantCulture),
                            ConnectedToEquipmentId =
                                connectedToEquipmentId == 0 ? (int?)null : connectedToEquipmentId,
                            ScheduleFrequencyId = scheduleTypeId,
                            Type = channelType
                        });
            }

            return serviceResult;
        }

        protected static string GetDefaultChannelName(int channelNumber, string instrumentName)
        {
            int decimalLength = channelNumber.ToString("D", CultureInfo.InvariantCulture).Length + 3;
            return string.Format(CultureInfo.CurrentUICulture, "{0}-{1}", channelNumber.ToString("D" + decimalLength, CultureInfo.InvariantCulture), instrumentName);            
        }

        public Channel GetChannelById(int channelId)
        {
            return channelRepository.Find(c => c.Id == channelId);
        }

        public IEnumerable<Channel> GetUnallocatedChannelsFor(int instrumentId)
        {
            return channelRepository.FindAll(ch => ch.InstrumentId == instrumentId && ch.ConnectedToEquipmentId == null);
        }
    }
}
