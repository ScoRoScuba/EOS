namespace EOS2.Services.BusinessDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EOS2.Common.Exceptions;
    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class EquipmentService : IEquipmentService
    {
        private readonly IRepository<Equipment> equipmentRepository;
        private readonly IChannelService channelService;

        public EquipmentService(IChannelService channelService, IRepository<Equipment> equipmentRepository)
        {        
            if (channelService == null) throw new ArgumentNullException("channelService");
            if (equipmentRepository == null) throw new ArgumentNullException("equipmentRepository");

            this.channelService = channelService;
            this.equipmentRepository = equipmentRepository;
        }

        public ServiceResultDictionary SaveEquipment(Equipment equipment)
        {
            if (equipment == null) throw new ArgumentNullException("equipment");

            var serviceResult = new ServiceResultDictionary();

            if (equipment.Id > 0)
            {
                this.equipmentRepository.Update(equipment);
            }
            else
            {
                this.equipmentRepository.Add(equipment);
            }

            return serviceResult;
        }

        public Equipment GetEquipment(int equipmentId)
        {
            var equipment = this.equipmentRepository.Find(e => e.Id == equipmentId);

            return equipment;
        }

        public bool EquipmentExists(string name, int plantAreaId, int id)
        {
            return this.equipmentRepository.Find(e => e.Name.ToLower().Trim() == name.ToLower().Trim() && e.PlantAreaId == plantAreaId && e.Id != id) != null;
        }

        public IEnumerable<Instrument> GetInstrumentsAttachedTo(int equipmentId)
        {
            var connectedInstruments = channelService.GetChannelsForEquipment(equipmentId);

            return connectedInstruments.Select(i => i.Instrument).GroupBy(
                                                        ins => ins.Id,
                                                        inst => inst,
                                                        (key, instrument) => new { InstrumentId = key, Instrument = instrument })
                                        .Select(i => i.Instrument.First())
                                        .ToList();
        }

        public void AllocateToChannel(int equipmentId, int channelId)
        {
            var channel = channelService.GetChannelById(channelId);
            if (channel == null) throw new ChannelAllocationException("Unknown Channel", channelId, equipmentId);

            var equipment = this.GetEquipment(equipmentId);
            if (equipment == null) throw new ChannelAllocationException("Unknown Item of Equipment", channelId, equipmentId);

            channel.ConnectedToEquipmentId = equipmentId;

            channelService.SaveChannel(channel);
        }

        public void DeallocateChannel(int channelId)
        {
            var channel = channelService.GetChannelById(channelId);
            if (channel == null) throw new ChannelAllocationException("Unknown Channel", channelId, null);
            
            channel.ConnectedToEquipmentId = null;

            channelService.SaveChannel(channel);
        }
    }
}
