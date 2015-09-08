using Microsoft.Practices.Unity.Mvc;
using Trial.Scheduler.Admin.Config;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UnityWebActivator), "Start")]

namespace Trial.Scheduler.Admin.Config
{
    public static class UnityWebActivator
    {
        public static void Start()
        {
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }
    }
}