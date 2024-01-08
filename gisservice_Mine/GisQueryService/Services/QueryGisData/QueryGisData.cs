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
    public class QueryGisData: IQueryGisData
    {         
        private readonly IConfiguration _config;        
        private readonly IMessageBusClient _messageBusClient;

        public QueryGisData(IConfiguration configuration, IMessageBusClient messageBusClient)
        {
            Console.WriteLine("QueryGisData Init");
            _config = configuration;
            _messageBusClient = messageBusClient;
        }


        public ResultDTO<object> QueryGisDataToQueue()
        {
            Console.WriteLine("QueryGisDataToQueue Init");
            string message = "";
            string messageLevel = "";
            string messageType = "";
            DataSet queryDS = new DataSet();
            try
            {
                BaseParams param = new BaseParams();
                string[] serverInfo = _config.GetSection("AppSettings")["SQLString"].Trim().Split('|');
                //string[] serverInfo = new string[4];
                param.SERVER_NAME = serverInfo[0];
                param.DATABASE_NAME = serverInfo[1];
                param.LOGIN = serverInfo[2];
                param.PASSWORD = serverInfo[3];
                param.DATABASE_TYPE = "S";
                param.SSAPI_ADDRESS = _config.GetSection("AppSettings")["SSAPI"].Trim();

                object[][] queryParam = {
                        new object[1]  { "" }
                   };

                serverInfo=new ServerInfo(_config).GetServerInfo(param);
                
                queryDS = new WebApiClient(_config).SPExeBatchMultiArr2(serverInfo, "SP_iRentAddr_TO_GPS", queryParam, false , ref message, ref messageLevel, ref messageType);

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
                            SEQNO = 0,//int.Parse(queryDS.Tables[0].Rows[i]["SEQNO"].ToString()),
                            CITY = queryDS.Tables[0].Rows[i]["city"].ToString(),
                            POST = queryDS.Tables[0].Rows[i]["post"].ToString(),
                            ADDR = queryDS.Tables[0].Rows[i]["ADDR"].ToString(),
                            ADDRESS = queryDS.Tables[0].Rows[i]["ADDR"].ToString(),
                            Lat="0",
                            Lng="0",
                            TransType = "AddrToGps"
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
