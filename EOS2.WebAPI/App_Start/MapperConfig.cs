namespace EOS2.WebAPI
{
    using AutoMapper;

    using EOS2.Model;
    using EOS2.WebAPI.Models;

    public static class MapperConfig
    {
        public static void Configure()
        {
            ModelToApiModelToModel();
        }

        private static void ModelToApiModelToModel()
        {
            // [NOTE: Any .ForMembers after a .ReverseMap only apply to the reverse mapping, any before only apply to the forward map, so please respect order]
            // Full ViewModels
            Mapper.CreateMap<InstrumentType, ReferenceDataItem>().ReverseMap();
            Mapper.CreateMap<EquipmentType, ReferenceDataItem>().ReverseMap();
            Mapper.CreateMap<CertificateType, ReferenceDataItem>().ReverseMap();
            Mapper.CreateMap<CalibrationFrequency, ReferenceDataItem>().ReverseMap();
            Mapper.CreateMap<ChannelType, ReferenceDataItem>().ReverseMap();
            Mapper.CreateMap<FurnaceClass, ReferenceDataItem>().ReverseMap();
        }
    }
}