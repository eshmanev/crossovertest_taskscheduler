using System.ServiceModel;
using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Service.Services
{
    public interface IClientCallback
    {
        [OperationContract]
        ExecuteCommandResponse Execute(ExecuteCommandRequest request);
    }
}