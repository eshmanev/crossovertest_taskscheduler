using System;

namespace Trial.Scheduler.Contracts.Logging
{
    public interface ILogger
    {
        void LogError(Exception exception, string message);
    }
}