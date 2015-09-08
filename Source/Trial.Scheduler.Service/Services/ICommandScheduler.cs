using System.ServiceModel;
using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Service.Services
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ICommandScheduler
    {
        [OperationContract]
        [FaultContract(typeof(SchedulerErrorDto))]
        void ScheduleExecution(ScheduleCommandRequest request);

        [OperationContract]
        [FaultContract(typeof(SchedulerErrorDto))]
        ExecuteCommandResponse ExecuteCommand(ExecuteCommandRequest request);
    }
}