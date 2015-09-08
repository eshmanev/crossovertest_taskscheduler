using System.Collections.Concurrent;
using System.ServiceModel;
using Trial.Scheduler.Contracts.Dto;

namespace Trial.Scheduler.Service.Services
{
    public class CallbackRegistry : ICallbackRegistry
    {
        private readonly ConcurrentDictionary<string, IClientCallback> _clients = new ConcurrentDictionary<string, IClientCallback>();

        public void Register(string clientName, IClientCallback callback)
        {
            Cleanup();

            if (_clients.ContainsKey(clientName))
                throw new FaultException<SchedulerErrorDto>(new SchedulerErrorDto { Message = string.Format("Client {0} has already registered.", clientName) });

            _clients[clientName] = callback;
        }

        public IClientCallback Get(string clientName)
        {
            Cleanup();
            
            IClientCallback callback;
            if (!_clients.TryGetValue(clientName, out callback))
                throw new FaultException(new FaultReason("Unable to find a client."));

            return callback;
        }

        private void Cleanup()
        {
            foreach (var pair in _clients.ToArray())
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                var communication = (ICommunicationObject)pair.Value;
                if (communication.State != CommunicationState.Opened)
                {
                    IClientCallback tmp;
                    _clients.TryRemove(pair.Key, out tmp);
                }
            }
        }
    }
}