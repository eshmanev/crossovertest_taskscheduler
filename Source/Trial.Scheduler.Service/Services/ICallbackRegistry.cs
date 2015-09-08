namespace Trial.Scheduler.Service.Services
{
    public interface ICallbackRegistry
    {
        void Register(string clientName, IClientCallback callback);

        IClientCallback Get(string clientName);
    }
}