using System.ServiceModel;
using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Service.Services
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClientCallback))]
    public interface IRegistrator
    {
        [OperationContract]
        [FaultContract(typeof(SchedulerErrorDto))]
        void RegisterClient(string clientName);
    }
}