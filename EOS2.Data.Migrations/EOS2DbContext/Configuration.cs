namespace EOS2.Data.Migrations.EOS2DbContext
{
    using System;
    using System.Data.Entity.Migrations;

    using EOS2.Data.Migrations.Contexts;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "Tool Generated COde, outside scope of these checks")]
    public sealed class Configuration : DbMigrationsConfiguration<EOS2DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"EOS2DbContext";
        }

        protected override void Seed(EOS2DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
        }
    }
}
