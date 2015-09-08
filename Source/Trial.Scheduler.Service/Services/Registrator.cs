using System.Diagnostics;
using System.ServiceModel;
using Trial.Scheduler.Core.Services;

namespace Trial.Scheduler.Service.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Registrator : ServiceBase, IRegistrator
    {
        private readonly ICallbackRegistry _registry;

        public Registrator(ICallbackRegistry registry)
        {
            _registry = registry;
        }

        public void RegisterClient(string clientName)
        {
            Trace.WriteLine("Registering client " + clientName);
        
            var callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            _registry.Register(clientName, callback);

            Trace.WriteLine("Completed.\r\n");
        }
    }
}