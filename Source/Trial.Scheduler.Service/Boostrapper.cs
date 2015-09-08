using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Trial.Scheduler.Core.Services;
using Trial.Scheduler.Core.Unity;
using Trial.Scheduler.Core.Validation;
using Trial.Scheduler.Service.Services;
using Trial.Scheduler.Service.Tasks;

namespace Trial.Scheduler.Service
{
    public class Bootstrapper : IDisposable
    {
        private readonly IUnityContainer _container = new UnityContainer();

        public IEnumerable<ServiceBase> GetServices()
        {
            yield return _container.Resolve<CommandScheduler>();
            yield return _container.Resolve<Registrator>();
        }

        public void Run()
        {
            _container.AddExtension(new SchedulerUnityExtension());
            _container
                .RegisterType<ICallbackRegistry, CallbackRegistry>(new ContainerControlledLifetimeManager())
                .RegisterType<IDictionary<string, IClientCallback>, ConcurrentDictionary<string, IClientCallback>>()
                .RegisterType<IValidatorLocator, ValidatorLocator>()
                .RegisterType<ITaskManager, TaskManager>(new ContainerControlledLifetimeManager())
                .RegisterType<ExecuteCommandRequestValidator>()
                .RegisterType<ScheduleCommandRequestValidator>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}