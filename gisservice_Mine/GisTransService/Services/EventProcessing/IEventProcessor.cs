namespace GisTransService.Services.EventProcessing
{
    public interface IEventProcessor
    {
         void ProcessEvent(string message);
    }
}