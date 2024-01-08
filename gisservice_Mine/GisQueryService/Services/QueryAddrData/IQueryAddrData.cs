using GisCommonService.Dtos;

namespace GisQueryService.Services
{
    public interface IQueryAddrData
    {
        ResultDTO<object> QueryAddrDataToQueue();
    }
}