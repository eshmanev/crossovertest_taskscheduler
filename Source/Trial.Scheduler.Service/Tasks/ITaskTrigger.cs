using System;

namespace Trial.Scheduler.Service.Tasks
{
    public interface ITaskTrigger
    {
        DateTime NextTime { get; }
    }
}