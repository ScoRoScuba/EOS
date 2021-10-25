namespace EOS2.Web.Tests.TestStubs
{
    using System;
    using System.Collections.Generic;

    using EOS2.Common.Validation;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class StubInstrumentService : IInstrumentService
    {
        private readonly bool instrumentExistsReturnValue;

        private readonly int organizationIdReturnValue;

        public StubInstrumentService(bool instrumentExistsReturnValue)
        {
            this.instrumentExistsReturnValue = instrumentExistsReturnValue;
        }

        public StubInstrumentService(int organizationIdReturnValue)
        {
            this.organizationIdReturnValue = organizationIdReturnValue;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Test Code, Main function expects to use DB so must be a function")]
        public static IEnumerable<InstrumentType> GetInstrumentTypes()
        {
            throw new NotImplementedException();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Test Code, Main function expects to use DB so must be a function")]
        public static IEnumerable<CalibrationFrequency> GetCalibrationFrequencies()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Instrument> GetInstrumentsForEquipment(int equipmentId)
        {
            throw new NotImplementedException();
        }

        public static ServiceResultDictionary AttachEquipment(int instrumentId, int equipmentId)
        {
            throw new NotImplementedException();
        }

        public static ServiceResultDictionary DetachEquipment(int instrumentId, int equipmentId)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Instrument> GetInstrumentsForPlantArea(int plantAreaId)
        {
            throw new NotImplementedException();
        }

        public Instrument GetInstrument(int instrumentId)
        {
            return new Instrument()
                       {
                           PlantArea =
                               new PlantArea
                                   {
                                       Site =
                                           new Site
                                               {
                                                   OrganizationId = organizationIdReturnValue
                                               }
                                   }
                       };
        }

        public ServiceResultDictionary SaveInstrument(Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public bool InstrumentExists(string name, int plantAreaId, int id)
        {
            return instrumentExistsReturnValue;
        }

        public IEnumerable<Equipment> GetEquipmentAttachedTo(int instrumentId)
        {
            throw new NotImplementedException();
        }
    }
}
