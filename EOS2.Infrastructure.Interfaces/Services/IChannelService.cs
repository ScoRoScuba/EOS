namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Model;

    public interface IChannelService
    {
        ServiceResultDictionary CreateDefaultSetOfChannelsForInstrument(
            int numberOfChannelsToCreate,
            int parentInstrumentId,
            int channelTypeId,
            int connectedToEquipmentId,
            int scheduleTypeId);

        ServiceResultDictionary SaveChannel(Channel channel);

        IEnumerable<Channel> GetChannelsForInstrument(int instrumentId);

        IEnumerable<Channel> GetChannelsForEquipment(int equipmentId);

        Channel GetChannelById(int channelId);

        IEnumerable<Channel> GetUnallocatedChannelsFor(int instrumentId);
    }
}