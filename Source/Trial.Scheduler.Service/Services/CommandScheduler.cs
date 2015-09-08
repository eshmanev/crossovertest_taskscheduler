using System;
using System.Diagnostics;
using System.ServiceModel;
using Trial.Scheduler.Contracts.Dto;
using Trial.Scheduler.Core.Services;
using Trial.Scheduler.Service.Tasks;

namespace Trial.Scheduler.Service.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CommandScheduler : ServiceBase, ICommandScheduler
    {
        private readonly ICallbackRegistry _registry;
        private readonly ITaskManager _taskManager;

        public CommandScheduler(ICallbackRegistry registry, ITaskManager taskManager)
        {
            _registry = registry;
            _taskManager = taskManager;
        }

        public void ScheduleExecution(ScheduleCommandRequest request)
        {
            this.Handle(request, ScheduleExecutionCore);
        }

        private void ScheduleExecutionCore(ScheduleCommandRequest request)
        {
            Trace.WriteLine(string.Format("Scheduling command {0} on {1}", request.Command.CommandText, request.Command.ClientName));

            var task = new ActionTask(() => ExecuteCommandCore(request.Command));
            switch (request.Trigger)
            {
                case Trigger.Single:
                    task.AddTrigger(new ExactTimeTrigger(request.FirstDateTime));
                    break;

                case Trigger.Daily:
                    task.AddTrigger(new DailyTrigger(request.FirstDateTime.TimeOfDay));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            _taskManager.AddTask(task);

            Trace.WriteLine("Command will be executed at {0}\r\n" + task.NextTime);
        }

        public ExecuteCommandResponse ExecuteCommand(ExecuteCommandRequest request)
        {
            return this.HandleAndReturn(request, ExecuteCommandCore);
        }

        private ExecuteCommandResponse ExecuteCommandCore(ExecuteCommandRequest request)
        {
            Trace.WriteLine(string.Format("Executing command {0} on {1}", request.CommandText , request.ClientName));
            
            IClientCallback callback = _registry.Get(request.ClientName);
            var response = callback.Execute(request);

            Trace.WriteLine(string.Format("Command completed. Output: {0}\r\n", response.CommandOutput));
            return response;
        }
    }
}