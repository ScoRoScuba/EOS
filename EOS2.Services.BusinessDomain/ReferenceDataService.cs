namespace EOS2.Services.BusinessDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;

    public class ReferenceDataService : IReferenceDataService
    {
        private readonly IRepository<ScheduleFrequency> frequencyRepository;
        private readonly IRepository<EquipmentType> equipmentTypeRepository;
        private readonly IRepository<InstrumentType> instrumentTypeRepository;
        private readonly IRepository<CalibrationFrequency> calibrationFrequencyRepository;
        private readonly IRepository<ChannelType> channelTypeRepository;
        private readonly IRepository<CertificateType> certificateTypeRepository; 

        public ReferenceDataService(
            IRepository<EquipmentType> equipmentTypeRepository,
            IRepository<InstrumentType> instrumentTypeRepository,
            IRepository<ScheduleFrequency> frequencyRepository,
            IRepository<ChannelType> channelTypeRepository, 
            IRepository<CalibrationFrequency> calibrationFrequencyRepository,
            IRepository<CertificateType> certificateTypeRepository)
        {
            if (equipmentTypeRepository == null) throw new ArgumentNullException("equipmentTypeRepository");
            if (instrumentTypeRepository == null) throw new ArgumentNullException("instrumentTypeRepository");
            if (frequencyRepository == null) throw new ArgumentNullException("frequencyRepository");
            if (channelTypeRepository == null) throw new ArgumentNullException("channelTypeRepository");
            if (calibrationFrequencyRepository == null) throw new ArgumentNullException("calibrationFrequencyRepository");
            if (certificateTypeRepository == null) throw new ArgumentNullException("certificateTypeRepository");

            this.equipmentTypeRepository = equipmentTypeRepository;
            this.instrumentTypeRepository = instrumentTypeRepository;
            this.frequencyRepository = frequencyRepository;
            this.channelTypeRepository = channelTypeRepository;
            this.calibrationFrequencyRepository = calibrationFrequencyRepository;
            this.certificateTypeRepository = certificateTypeRepository;
        }

        public IEnumerable<EquipmentType> GetEquipmentTypes()
        {
            return equipmentTypeRepository.GetAll().ToList();
        }

        public IEnumerable<InstrumentType> GetInstrumentTypes()
        {
            return instrumentTypeRepository.GetAll().ToList();
        }

        public IEnumerable<ScheduleFrequency> GetScheduleFrequencies()
        {
            return frequencyRepository.GetAll().ToList();
        }

        public IEnumerable<ChannelType> GetChannelTypes()
        {
            return channelTypeRepository.GetAll().ToList();
        }

        public IEnumerable<CalibrationFrequency> GetCalibrationFrequencies()
        {
            return calibrationFrequencyRepository.GetAll();
        }

        public IEnumerable<CertificateType> GetEquipmentCertificateTypes()
        {
            return certificateTypeRepository.FindAll(ct => ct.IsEquipmentApplicable);
        }

        public IEnumerable<CertificateType> GetInstrumentCertificateTypes()
        {
            return certificateTypeRepository.FindAll(ct => ct.IsInstrumentApplicable);
        }
    }
}
