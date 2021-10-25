namespace EOS2.Web.Tests.TestStubs
{
    using System;
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class StubEquipmentService : IEquipmentService
    {
        private readonly bool equipmentExistsReturnValue;

        public StubEquipmentService(bool equipmentExistsReturnValue)
        {
            this.equipmentExistsReturnValue = equipmentExistsReturnValue;
        }

        public static IEnumerable<Equipment> GetEquipmentsByPlantArea(int plantAreaId)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Equipment> GetEquipmentsByInstrument(int instrumentId)
        {
            throw new NotImplementedException();
        }

        public bool EquipmentExists(string name, int plantAreaId, int id)
        {
            return equipmentExistsReturnValue;
        }

        public ServiceResultDictionary SaveEquipment(Equipment equipment)
        {
            throw new NotImplementedException();
        }

        public Equipment GetEquipment(int equipmentId)
        {
            throw new NotImplementedException();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "equipmentId", Justification = "Test code, must be here.")]
        public IEnumerable<Instrument> GetInstrumentsAttachedTo(int equipmentId)
        {
            throw new NotImplementedException();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "equipmentId", Justification = "Test code, must be here.")]
        public IEnumerable<Channel> GetAllocatedChannelsFor(int equipmentId)
        {
            throw new NotImplementedException();            
        }

        public void AllocateToChannel(int equipmentId, int channelId)
        {
        throw new NotImplementedException();
        }

        public void DeallocateChannel(int channelId)
        {
        throw new NotImplementedException();
        }
    }
}
