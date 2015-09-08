using System;

namespace Trial.Scheduler.Service.Tasks
{
    public class ExactTimeTrigger : ITaskTrigger
    {
        public ExactTimeTrigger(int year, int month, int day, int hour, int minute)
        {
            NextTime = new DateTime(year, month, day, hour, minute, 0);
        }

        public ExactTimeTrigger(DateTime dateAndTime)
        {
            NextTime = dateAndTime;
        }

        public DateTime NextTime { get; private set; }
    }
}