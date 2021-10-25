namespace EOS2.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using EOS2.Model;

    public interface IReferenceDataService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This should remain a method as it makes a db call")]
        IEnumerable<InstrumentType> GetInstrumentTypes();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This should remain a method as it makes a db call")]
        IEnumerable<EquipmentType> GetEquipmentTypes();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This should remain a method as it makes a db call")]
        IEnumerable<ScheduleFrequency> GetScheduleFrequencies();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This should remain a method as it makes a db call")]
        IEnumerable<ChannelType> GetChannelTypes();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This should remain a method as it makes a db call")]
        IEnumerable<CalibrationFrequency> GetCalibrationFrequencies();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Not appropriate to use property here")]
        IEnumerable<CertificateType> GetInstrumentCertificateTypes();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Not appropriate to use property here")]
        IEnumerable<CertificateType> GetEquipmentCertificateTypes();
    }
}
