[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EOS2.Web.MapperConfig), "Configure", Order = 2)]

namespace EOS2.Web
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using EOS2.Identity.Model;
    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Model;
    using EOS2.Web.Areas.Organizations.ViewModels.Certificate;
    using EOS2.Web.Areas.Organizations.ViewModels.Common;
    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Areas.Organizations.ViewModels.Equipments;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.Areas.Organizations.ViewModels.PlantAreas;
    using EOS2.Web.Areas.Organizations.ViewModels.Schedules;
    using EOS2.Web.Areas.Organizations.ViewModels.Site;
    using EOS2.Web.Areas.Organizations.ViewModels.Users;
    
    using EOS2.Web.ViewModels;

    public static class MapperConfig
    {
        public static void Configure()
        {
            var logger = DependencyResolver.Current.GetService<ILoggerService>();            
            try
            {  
                // this is the prefered method to configure Automapper
                // heres why.  https://groups.google.com/forum/?fromgroups=#!topic/automapper-users/0RgIjrKi28U 
                // Comments from Jimmy Bogard the author of Automapper
                Mapper.Initialize(
                                cfg =>
                                    {
                                        ModelToViewModelToModel(cfg);
                                        CommonModelToViewModelToModel(cfg);
                                    });
                        
                Mapper.AssertConfigurationIsValid();

                logger.Log("AutoMapper Configuration Set");                
            } 
            catch (Exception ex)
            {                                    
                logger.LogFatal("AutoMapper Configuration Error", ex);
                throw;
            }
        }

        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NonExistent", Justification = "Named as required")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Extension Method")]
        public static IMappingExpression<TSource, TDestination> IgnoreNonExistentMembers<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mapping)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            var existingMappings = destinationType.IsSubclassOf(typeof(BaseViewModel)) ? 
                                           Mapper.GetAllTypeMaps().First(x => x.SourceType == sourceType && x.DestinationType == destinationType) : 
                                           Mapper.GetAllTypeMaps().First(x => x.DestinationType == destinationType && x.SourceType == sourceType);

            foreach (var property in existingMappings.GetUnmappedPropertyNames())
            {
                mapping.ForMember(property, opt => opt.Ignore());
            }
            
            return mapping;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "High Coupling here is expected due to Automapper use")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "FXCop calculates Cyclomatic Complexity incorrectly when evaluating lamda expressions, this code has a cyclomaticComplexity of 1 (well below its recommended value max of 25), but FXCop calculates it to be 33 as it evaluates the IL not the code, as cyclomatic complexity is a measure of readability the IL code is irrelevant!")]
        private static void ModelToViewModelToModel(IProfileExpression cfg)
        {
            // [NOTE: Any .ForMembers after a .ReverseMap only apply to the reverse mapping, any before only apply to the forward map, so please respect order]
            // Full ViewModels
            cfg.CreateMap<Equipment, EquipmentEditViewModel>()
                .ForMember(dest => dest.StoredName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Schedules, opt => opt.Ignore())
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap()
                .ForSourceMember(src => src.StoredName, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.Schedules, opt => opt.Ignore());
                
            cfg.CreateMap<Schedule, ScheduleEditViewModel>()
                .ForMember(dest => dest.StoredName, opt => opt.MapFrom(src => src.Name))
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap()
                .ForSourceMember(src => src.StoredName, opt => opt.Ignore())
                .ForMember(dest => dest.FurnaceClass, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.Frequency, opt => opt.Ignore());

            cfg.CreateMap<Instrument, InstrumentEditViewModel>()
                .ForMember(dest => dest.StoredName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Certificates, opt => opt.MapFrom(src => src.CertificateHeaders))
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap()
                .ForSourceMember(src => src.StoredName, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.CalibrationFrequency, opt => opt.Ignore())
                .ForMember(dest => dest.CertificateHeaders, opt => opt.MapFrom(src => src.Certificates));

            cfg.CreateMap<PlantArea, PlantAreaEditViewModel>()
                .ForMember(dest => dest.StoredName, opt => opt.MapFrom(src => src.Name))
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap()
                .ForSourceMember(src => src.StoredName, opt => opt.Ignore());

            cfg.CreateMap<Site, CustomerSiteEditViewModel>()
                .ForMember(dest => dest.StoredName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.OrganizationId))
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap()
                .ForSourceMember(src => src.StoredName, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.CustomerId));

            cfg.CreateMap<Organization, CustomerEditViewModel>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap();

            cfg.CreateMap<Channel, ChannelViewModel>()
                .ForMember(dest => dest.SelectedChannelTypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.SelectedEquipmentTypeId, opt => opt.MapFrom(src => src.ConnectedToEquipmentId))
                .ForMember(dest => dest.SelectedScheduleFrequencyId, opt => opt.MapFrom(src => src.ScheduleFrequencyId)) 
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.SelectedChannelTypeId))
                .ForMember(dest => dest.ConnectedToEquipmentId, opt => opt.MapFrom(src => src.SelectedEquipmentTypeId))
                .ForMember(dest => dest.ScheduleFrequencyId, opt => opt.MapFrom(src => src.SelectedScheduleFrequencyId));

            cfg.CreateMap<CertificateHeader, CertificateEditViewModel>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.Ignore());

            cfg.CreateMap<User, UserEditViewModel>()
                .ForMember(dest => dest.ConfirmationEmail, opt => opt.MapFrom(src => src.Email))
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ConfirmationEmail));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "High Coupling here is expected due to Automapper use")]
        private static void CommonModelToViewModelToModel(IProfileExpression cfg)
        {
            // Common ViewModels (these are consumed by full View Models)
            cfg.CreateMap<CalibrationFrequency, CalibrationFrequencyViewModel>()
                .ReverseMap();

            cfg.CreateMap<Equipment, EquipmentViewModel>()
                .ForSourceMember(src => src.Schedules, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Schedules, opt => opt.Ignore());

            cfg.CreateMap<EquipmentType, EquipmentTypeViewModel>()
                .ReverseMap();

            cfg.CreateMap<Schedule, ScheduleViewModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ReverseMap();

            cfg.CreateMap<FurnaceClass, FurnaceClassViewModel>()
                .ReverseMap();

            cfg.CreateMap<Instrument, InstrumentViewModel>()
                .ReverseMap();

            cfg.CreateMap<InstrumentType, InstrumentTypeViewModel>()
                .ReverseMap();

            cfg.CreateMap<ScheduleFrequency, FrequencyViewModel>()
                .ReverseMap();

            cfg.CreateMap<ScheduleType, ScheduleTypeViewModel>()
                .ReverseMap();

            cfg.CreateMap<PlantArea, PlantAreaViewModel>()
                .ReverseMap();

            cfg.CreateMap<CertificateHeader, CertificateViewModel>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreNonExistentMembers()
                .ReverseMap();

            cfg.CreateMap<CertificateType, CertificateTypeViewModel>()      
                .ReverseMap();

            cfg.CreateMap<CertificateBody,  CertificateDetailViewModel>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ReverseMap();

            cfg.CreateMap<HttpPostedFileBase, byte[]>().ConvertUsing<ByteFileBaseConverter>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "It is used in CommonModelToViewModelToModel()")]
        internal class ByteFileBaseConverter : ITypeConverter<HttpPostedFileBase, byte[]>
        {
            public byte[] Convert(ResolutionContext context)
            {
                if (context == null || context.SourceValue == null) return new byte[0];

                byte[] returnFile;

                using (var memoryStream = new MemoryStream())
                {
                    ((HttpPostedFileBase)context.SourceValue).InputStream.CopyTo(memoryStream);
                    returnFile = memoryStream.ToArray();
                }

                return returnFile;
            }
        }
    }
}