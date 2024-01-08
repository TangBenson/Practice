using GisCommonService.Dtos;

namespace GisQueryService.Services
{
    public interface IQueryGisData
    {
         ResultDTO<object> QueryGisDataToQueue();
    }
}