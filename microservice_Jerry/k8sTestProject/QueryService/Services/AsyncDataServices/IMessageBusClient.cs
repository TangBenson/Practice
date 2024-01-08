namespace QueryService.Services.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void SendRequest(string message);
    }
}