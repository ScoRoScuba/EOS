namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Model;

    public interface IEquipmentService
    {
        ServiceResultDictionary SaveEquipment(Equipment equipment);

        Equipment GetEquipment(int equipmentId);

        IEnumerable<Instrument> GetInstrumentsAttachedTo(int equipmentId);

        bool EquipmentExists(string name, int plantAreaId, int id);

        void AllocateToChannel(int equipmentId, int channelId);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deallocate", Justification = "Method Name Describes Action")]
        void DeallocateChannel(int channelId);
    }
}
