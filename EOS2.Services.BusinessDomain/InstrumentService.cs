namespace EOS2.Services.BusinessDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class InstrumentService : IInstrumentService
    {
        private readonly IRepository<Instrument> instrumentRepository;
        private readonly IRepository<Channel> channelRepository;

        public InstrumentService(IRepository<Instrument> instrumentRepository,  IRepository<Channel> channelRepository)
        {
            if (instrumentRepository == null) throw new ArgumentNullException("instrumentRepository");
            if (channelRepository == null) throw new ArgumentNullException("channelRepository");

            this.instrumentRepository = instrumentRepository;
            this.channelRepository = channelRepository;           
        }

        public ServiceResultDictionary SaveInstrument(Instrument instrument)
        {
            if (instrument == null) throw new ArgumentNullException("instrument");

            var serviceResult = new ServiceResultDictionary();

            if (instrument.Id > 0)
            {
                instrumentRepository.Update(instrument);
            }
            else
            {
                instrumentRepository.Add(instrument);
            }

            return serviceResult;
        }

        public Instrument GetInstrument(int instrumentId)
        {
            var instrument = instrumentRepository.Find(i => i.Id == instrumentId);

            return instrument;
        }

        public bool InstrumentExists(string name, int plantAreaId, int id)
        {
            return instrumentRepository.Find(i => i.Name.ToLower().Trim() == name.ToLower().Trim() && i.PlantAreaId == plantAreaId && i.Id != id) != null;
        }

        public IEnumerable<Equipment> GetEquipmentAttachedTo(int instrumentId)
        {
            var channels = this.channelRepository.FindAll(e => e.InstrumentId == instrumentId).ToList();

            return channels.Any()
                       ? channels
                            .Where(e => e.ConnectedToEquipment != null)
                            .Select(i => i.ConnectedToEquipment)
                             .GroupBy(
                                 eq => eq.Id,
                                 equip => equip,
                                 (key, equipment) => new { EquipmentId = key, Equipment = equipment })
                             .Select(e => e.Equipment.First())
                             .ToList()
                       : new List<Equipment>();
        }
    }
}
