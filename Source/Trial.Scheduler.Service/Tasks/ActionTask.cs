using System;

namespace Trial.Scheduler.Service.Tasks
{
    public class ActionTask : TaskBase
    {
        private readonly Action _action;

        public ActionTask(Action action)
        {
            _action = action;
        }

        public override void Execute()
        {
            _action();
        }
    }
}