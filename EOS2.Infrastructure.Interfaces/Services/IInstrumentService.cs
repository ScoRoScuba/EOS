namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Model;

    public interface IInstrumentService
    {
        ServiceResultDictionary SaveInstrument(Instrument instrument);

        Instrument GetInstrument(int instrumentId);

        bool InstrumentExists(string name, int plantAreaId, int id);

        IEnumerable<Equipment> GetEquipmentAttachedTo(int instrumentId);
    }
}
