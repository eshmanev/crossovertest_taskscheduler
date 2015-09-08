using System.ServiceModel;
using System.Threading.Tasks;
using Trial.Scheduler.Admin.Service;
using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Admin.Wcf
{
    public class CommandSchedulerWrapper : ICommandScheduler
    {
        private CommandSchedulerClient _client;

        public void ScheduleExecution(ScheduleCommandRequest request)
        {
            GetClient().ScheduleExecution(request);
        }

        public Task ScheduleExecutionAsync(ScheduleCommandRequest request)
        {
            return GetClient().ScheduleExecutionAsync(request);
        }

        public ExecuteCommandResponse ExecuteCommand(ExecuteCommandRequest request)
        {
            return GetClient().ExecuteCommand(request);
        }

        public Task<ExecuteCommandResponse> ExecuteCommandAsync(ExecuteCommandRequest request)
        {
            return GetClient().ExecuteCommandAsync(request);
        }

        private CommandSchedulerClient GetClient()
        {
            if (_client == null || _client.State == CommunicationState.Faulted)
                _client = new CommandSchedulerClient("NetTcpBinding_ICommandScheduler");

            return _client;
        }
    }
}