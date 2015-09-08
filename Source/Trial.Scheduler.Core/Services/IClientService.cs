using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Core.Services
{
    public interface IClientService
    {
        ClientDto[] ListClients(PageParamsDto request);

        NewClientResponse AddClient(NewClientRequest request);

        void RemoveClient(RemoveClientRequest request);
    }
}