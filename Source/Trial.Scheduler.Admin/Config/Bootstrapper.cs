using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Routing;
using FluentValidation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System;
using System.Web.Mvc;
using Trial.Scheduler.Admin.Controllers;
using Trial.Scheduler.Admin.Exceptions;
using Trial.Scheduler.Admin.Service;
using Trial.Scheduler.Admin.Wcf;
using Trial.Scheduler.Core.Services;
using Trial.Scheduler.Core.Unity;
using Trial.Scheduler.Core.Validation;

namespace Trial.Scheduler.Admin.Config
{
    public class Bootstrapper : IDisposable
    {
        private readonly IUnityContainer _container = new UnityContainer();

        public void Run()
        {
            _container.AddExtension(new SchedulerUnityExtension());

            ConfigureContainer();
            ConfigureMvc();
            GlobalConfiguration.Configure(ConfigureWebApi);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        private void ConfigureContainer()
        {
            _container
                .RegisterType<IClientService, ClientService>()
                .RegisterType<ICommandService, CommandService>()
                .RegisterType<ICommandScheduler, CommandSchedulerWrapper>()
                .RegisterType<IValidatorLocator, ValidatorLocator>()
                .RegisterType<IValidator, NewClientRequestValidator>("NewClientRequestValidator")
                .RegisterType<IValidator, NewCommandRequestValidator>("NewCommandRequestValidator")
                .RegisterType<IValidator, ScheduleRequestValidator>("ScheduleRequestValidator")
                ;
        }

        private void ConfigureMvc()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));

            RouteTable.Routes.MapRoute(
                "DefaultTemplate",
                "{template}.html",
                new {controller = "Layout", action = "Html", folder = "Templates", subfolder = string.Empty});

            RouteTable.Routes.MapRoute(
                "AngularTemplate",
                "template/{subfolder}/{template}.html",
                new {controller = "Layout", action = "Html", folder = "Angular", subfolder = string.Empty});

            RouteTable.Routes.MapRoute(
                "Default",
                "",
                new {controller = "Layout", action = "Index"}
                );
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(_container);
            config.MapHttpAttributeRoutes();
            config.EnableSystemDiagnosticsTracing();
            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());
        }
    }
}