using System;

namespace Repository
{
    public class LogService : ILogService
    {
        public void LogError(Exception ex, string message = "")
        {
            Console.WriteLine($"{ex.ToString()} - {message}");
        }

        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

        public void LogWarn(string message)
        {
            Console.WriteLine(message);
        }
    }
}