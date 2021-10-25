namespace EOS2.Data.Migrations.Contexts
{
    using System;
    using System.Data.Entity;

    using EOS2.Data.Mappings.Logging;
    using EOS2.Model.Elmah;

    public class ElmahLoggingContext : DbContext
    {
        public ElmahLoggingContext()
        {
        }

        public ElmahLoggingContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<ElmahError> ElmahError { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException("modelBuilder");

            modelBuilder.Configurations.Add(new ElmahErrorMappings());

            base.OnModelCreating(modelBuilder);
        }        
    }
}
