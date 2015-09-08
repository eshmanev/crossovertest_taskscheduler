using System;

namespace Trial.Scheduler.Service.Tasks
{
    public interface ITask
    {
        DateTime NextTime { get; }
     
        event EventHandler NextTimeChanged;

        void UpdateNextTime();

        void AddTrigger(ITaskTrigger trigger);
        
        void RemoveTrigger(ITaskTrigger trigger);
        
        void Execute();
    }
}