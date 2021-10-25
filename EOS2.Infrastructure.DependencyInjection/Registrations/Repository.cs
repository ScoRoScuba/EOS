namespace EOS2.Infrastructure.DependencyInjection.Registrations
{
    using EOS2.Infrastructure.Interfaces.Repository;
    
    using EOS2.Model;
    using EOS2.Repository;

    using Microsoft.Practices.Unity;

    public static class Repository 
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IRepository<Organization>, Repository<Organization>>();
            container.RegisterType<IRepository<OrganizationRole>, Repository<OrganizationRole>>();
            container.RegisterType<IRepository<OrganizationRoleUser>, Repository<OrganizationRoleUser>>();

            container.RegisterType<IRepository<Site>, Repository<Site>>();
            container.RegisterType<IRepository<PlantArea>, Repository<PlantArea>>();
            container.RegisterType<IRepository<Equipment>, Repository<Equipment>>();
            container.RegisterType<IRepository<EquipmentType>, Repository<EquipmentType>>();
            container.RegisterType<IRepository<Instrument>, Repository<Instrument>>();
            container.RegisterType<IRepository<InstrumentType>, Repository<InstrumentType>>();
            container.RegisterType<IRepository<CalibrationFrequency>, Repository<CalibrationFrequency>>();
            container.RegisterType<IRepository<Schedule>, Repository<Schedule>>();
            container.RegisterType<IRepository<FurnaceClass>, Repository<FurnaceClass>>();
            container.RegisterType<IRepository<ScheduleFrequency>, Repository<ScheduleFrequency>>();
            container.RegisterType<IRepository<ScheduleType>, Repository<ScheduleType>>();
            container.RegisterType<IRepository<CertificateHeader>, Repository<CertificateHeader>>();
            container.RegisterType<IRepository<CertificateBody>, Repository<CertificateBody>>();
            container.RegisterType<IRepository<CertificateType>, Repository<CertificateType>>();

            container.RegisterType<IRepository<ChannelType>, Repository<ChannelType>>();      
            container.RegisterType<IRepository<Channel>, Repository<Channel>>();      
        }
    }
}
