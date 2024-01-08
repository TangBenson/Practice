using GisCommonService.Params;

namespace GisQueryService.Services.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void SendRequestToGetGps(AddrParam param);
    }
}