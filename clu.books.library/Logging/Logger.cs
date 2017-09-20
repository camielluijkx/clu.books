using System;
using System.Collections.Generic;
using System.IO;
using clu.books.library.Extensions;
using clu.books.library.settings;
using Google.Api;
using Google.Cloud.Logging.Type;
using Google.Cloud.Logging.V2;
using System.Threading.Tasks;

namespace clu.books.library.Logging
{
    public class Logger : ILogger
    {
        private readonly LoggingServiceV2Client loggingService;

        private const string projectId = "books-179018";

        public Logger(IConfigurationSettings configurationSettings)
        {
            const string envKeyName = "GOOGLE_APPLICATION_CREDENTIALS";
            string currentEnvValue = Environment.GetEnvironmentVariable(envKeyName);
            if (string.IsNullOrEmpty(currentEnvValue) || !File.Exists(currentEnvValue))
            {
                string keyFile = configurationSettings.ServiceAccountPrivateKeyFile;
                Environment.SetEnvironmentVariable(envKeyName, keyFile);
            }

            loggingService = LoggingServiceV2Client.Create();
        }

        private async Task LogAsync(string logMessage, LogSeverity logSeverity)
        {
            // Prepare new log entry.
            LogEntry logEntry = new LogEntry();
            string logId = "clu-books-log";
            LogName logName = new LogName(projectId, logId);
            LogNameOneof logNameToWrite = LogNameOneof.From(logName);
            logEntry.LogName = logName.ToString();
            logEntry.Severity = logSeverity;

            // Create log entry message.
            string messageId = DateTime.Now.Millisecond.ToString();
            string entrySeverity = logEntry.Severity.ToString().ToUpper();
            logEntry.TextPayload =
                $"{messageId} {entrySeverity} - {logMessage}";

            // Set the resource type to control which GCP resource the log entry belongs to.
            // See the list of resource types at:
            // https://cloud.google.com/logging/docs/api/v2/resource-list
            // This sample uses resource type 'global' causing log entries to appear in the
            // "Global" resource list of the Developers Console Logs Viewer:
            //  https://console.cloud.google.com/logs/viewer
            MonitoredResource resource = new MonitoredResource {Type = "global"};

            // Add log entry to collection for writing. Multiple log entries can be added.
            IEnumerable<LogEntry> logEntries = new[] { logEntry };

            // Create dictionary object to add custom labels to the log entry.
            IDictionary<string, string> entryLabels = new Dictionary<string, string>();
            entryLabels.Add("size", "large");
            entryLabels.Add("color", "red");

            // Write new log entry.
            await loggingService.WriteLogEntriesAsync(logNameToWrite, resource, entryLabels, logEntries);
        }

        public async Task LogDebugAsync(string logMessage)
        {
            await LogAsync(logMessage, LogSeverity.Debug);
        }

        public async Task LogInfoAsync(string logMessage)
        {
            await LogAsync(logMessage, LogSeverity.Info);
        }

        public async Task LogWarningAsync(string logMessage)
        {
            await LogAsync(logMessage, LogSeverity.Warning);
        }

        public async Task LogErrorAsync(string logMessage)
        {
            await LogAsync(logMessage, LogSeverity.Error);
        }

        public async Task LogExceptionAsync(Exception ex)
        {
            await LogAsync(ex.ToExceptionMessage(), LogSeverity.Error);
        }
    }
}
