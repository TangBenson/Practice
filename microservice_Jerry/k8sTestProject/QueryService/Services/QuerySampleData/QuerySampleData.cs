using System;
using System.Threading.Tasks;
using System.Data;
using CommonService.Utilities;
using CommonService.Dtos;
using CommonService.Params;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QueryService.Params;
using QueryService.Services.AsyncDataServices;
//QueryService透握ssapi去db查地址資料後，publish到RabbitMQ，再subscribe到Trans Service，再去tdx做geocode交換(以前是google)
namespace QueryService.Services
{
    public class QuerySampleData: IQuerySampleData //繼承介面，表示我要實作它
    {
        //宣告一個變數_config去接外面傳進來的configuration，using要先引用
        private readonly IConfiguration _config;

        //初始化RabbitMQ服務，建構式要傳進實體。(在startup依賴注入，這邊直接這樣用，下方建構子也要加程式碼進去。宣告一個介面，實體在建構子再做綁定)
        private readonly IMessageBusClient _messageBusClient;

        //建構式預設會抓appSetting的configuration，就是以前的webconfig
        //建構式，class初始化跑的
        public QuerySampleData(IConfiguration configuration, IMessageBusClient messageBusClient)
        {
            Console.WriteLine("QueryData Init");
            _config = configuration;
            _messageBusClient = messageBusClient;//實體和介面做綁定
        }


        public ResultDTO<DataSet> QuerySampleDataFromDB()
        {
            Console.WriteLine("QuerySampleDataFromDB Init");
            //固定寫法start------------------    
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
                param.DATABASE_TYPE = "S"; //sql或oracle
                param.SSAPI_ADDRESS = _config.GetSection("AppSettings")["SSAPI"].Trim();

                object[][] queryParam = {
                        new object[1]  { "0" } //對應到sp參數1:1數順序對下來，若sp是收table則傳入json
                        // new object[1]  { "" }                      
                   };
                serverInfo=new ServerInfo(_config).GetServerInfo(param);
                
                Console.WriteLine("WebApiClient Init" + JsonConvert.SerializeObject(serverInfo));
                //false表示放棄transaction的控制，把控制放在資料庫端而非服務端。服務端控制就是用MSDTC。
                //只會呼叫ssapi的2個func，一個是query，一個是table parameter無上限傳入，後者會傳回DataSet，可同時存不同data來源(陣列)，可在一個交易內對db做存檔，
                //好處是因為用datatable parameter，幾萬筆資料只要呼叫一次資料庫，不用像以前元件call迴圈一筆一筆呼叫。
                queryDS = new WebApiClient(_config).SPExeBatchMultiArr2(serverInfo, "SP_GetNews", queryParam, false , ref message, ref messageLevel, ref messageType);
                // queryDS = new WebApiClient(_config).SPExeBatchMultiArr2(serverInfo, "SP_TEST010_Q", queryParam, false , ref message, ref messageLevel, ref messageType);
                //固定寫法end------------------    

                Console.WriteLine("WebApiClient After"+queryDS.Tables[0].Rows.Count.ToString());  
                // if (message != "")
                // {
                //     throw new Exception(message + "<br>" + JsonConvert.SerializeObject(queryParam));
                // }
                // else
                // {
                //     for (int i = 0; i < queryDS.Tables[0].Rows.Count; i++)
                //     {
                //         var addr = new AddrParam()
                //         {
                //             SEQNO = int.Parse(queryDS.Tables[0].Rows[i]["SEQNO"].ToString()),
                //             ADDRESS = queryDS.Tables[0].Rows[i]["ADDRESS"].ToString()
                //         };
                //         //送message到queue裡面等(物件做序列化成文字檔再送出去)
                //         _messageBusClient.SendRequest(JsonConvert.SerializeObject(addr));
                //     }
                // }

                return (new ResultDTO<DataSet>
                {
                    Message = "",
                    Data = queryDS,
                    Result = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> QueryData has error: {ex.Message.Trim()}");
                return (new ResultDTO<DataSet>
                {
                    Message = ex.Message,
                    Data = null,
                    Result = false
                });
            }

        }

        
    }
}
