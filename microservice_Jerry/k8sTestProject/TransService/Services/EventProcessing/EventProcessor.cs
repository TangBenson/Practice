using System;
using TransService.Services.AsyncDataService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TransService.Params;
using CommonService.Utilities;
using CommonService.Params;
using CommonService.Dtos;

namespace TransService.Services.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        public IConfiguration _config { get; }


        public EventProcessor(IConfiguration config)
        {
            _config = config;
        }
        
        public void ProcessEvent(string message)
        {
            Console.WriteLine($"--> Event Processing get data: {message}");

            //把message轉型成AddrParam
            AddrParam addrParam = JsonConvert.DeserializeObject<AddrParam>(message);
            //用get把內容組成的url丟給它，回傳json，並做utf8編碼
            OpenDataParam param = new OpenDataParam();
            param.Format = "JSON";
            param.Url = _config["TdxBaseApi"].Trim() + _config["TdxAddrToGPS"].Trim() + addrParam.ADDRESS + "?$format=JSON";
            param.Encoding = "utf-8";
            param.Method = "GET";
            param.PostData = "";

            ResultDTO<JObject> gpsResult = new ResultDTO<JObject>();
            gpsResult = new OpenDataService().GetOpenData(param);

            Console.WriteLine($"--> Event Processing get data: {JsonConvert.SerializeObject(gpsResult)}");//回來的內容作序列化成文字
            /*
            AddrParam param = JsonConvert.DeserializeObject<AddrParam>(message);
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
            */
        }
    }
}