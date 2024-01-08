using GisCommonService.Params;

namespace GisTransService.Services.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void SendRequestToSaveGis(AddrSaveParam param);
    }
}