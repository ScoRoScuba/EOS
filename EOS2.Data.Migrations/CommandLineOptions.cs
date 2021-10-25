namespace EOS2.Data.Migrations
{
    using System;

    using EOS2.Data.Migrations.Model;

    using Fclp;

    public class CommandLineOptions 
    {
        public CommandLineOptions()
        {
            IsValid = false;
            DropDatabase = false;
            ConnectionString = string.Empty;
            LogSQL = true;
        }

        public bool IsValid { get; private set; }

        public bool DropDatabase { get; private set; }

        public bool LogSQL { get; private set; }

        public bool PreviewOnly { get; private set; }

        public ConfigurationType Configuration { get; private set; }

        public string StartingMigration { get; private set; }

        public string TargetMigration { get; private set; }

        public bool HasConnectionString
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ConnectionString);
            }
        }

        public string ConnectionString { get; private set; }

        internal string ConfigurationType { get; private set; }

        public static CommandLineOptions GetCommandLineOptions(string[] args)
        {
            var parser = new FluentCommandLineParser<CommandLineOptions>();

            parser.Setup(arg => arg.ConfigurationType).As('d', "dataConfiguration").Required().WithDescription("Configuration to use for Migration");
            parser.Setup(arg => arg.TargetMigration).As('t', "targetMigration").WithDescription("Migration to apply (upgrade database to)");
            parser.Setup(arg => arg.ConnectionString).As('c', "connectionString").WithDescription("Connection String of DB to Migrate to");
            parser.Setup(arg => arg.LogSQL).As('l', "logSQL").WithDescription("Write the SQL out to log file (default is True");
            parser.Setup(arg => arg.DropDatabase).As('z', "dropDatabase").WithDescription("Drop Database in the connection string");
            parser.Setup(arg => arg.PreviewOnly).As('p', "previewOnly").WithDescription("Log the SQL to run, but do not perform migration");

            parser.SetupHelp(args);

            var parserResult = parser.Parse(args);

            if (parserResult.HasErrors)
            {
                return new CommandLineOptions() { IsValid = false };
            }

            var options = parser.Object;

            ConfigurationType parsedConfigurationType;
            if (Enum.TryParse(options.ConfigurationType, true, out parsedConfigurationType))
            {
                options.Configuration = parsedConfigurationType;
            } 
            else
            {
                var supportedConfigs = string.Join(", ", Enum.GetNames(typeof(ConfigurationType)));
                Console.WriteLine("Invalid data configuration. Supported configurations are {0}", supportedConfigs);

                return new CommandLineOptions() { IsValid = false };                
            }

            if (options.Configuration == Model.ConfigurationType.All && options.TargetMigration != null)
            {
                return new CommandLineOptions() { IsValid = false };
            }

            options.IsValid = true;

            return options;            
        }

        public void SetConfiguration(ConfigurationType configurationType)
        {
            this.Configuration = configurationType;

            // The below line will need to be commented back in if we split out to using multiple databases (as a single connection string input command would no longer be valid)
            // this.ConnectionString = string.Empty;
        }

        public CommandLineOptions Copy()
        {
            return new CommandLineOptions()
                       {
                           IsValid = this.IsValid,
                           DropDatabase = this.DropDatabase,
                           LogSQL = this.LogSQL,
                           PreviewOnly = this.PreviewOnly,
                           Configuration = this.Configuration,
                           StartingMigration = this.StartingMigration,
                           TargetMigration = this.TargetMigration,
                           ConnectionString = this.ConnectionString,
                           ConfigurationType = ConfigurationType
                       };
        }
    }
}
