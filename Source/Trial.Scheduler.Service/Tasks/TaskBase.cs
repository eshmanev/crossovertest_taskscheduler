using System;
using System.Collections.Generic;
using System.Linq;

namespace Trial.Scheduler.Service.Tasks
{
    public abstract class TaskBase : ITask
    {
        private readonly SortedList<DateTime, ITaskTrigger> _triggers = new SortedList<DateTime, ITaskTrigger>();
        private readonly object _lockObj = new object();
        public event EventHandler NextTimeChanged;
        
        public DateTime NextTime { get; private set; }

        public void AddTrigger(ITaskTrigger trigger)
        {
            lock (_lockObj)
            {
                _triggers.Add(trigger.NextTime, trigger);
                UpdateNextTime();
            }
        }

        public void RemoveTrigger(ITaskTrigger trigger)
        {
            lock (_lockObj)
            {
                int index = _triggers.IndexOfValue(trigger);
                if (index >= 0)
                {
                    _triggers.RemoveAt(index);
                    UpdateNextTime();    
                }
            }
        }

        public abstract void Execute();

        public void UpdateNextTime()
        {
            bool changed = false;

            lock (_lockObj)
            {
                var now = DateTime.Now;
                var array = _triggers.ToArray();
                for (var i = 0; i < array.Length; i++)
                {
                    var storedTime = _triggers.Keys[i];
                    var trigger = _triggers.Values[i];
                    var triggerTime = trigger.NextTime;

                    if (storedTime < triggerTime)
                    {
                        _triggers.Remove(storedTime);
                        _triggers.Add(triggerTime, trigger);
                    }

                    if (NextTime > triggerTime && triggerTime > now)
                    {
                        NextTime = triggerTime;
                        changed = true;
                    }

                    if (NextTime < now && NextTime < triggerTime)
                    {
                        NextTime = triggerTime;
                        changed = true;
                    }
                }
            }

            if (changed)
                OnNextTimeChanged();
        }

        private void OnNextTimeChanged()
        {
            var handler = NextTimeChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}