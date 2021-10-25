namespace EOS2.WebAPI.Models
{
    using System.Collections.Generic;

    public class ReferenceData
    {
        /// <summary>
        /// The list of Instrument Types
        /// </summary>
        public IEnumerable<ReferenceDataItem> InstrumentTypes { get; set; }

        /// <summary>
        /// The list of Equipment Types
        /// </summary>
        public IEnumerable<ReferenceDataItem> EquipmentTypes { get; set; }

        /// <summary>
        /// The list of Chennel Types
        /// </summary>
        public IEnumerable<ReferenceDataItem> ChannelTypes { get; set; }

        /// <summary>
        /// The list of Certificate Types
        /// </summary>
        public IEnumerable<ReferenceDataItem> CertificateTypes { get; set; }

        /// <summary>
        /// The list of Calibration Frequencies
        /// </summary>
        public IEnumerable<ReferenceDataItem> CalibrationFrequencies { get; set; }
    }
}