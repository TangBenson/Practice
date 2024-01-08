using System;
using System.Data;
using GisCommonService.Dtos;
using GisCommonService.Params;
using GisCommonService.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GisSaveService.Services.EventProcessing
{
    public class EventProcessor: IEventProcessor
    {
        public IConfiguration _config { get; }

        public EventProcessor(IConfiguration config)
        {
            _config = config;
        }

        public void ProcessEvent(string messageQ)
        {
            AddrSaveParam addrParam = JsonConvert.DeserializeObject<AddrSaveParam>(messageQ);
            Console.WriteLine($"--> Event Processing get data: {JsonConvert.SerializeObject(addrParam)}");

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

                if(addrParam.TransType=="AddrToGps"){
                    object[][] saveParam = {
                            new object[7]  
                            { 
                                addrParam.SEQNO.ToString(),
                                addrParam.lat.ToString(),
                                addrParam.lng.ToString(),
                                addrParam.TRANSRESULT,
                                addrParam.CITY,
                                addrParam.POST,
                                addrParam.ADDR
                            }
                    };
                    serverInfo=new ServerInfo(_config).GetServerInfo(param);
                    
                    queryDS = new WebApiClient(_config).SPExeBatchMultiArr2(serverInfo, "SP_MAPSAVEGPS", saveParam, true , ref message, ref messageLevel, ref messageType);
                    if (message != "")
                    {
                        throw new Exception(message + "<br>" + JsonConvert.SerializeObject(saveParam));
                    }
                }else{
                    object[][] saveParam1 = {
                            new object[6]  
                            { 
                                addrParam.SEQNO.ToString(),
                                addrParam.CITY,
                                addrParam.POST,
                                addrParam.ADDR,
                                addrParam.ADDRESS,
                                addrParam.TRANSRESULT
                            }
                    };
                    serverInfo=new ServerInfo(_config).GetServerInfo(param);
                    
                    queryDS = new WebApiClient(_config).SPExeBatchMultiArr2(serverInfo, "SP_MAPSAVEADDR", saveParam1, true , ref message, ref messageLevel, ref messageType);
                    if (message != "")
                    {
                        throw new Exception(message + "<br>" + JsonConvert.SerializeObject(saveParam1));
                    }
                }


                if (queryDS.Tables.Count > 0)
                {
                    Console.WriteLine($"--> Save Data Success, TransType={addrParam.TransType}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Save Data has error: {ex.Message.Trim()}");
            }
        }
    }
}