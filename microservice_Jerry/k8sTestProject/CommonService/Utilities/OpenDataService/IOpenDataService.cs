using Newtonsoft.Json.Linq;
using CommonService.Params;
using CommonService.Dtos;

namespace CommonService.Utilities.Interfaces
{
    public interface IOpenDataService
    {
        ResultDTO<JObject> GetOpenData(OpenDataParam param);
    }
}