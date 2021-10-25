namespace EOS2.Data.Migrations.ELMAHLogging
{
    using System.Data.Entity.Migrations;

    using EOS2.Data.Migrations.Contexts;

    internal sealed class Configuration : DbMigrationsConfiguration<ElmahLoggingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"ELMAHLogging";
        }
    }
}
