namespace EOS2.Data.Migrations.Contexts
{
    using System;
    using System.Data.Entity;

    using EOS2.Data.Mappings.EOS2Model;
    using EOS2.Repository;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "Tool Generated COde, outside scope of these checks")]
    public class EOS2DbContext : EOS2DataContext
    {
        public EOS2DbContext()
        {
        }

        public EOS2DbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException("modelBuilder");

            modelBuilder.Configurations.Add(new EquipmentMappings());
            modelBuilder.Configurations.Add(new FurnaceClassMappings());
            modelBuilder.Configurations.Add(new InstrumentMappings());            
            modelBuilder.Configurations.Add(new OrganizationMappings());
            modelBuilder.Configurations.Add(new OrganizationRoleMappings());
            modelBuilder.Configurations.Add(new PlantAreaMappings());
            modelBuilder.Configurations.Add(new SiteMappings());

            modelBuilder.Configurations.Add(new ChannelMapping());
            modelBuilder.Configurations.Add(new ChannelTypeMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
