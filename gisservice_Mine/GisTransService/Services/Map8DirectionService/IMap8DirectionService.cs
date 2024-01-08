using GisCommonService.Dtos;
using GisTransService.Models.Params;
using Newtonsoft.Json.Linq;

namespace GisTransService.Services
{
    public interface IMap8DirectionService
    {
        ResultDTO<JObject> getMap8DirectionData(Map8DirectionParam param);
        ResultDTO<JObject> getMap8GeoCodeData(Map8GeoCodeParam param);
    }
}
