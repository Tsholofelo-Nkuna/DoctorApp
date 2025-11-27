namespace DoctorManagement.API.Logging
{
    public class FileLogProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
          return new FileLogger(categoryName);
        }

        public void Dispose()
        {
           
        }
    }
}
