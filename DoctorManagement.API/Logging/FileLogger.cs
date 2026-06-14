
using System.Data;

namespace DoctorManagement.API.Logging
{
    public class FileLogger(string categoryName) : ILogger
    {
        public static object resourceLock = new object();
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var logMessage = formatter(state, exception);
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{DateTime.Now:yyMMdd}.log");
            lock (resourceLock) {
                FileStream? fs = null;
                if (!File.Exists(filePath)) {
                    using (fs = File.Create(filePath)) { } 
                }

                File.AppendAllLines(filePath, [$"At {DateTime.Now:dd-MMM-yyyy hh:mm tt} {categoryName} said: {logMessage}", $"{exception?.StackTrace}"]);
                if (!string.IsNullOrWhiteSpace(exception?.StackTrace))
                {
                    File.AppendAllLines(filePath, [$"Stack trace: {exception.StackTrace}"]);
                }
            }
        }
    }
}
