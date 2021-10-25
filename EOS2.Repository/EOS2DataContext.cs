namespace EOS2.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Model;

    public class EOS2DataContext : DbContext, IDataContext
    {
        public EOS2DataContext()
        {
        }

        public EOS2DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        #region DbSets
        public IDbSet<CertificateHeader> CertificateHeaders { get; set; }

        public IDbSet<CertificateBody> CertificateBodies { get; set; }

        public DbSet<CalibrationFrequency> CalibrationFrequency { get; set; }

        public DbSet<CertificateType> CertificateTypes { get; set; }
        
        public DbSet<Equipment> Equipment { get; set; }

        public DbSet<EquipmentType> EquipmentTypes { get; set; }

        public DbSet<FurnaceClass> FurnaceClasses { get; set; }

        public DbSet<Instrument> Instruments { get; set; }

        public DbSet<InstrumentType> InstrumentTypes { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<OrganizationRole> OrganizationRoles { get; set; }

        public DbSet<OrganizationRoleUser> OrganizationRoleUsers { get; set; }

        public DbSet<PlantArea> PlantAreas { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<ScheduleFrequency> Frequencies { get; set; }

        public DbSet<ScheduleType> ScheduleTypes { get; set; }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<ChannelType> ChannelTypes { get; set; }

        #endregion

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry<TEntity>(entity);
        }

        public new DbEntityEntry Entry(object entity)
        {
          return base.Entry(entity);
        }

        public IEnumerable<DbEntityEntry> GetChangeTrackerEntries()
        {
            return ChangeTracker.Entries();
        }

        public void Update<T>(T entityToUpdate) where T : class
        {
            Set<T>().Attach(entityToUpdate);
            Entry(entityToUpdate).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException("modelBuilder");

            modelBuilder.Conventions.Add(new DateTime2Convention());

            // Dont use '_' as part of foreign key relationship
            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());

            TableSplitting(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void TableSplitting(DbModelBuilder modelBuilder)
        {
            // Certificates
            modelBuilder.Entity<CertificateHeader>().ToTable("CertificateDetails");
            modelBuilder.Entity<CertificateBody>().ToTable("CertificateDetails");
            modelBuilder.Entity<CertificateHeader>().HasRequired(u => u.CertificateBody).WithRequiredPrincipal(a => a.CertificateHeader).WillCascadeOnDelete(true);
        }

        // Provides a convention for fixing the independent association (IA) foreign key column names.  
        private class ForeignKeyNamingConvention : IStoreModelConvention<AssociationType>
        {
            public void Apply(AssociationType item, DbModel model)
            {
                // Identify ForeignKey properties (including IAs)  
                if (item != null && item.IsForeignKey)
                {
                    // rename FK columns  
                    var constraint = item.Constraint;
                    if (DoPropertiesHaveDefaultNames(constraint.FromProperties,  constraint.ToProperties))
                    {
                        NormalizeForeignKeyProperties(constraint.FromProperties);
                    }

                    if (DoPropertiesHaveDefaultNames(constraint.ToProperties,  constraint.FromProperties))
                    {
                        NormalizeForeignKeyProperties(constraint.ToProperties);
                    }
                }
            }
       
            private static bool DoPropertiesHaveDefaultNames(ReadOnlyMetadataCollection<EdmProperty> properties,  ReadOnlyMetadataCollection<EdmProperty> otherEndProperties)
            {
                if (properties.Count != otherEndProperties.Count)
                {
                    return false;
                }

                for (int i = 0; i < properties.Count; ++i)
                {
                    if (!properties[i].Name.EndsWith("_" + otherEndProperties[i].Name))
                    {
                        return false;
                    }
                }

                return true;
            }

            private static void NormalizeForeignKeyProperties(ReadOnlyMetadataCollection<EdmProperty> properties)
            {
                for (int i = 0; i < properties.Count; ++i)
                {
                    int underscoreIndex = properties[i].Name.IndexOf('_');
                    if (underscoreIndex > 0)
                    {
                        properties[i].Name = properties[i].Name.Remove(underscoreIndex, 1);
                    }
                }
            }
        }
    }
}
