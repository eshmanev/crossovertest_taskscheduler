using System;

namespace Trial.Scheduler.Service.Tasks
{
    public class DailyTrigger : ITaskTrigger
    {
        private readonly TimeSpan _time;

        public DailyTrigger(TimeSpan time)
        {
            _time = time;
        }

        public DateTime NextTime
        {
            get
            {
                var current = DateTime.Now;
                var next = new DateTime(current.Year, current.Month, current.Day, _time.Hours, _time.Minutes, _time.Seconds);
                return current.TimeOfDay <= _time ? next : next.AddDays(1);
            }
        }
    }
}