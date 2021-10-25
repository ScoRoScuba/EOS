namespace EOS2.Data.Migrations
{
    using System;
    using System.Configuration;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using EOS2.Data.Migrations.Model;
    using EOS2.Repository;

    public static class Program
    {
        private static readonly string FilePath = ConfigurationManager.AppSettings["SQLLogFolder"];

        public static void Main(string[] args)
        {
            var commandParameters = CommandLineOptions.GetCommandLineOptions(args);
            if (commandParameters.IsValid)
            {
                ProcessCommandParameters(commandParameters);

                Console.WriteLine("Migration Complete");
            }
        }

        private static void ProcessCommandParameters(CommandLineOptions commandParameters)
        {
            CreateSQLLogFolder(commandParameters);

            DropDatabase(commandParameters);

            ProcessConfigurationChoice(commandParameters);

            SeedDatabase(commandParameters);
        }

        private static void ProcessConfigurationChoice(CommandLineOptions commandParameters)
        {
            switch (commandParameters.Configuration)
            {
                case ConfigurationType.All:
                    {
                        var localParameters = commandParameters.Copy();

                        foreach (var db in Enum.GetValues(typeof(ConfigurationType)).Cast<object>().Where(db => (ConfigurationType)db != ConfigurationType.All))
                        {
                            localParameters.SetConfiguration((ConfigurationType)db);

                            MigrateDatabase(localParameters);
                        }

                        break;
                    }

                default:
                    { 
                        MigrateDatabase(commandParameters);
                        break;
                    }
            }
        }

        private static void MigrateDatabase(CommandLineOptions commandParameters)
        {
            var configuration = ConfigurationContextBuilder.GetConfiguration(commandParameters.Configuration);

            configuration.TargetDatabase = GetMigrationDbConnectionInfo(commandParameters);

            var migrator = new DbMigrator(configuration);

            WriteSQLScript(commandParameters, configuration);

            if (!commandParameters.PreviewOnly)
            {
                Console.WriteLine("    Migrating Database");
                migrator.Update(commandParameters.TargetMigration);
                Console.WriteLine("    Database Migrated");
            }

            Console.WriteLine("  Database Complete");
        }

        private static void WriteSQLScript(CommandLineOptions commandParameters, DbMigrationsConfiguration configuration)
        {
            if (!commandParameters.LogSQL && !commandParameters.PreviewOnly)
            {
                return;
            }

            Console.WriteLine("    Creating SQL script");

            // This will need its own migrator as we dont want to clear out the actual one for migration.
            var migratorLog = new DbMigrator(configuration);

            var filename = FilePath + DateTime.Now.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture) + "_" + commandParameters.ConfigurationType +
                           "_" + commandParameters.Configuration + (commandParameters.PreviewOnly ? "_p" : string.Empty) + ".txt";
            try
            {
                using (var fileStream = CreateFileStream(filename))
                {
                    TextWriter oldOut = Console.Out;

                    var writer = new StreamWriter(fileStream);

                    Console.SetOut(writer);

                    Console.WriteLine("--Connection String: {0}", commandParameters.HasConnectionString ? commandParameters.ConnectionString : ConfigurationManager.ConnectionStrings[commandParameters.Configuration.ToString()].ConnectionString);

                    var scriptor = new MigratorScriptingDecorator(migratorLog);

                    var script = scriptor.ScriptUpdate(commandParameters.StartingMigration, commandParameters.TargetMigration);

                    Console.WriteLine(script);

                    Console.SetOut(oldOut);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Cannot open {0} for writing", filename);
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine("    Script saved to {0}", filename);
        }

        private static ConnectionStringSettings GetMigrationConnectionStringInformation(CommandLineOptions commandParameters)
        {
            if (commandParameters.HasConnectionString)
            {
                return new ConnectionStringSettings
                            {
                                ConnectionString = commandParameters.ConnectionString,
                                ProviderName = "System.Data.SQLClient"
                            };
            }
            else
            {
                return ConfigurationManager.ConnectionStrings[commandParameters.ConfigurationType];
            }
        }

        private static DbConnectionInfo GetMigrationDbConnectionInfo(CommandLineOptions commandParameters)
        {
            var connectionInfo = GetMigrationConnectionStringInformation(commandParameters);

            return new DbConnectionInfo(connectionInfo.ConnectionString, connectionInfo.ProviderName);
        }

        private static void DropDatabase(CommandLineOptions commandParameters)
        {
            if (commandParameters.DropDatabase && commandParameters.Configuration == ConfigurationType.All)
            {
                Console.WriteLine("    Initializing Database");
                DatabaseInitializer.Initialize(commandParameters.HasConnectionString ? commandParameters.ConnectionString : commandParameters.Configuration.ToString());
                Console.WriteLine("    Initialization Complete");
            }
        }

        private static void CreateSQLLogFolder(CommandLineOptions commandParameters)
        {
            if (commandParameters.LogSQL || commandParameters.PreviewOnly)
            {
                var directoryInfo = (new FileInfo(FilePath)).Directory;
                if (directoryInfo != null)
                {
                    directoryInfo.Create();
                }
            }
        }

        private static void SeedDatabase(CommandLineOptions commandParameters)
        {
            if (commandParameters.Configuration == ConfigurationType.All)
            {
                Console.WriteLine(" Start Seeding Database");

                DataSeeding.DataSeeding.ConnectionStringInfo = GetMigrationConnectionStringInformation(commandParameters);

                DataSeeding.DataSeeding.ReferenceData();

                DataSeeding.DataSeeding.SeedSecurityData();

                DataSeeding.DataSeeding.EOS2Data();

                Console.WriteLine("Total Seed Time : {0}:{1}:{2}", DataSeeding.DataSeeding.TotalSeedTime.Hours, DataSeeding.DataSeeding.TotalSeedTime.Minutes, DataSeeding.DataSeeding.TotalSeedTime.Seconds);

                Console.WriteLine(" Finisihed Seeding Database");
            }
        }

        private static FileStream CreateFileStream(string newFilename)
        {
            return new FileStream(newFilename, FileMode.OpenOrCreate, FileAccess.Write);
        }
    }
}
