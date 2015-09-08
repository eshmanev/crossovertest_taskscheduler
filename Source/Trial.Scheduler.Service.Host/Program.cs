using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace Trial.Scheduler.Service.Host
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (var bootstrapper = new Bootstrapper())
            {
                bootstrapper.Run();
                var hosts = bootstrapper.GetServices().Select(x => new SchedulerServiceHost(x)).ToArray();

                Trace.Listeners.Clear();
                Trace.Listeners.Add(new ConsoleTraceListener());

                if (Environment.UserInteractive)
                {
                    hosts.ForEach(x => x.StartInternal(args));
                    
                    Console.WriteLine("Scheduler service has been started.");
                    Console.WriteLine("Press any key to stop service and exit.");
                    Console.ReadLine();

                    hosts.ForEach(x => x.StopInternal());
                }
                else
                {
                    ServiceBase.Run(hosts.Cast<ServiceBase>().ToArray());
                }
            }
        }

        private static void ForEach(this IEnumerable<SchedulerServiceHost> hosts, Action<SchedulerServiceHost> action)
        {
            foreach (var host in hosts)
                action(host);
        }
    }

}
