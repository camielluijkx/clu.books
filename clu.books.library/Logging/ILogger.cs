using System;
using System.Threading.Tasks;

namespace clu.books.library.Logging
{
    public interface ILogger
    {
        Task LogDebugAsync(string logMessage);

        Task LogInfoAsync(string logMessage);

        Task LogWarningAsync(string logMessage);

        Task LogErrorAsync(string logMessage);

        Task LogExceptionAsync(Exception ex);
    }
}
