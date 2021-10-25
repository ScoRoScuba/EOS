namespace EOSEventLogInstaller
{
    using System;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.Diagnostics;

    /// <summary>
    /// Run InstallUtil tool. Can be found here: C:\Windows\Microsoft.NET\Framework64\v2.0.50727
    /// installutil /LogFile=<Path to log file>.InstallLog /AssemblyName "EOS2.log4NetEventLogInstaller"
    /// 
    /// Ref1. http://msdn.microsoft.com/en-us/library/50614e95(VS.85).aspx
    /// Ref2. http://msdn.microsoft.com/en-us/library/system.diagnostics.eventloginstaller(VS.85).aspx
    /// 
    /// example cmd line
    /// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\installUtil /LogFile=c:\temp\eos2.install.log EOSEventLogInstaller.exe
    /// </summary>

    [RunInstaller(true)]
    [CLSCompliant(true)]
    public class EventLogInstaller : Installer
    {
        private System.Diagnostics.EventLogInstaller eosEventLogInstaller;

        public EventLogInstaller()
        {
            // Create an instance of an EventLogInstaller.
            this.eosEventLogInstaller = new System.Diagnostics.EventLogInstaller();

            // Set the source name of the event log.
            this.eosEventLogInstaller.Source = "EOS2-Application";

            // Set the event log that the source writes entries to.
            this.eosEventLogInstaller.Log = "Application";

            // Add eosEventLogInstaller to the Installer collection.
            Installers.Add(this.eosEventLogInstaller);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "myInstaller", Justification = "Installer Setup")]
        public static void Main()
        {
            using (EventLogInstaller installer = new EventLogInstaller())
            {                
            }
        }
    }
}
