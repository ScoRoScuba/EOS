namespace EOS2.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using AutoMapper;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.WebAPI.Models;

    [RoutePrefix("api/v1/ReferenceData")]
    public class ReferenceDataController : ApiController
    {
        private readonly IReferenceDataService referenceDataService;

        public ReferenceDataController(IReferenceDataService referenceDataService)
        {
            if (referenceDataService == null) throw new ArgumentNullException("referenceDataService");

            this.referenceDataService = referenceDataService;
        }

        /// <summary>
        /// Gets all the Reference Data in one hit.
        /// </summary>
        [Route("")]
        public ReferenceData Get()
        {
            var referenceData = new ReferenceData
                                    {
                                        InstrumentTypes = this.GetInstrumentTypes(),
                                        EquipmentTypes = this.GetEquipmentTypes(),
                                        CalibrationFrequencies = this.GetCalibrationFrequencies(),
                                        CertificateTypes = this.GetCertificateTypes(),
                                        ChannelTypes = this.GetChannelTypes(),
                                    };

            return referenceData;
        }

        /// <summary>
        /// Gets the list of Instrument Types.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Appropriate to be a Method not a Property"), Route("InstrumentType")]
        public IEnumerable<ReferenceDataItem> GetInstrumentTypes()
        {
            var instrumentTypes = Mapper.Map<IEnumerable<ReferenceDataItem>>(this.referenceDataService.GetInstrumentTypes());

            return instrumentTypes;
        }

        /// <summary>
        /// Gets the list of Equipment Types.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Appropriate to be a Method not a Property"), Route("EquipmentType")]
        public IEnumerable<ReferenceDataItem> GetEquipmentTypes()
        {
            var equipmentTypes = Mapper.Map<IEnumerable<ReferenceDataItem>>(this.referenceDataService.GetEquipmentTypes());

            return equipmentTypes;
        }

        /// <summary>
        /// Gets the list of Channel Types.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Appropriate to be a Method not a Property"), Route("ChannelType")]
        public IEnumerable<ReferenceDataItem> GetChannelTypes()
        {
            var channelTypes = Mapper.Map<IEnumerable<ReferenceDataItem>>(this.referenceDataService.GetChannelTypes());

            return channelTypes;
        }

        /// <summary>
        /// Gets the list of Certificate Types.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Appropriate to be a Method not a Property"), Route("CertificateType")]
        public IEnumerable<ReferenceDataItem> GetCertificateTypes()
        {
            // Currently only getting certificate types relating to instruments (i.e. calibrations), this can/will change with the full range of certification
            var certificateTypes = Mapper.Map<IEnumerable<ReferenceDataItem>>(this.referenceDataService.GetInstrumentCertificateTypes());

            return certificateTypes;
        }

        /// <summary>
        /// Gets the list of Calibration Frequencies.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Appropriate to be a Method not a Property"), Route("CalibrationFrequency")]
        public IEnumerable<ReferenceDataItem> GetCalibrationFrequencies()
        {
            var calibrationFrequencies = Mapper.Map<IEnumerable<ReferenceDataItem>>(this.referenceDataService.GetCalibrationFrequencies());

            return calibrationFrequencies;
        }
    }
}