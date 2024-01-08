using System;
using GisCommonService.Dtos;
using GisCommonService.Params;
using GisTransService.Models.Params;
using GisTransService.Services.AsyncDataServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GisTransService.Services.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        public IConfiguration _config { get; }
        private readonly IMap8DirectionService _map8DirectionService;
        private readonly IMessageBusClient _messageBusClient;

        public EventProcessor(IConfiguration config, IMap8DirectionService map8DirectionService, IMessageBusClient messageBusClient)
        {
            _config = config;
            _map8DirectionService = map8DirectionService;
            _messageBusClient = messageBusClient;
        }

        public void ProcessEvent(string message)
        {
            AddrParam param = JsonConvert.DeserializeObject<AddrParam>(message);
            Console.WriteLine($"--> Event Processing get data: {JsonConvert.SerializeObject(param)}");
            double lat = 0;
            double lng = 0;
            string TRANSRESULT = "";
            string city = "";
            string town = "";
            string addr = "";
            string address = "";
            try
            {
                Map8GeoCodeParam map8Param = new Map8GeoCodeParam();
                if(param.TransType=="AddrToGps"){
                    map8Param.address = param.ADDRESS;
                }else{
                    map8Param.address = "";
                    map8Param.latlng = param.Lat.ToString()+","+param.Lng.ToString();
                }
                ResultDTO<JObject> result = _map8DirectionService.getMap8GeoCodeData(map8Param);
                if (result.Result)
                {
                    if (result.Data["results"] != null)
                    {
                        Console.WriteLine($"--> Event Processing get data From map8: {JsonConvert.SerializeObject(result.Data["results"][0])}");
                        if(param.TransType=="AddrToGps"){
                            lat = double.Parse(result.Data["results"][0]["geometry"]["location"]["lat"].ToString());
                            lng = double.Parse(result.Data["results"][0]["geometry"]["location"]["lng"].ToString());
                        }else{
                            city=result.Data["results"][0]["city"].ToString();
                            town=result.Data["results"][0]["town"].ToString();
                            addr=result.Data["results"][0]["name"].ToString();
                            address=result.Data["results"][0]["formatted_address"].ToString();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"--> Event Processing get no result From map8");
                        TRANSRESULT = "no result From map8";
                    }
                }
                else
                {
                    Console.WriteLine($"--> Event Processing Call map8 Error:{result.Message}");
                    TRANSRESULT = result.Message;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Event Processing Call map8 Error:{ex.Message}");
                TRANSRESULT = ex.Message;
            }
            AddrSaveParam resultParam = new AddrSaveParam()
            {
                SEQNO = param.SEQNO,
                CITY = param.TransType=="AddrToGps"?param.CITY:city,
                POST = param.TransType=="AddrToGps"?param.POST:town,
                ADDR = param.TransType=="AddrToGps"?param.ADDR:addr,
                ADDRESS = param.TransType=="AddrToGps"?param.ADDRESS:address,
                lat = lat,
                lng = lng,
                TRANSRESULT = TRANSRESULT,
                TransType = param.TransType
            };
            _messageBusClient.SendRequestToSaveGis(resultParam);
        }
    }
}