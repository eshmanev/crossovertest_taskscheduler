using System.Linq;
using Trial.Scheduler.Contracts.Dto;
using Trial.Scheduler.Core.Model;

namespace Trial.Scheduler.Core.Services
{
    public class ClientService : ServiceBase, IClientService
    {
        // normally it should be replaced with pair of interfaces (Repository/UnitOfWork) for testability purposes
        private readonly DataModel _dataModel;

        public ClientService(DataModel dataModel)
        {
            _dataModel = dataModel;
        }

        public ClientDto[] ListClients(PageParamsDto request)
        {
            return this.HandleAndReturn(request, ListClientsCore);
        }

        public NewClientResponse AddClient(NewClientRequest request)
        {
            return this.HandleAndReturn(request, AddClientCore);
        }

        public void RemoveClient(RemoveClientRequest request)
        {
            this.Handle(request, RemoveClientCore);
        }

        private ClientDto[] ListClientsCore(PageParamsDto request)
        {
            return _dataModel.Client
                .OrderBy(x => x.Id)
                .Skip(request.Start)
                .Take(request.Count)
                .Select(x => new ClientDto {ClientId = x.Id, Name = x.Name, Address = x.Address})
                .ToArray();
        }

        private NewClientResponse AddClientCore(NewClientRequest request)
        {
            var entity = new Client {Name = request.Name, Address = request.Address};
            _dataModel.Client.Add(entity);
            _dataModel.SaveChanges();
            return new NewClientResponse {ClientId = entity.Id};
        }

        private void RemoveClientCore(RemoveClientRequest request)
        {
            var entity = _dataModel.Client.FirstOrDefault(x => x.Id == request.ClientId);
            if (entity != null)
            {
                _dataModel.Client.Remove(entity);
                _dataModel.SaveChanges();
            }
        }
    }
}