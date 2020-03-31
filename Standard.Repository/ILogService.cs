using System;

namespace Repository
{
    public interface ILogService
    {
        public void LogInfo(string message);

        public void LogWarn(string message);

        public void LogError(Exception ex, string message = "");
    }
}