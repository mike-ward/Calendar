using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;

namespace BlueOnion
{
    internal static class Log
    {
        public static void Error(string message)
        {
            WriteEntry(message, EventLogEntryType.Error);
        }

        public static void Warning(string message)
        {
            WriteEntry(message, EventLogEntryType.Warning);
        }

        public static void Information(string message)
        {
            WriteEntry(message, EventLogEntryType.Information);
        }

        private static void WriteEntry(string message, EventLogEntryType eventType)
        {
            if (EventLog.SourceExists(InstallEventLog.EventSource))
            {
                EventLog.WriteEntry(InstallEventLog.EventSource, message, eventType);
            }
        }
    }

    [RunInstaller(true)]
    public class InstallEventLog : Installer
    {
        public const string EventSource = "Calendar";

        public InstallEventLog()
        {
            var eventLogInstaller = new EventLogInstaller();
            eventLogInstaller.Source = EventSource;
            Installers.Add(eventLogInstaller);
        }
    }
}