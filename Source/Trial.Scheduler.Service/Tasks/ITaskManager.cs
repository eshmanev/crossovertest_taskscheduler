using System;
using System.Collections.Generic;

namespace Trial.Scheduler.Service.Tasks
{
    public interface ITaskManager : IDisposable
    {
        void Run();
        void Stop();
        int GetTaskCount();
        void AddTask(ITask task);
        void RemoveTask(ITask task);
        IEnumerable<ITask> GetTasks();
    }
}