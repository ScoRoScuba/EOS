namespace EOS2.Data.Migrations.SecurityContext
{
    using System;
    using System.Data.Entity.Migrations;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "Tool Generated COde, outside scope of these checks")]
    public sealed class Configuration : DbMigrationsConfiguration<Contexts.SecurityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"SecurityContext";
        }

        protected override void Seed(Contexts.SecurityDbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
        }
    }
}
