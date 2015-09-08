How to configure:

1. Ensure standard Nuget gallery is used (http://www.nuget.org/api/v2)
2. Create a database Scheduler. If you choose a different name, then please change Trial.Scheduler.Admin/web.config
3. Run Cmd/grant_permissions.cmd as an administrator. This command will reserve URL addresses
4. Open Visual Studio and load solution Scheduler/Scheduler.sln
5. Restore Nuget packages
7. Build solution
8. Set Trial.Scheduler.Admin as startup project and run it
9. Run Scheduler/Trial.Scheduler.Service.Host/bin/debug/Trial.Scheduler.Service.Host.exe. This is a service responsible for communications
10. a) Run Scheduler/Trial.Scheduler.Client/bin/debug/Trial.Scheduler.Client.exe. This is a client application listening for commands
    b) Enter a client name, for instance, Client1. You can list all clients in web application.
    c) Press enter when program asks to enter a configuration name (default value will be used)
11. Go to web application
    a) Click "List commands"
    b) Click "Run" or "Schedule" againts a necessary client
    c) Be notified, that command is executed and command's output log is transferred through service application to web application
        
  
Feedback: Very good task, but I think 3 days is not enough to accomlish all goals with a good quality.
Notes: I haven't implemented some goals yet:
- There is no opportunity to edit/delete clients and commands. You can only create a new one
- Clients output and logs haven't be stored in database yet. But it can be easily achieved inroducing a necessary methods.
- Applications are not secured. What to do? I would add authentication at least and use SSL to secure traffic.
- Also I would implement WCF discovery mechanism or pulling to guarantee that client is connected; otherwise it can be disconnected automatically at the moment