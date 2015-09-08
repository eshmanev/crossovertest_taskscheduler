using System;
using System.Diagnostics;

namespace Trial.Scheduler.Contracts.Logging
{
    public class NullLogger : ILogger
    {
        public static readonly ILogger Instance  = new NullLogger();

        public void LogError(Exception exception, string message)
        {
            Trace.TraceError(message);
        }
    }
}