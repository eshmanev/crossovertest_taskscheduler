using System.ServiceModel;
using System.ServiceProcess;

namespace Trial.Scheduler.Service.Host
{
    public class SchedulerServiceHost : ServiceBase
    {
        private readonly Core.Services.ServiceBase _service;
        private ServiceHost _serviceHost;

        public SchedulerServiceHost(Core.Services.ServiceBase service)
        {
            _service = service;
        }

        internal void StartInternal(string[] args)
        {
            OnStart(args);
        }

        internal void StopInternal()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            CloseHost();

            _serviceHost = new ServiceHost(_service);
            
            // _service instance is passed explicitly
            var behavior = _serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behavior.InstanceContextMode = InstanceContextMode.Single;

            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            base.OnStop();
            CloseHost();
        }

        private void CloseHost()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }
    }
}