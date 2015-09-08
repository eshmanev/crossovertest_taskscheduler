using System;
using System.ServiceModel;
using Trial.Scheduler.Client.Service;

namespace Trial.Scheduler.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter a client name:");
                string clientName = Console.ReadLine();

                string configurationName = "NetTcpBinding_IRegistrator";
                Console.WriteLine("Please enter a configuration name: [Default: {0}]", configurationName);
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    configurationName = newName;

                var scheduler = new RegistratorClient(new InstanceContext(new CommandHandler()), configurationName);
                scheduler.RegisterClient(clientName);

                Console.WriteLine("Client application has been started successfully.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception e)
            {  
                Console.WriteLine(e.ToString());
            }
        }
    }
}
