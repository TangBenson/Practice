using System;
using System.Threading.Tasks;
using System.Data;
using GisCommonService.Utilities;
using GisCommonService.Dtos;
using GisCommonService.Params;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using GisQueryService.Services.AsyncDataServices;

namespace GisQueryService.Services
{
    public class QueryAddrData : IQueryAddrData
    {
        private readonly IConfiguration _config;
        private readonly IMessageBusClient _messageBusClient;

        public QueryAddrData(IConfiguration configuration, IMessageBusClient messageBusClient)
        {
            Console.WriteLine("QueryAddrData Init");
            _config = configuration;
            _messageBusClient = messageBusClient;
        }


        public ResultDTO<object> QueryAddrDataToQueue()
        {
            Console.WriteLine("QueryAddrDataToQueue Init");
            string message = "";
            string messageLevel = "";
            string messageType = "";
            DataSet queryDS = new DataSet();
            try
            {
                BaseParams param = new BaseParams();
                string[] serverInfo = _config.GetSection("AppSettings")["SQLString"].Trim().Split('|');
                param.SERVER_NAME = serverInfo[0];
                param.DATABASE_NAME = serverInfo[1];
                param.LOGIN = serverInfo[2];
                param.PASSWORD = serverInfo[3];
                param.DATABASE_TYPE = "S";
                param.SSAPI_ADDRESS = _config.GetSection("AppSettings")["SSAPI"].Trim();

                object[][] queryParam = {
                        new object[1]  { "0" }
                   };
                serverInfo = new ServerInfo(_config).GetServerInfo(param);

                queryDS = new WebApiClient(_config).SPExeBatchMultiArr2(serverInfo, "SP_MAPGETGPSLIST", queryParam, false, ref message, ref messageLevel, ref messageType);

                if (message != "")
                {
                    throw new Exception(message + "<br>" + JsonConvert.SerializeObject(queryParam));
                }

                if (queryDS.Tables[0].Rows.Count > 0)
                {
                    Console.WriteLine($"--> QueryData has get: {queryDS.Tables[0].Rows.Count.ToString()}");
                    for (int i = 0; i < queryDS.Tables[0].Rows.Count; i++)
                    {
                        var addr = new AddrParam()
                        {
                            SEQNO = int.Parse(queryDS.Tables[0].Rows[i]["SEQNO"].ToString()),
                            CITY = "",
                            POST = "",
                            ADDR = "",
                            ADDRESS = "",
                            Lat = queryDS.Tables[0].Rows[i]["Latitude"].ToString(),
                            Lng = queryDS.Tables[0].Rows[i]["Longitude"].ToString(),
                            TransType = "GpsToAddr"
                        };
                        _messageBusClient.SendRequestToGetGps(addr);
                    }
                }

                return (new ResultDTO<object>
                {
                    Message = "",
                    Data = { },
                    Result = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> QueryData has error: {ex.Message.Trim()}");
                return (new ResultDTO<object>
                {
                    Message = ex.Message,
                    Data = { },
                    Result = false
                });
            }

        }


    }
}
