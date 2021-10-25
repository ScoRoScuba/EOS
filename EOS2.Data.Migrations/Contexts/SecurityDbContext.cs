namespace EOS2.Data.Migrations.Contexts
{
    using System;
    using System.Data.Entity;

    using EOS2.Data.Mappings.Security;
    using EOS2.Identity.Repository;

    public class SecurityDbContext : EOSIdentityDbContext 
    {
        public SecurityDbContext()
        {
        }

        public SecurityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException("modelBuilder");

            modelBuilder.Configurations.Add(new RoleMappings());
            modelBuilder.Configurations.Add(new UserClaimMappings());
            modelBuilder.Configurations.Add(new UserLoginMappings());
            modelBuilder.Configurations.Add(new UserMappings());
            modelBuilder.Configurations.Add(new UserRoleMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
