// using System;
// using System.Threading.Tasks;
// using System.Data;
// using CommonService.Utilities;
// using CommonService.Dtos;
// using CommonService.Params;
// using Microsoft.Extensions.Configuration;
// using Newtonsoft.Json;

// namespace TransService.Services
// {
//     public class QuerySampleData: IQuerySampleData
//     {         
//         private readonly IConfiguration _config;        

//         public QuerySampleData(IConfiguration configuration)
//         {
//             Console.WriteLine("QueryData Init");
//             _config = configuration;
//         }


//         public ResultDTO<DataSet> QuerySampleDataFromDB()
//         {
//             Console.WriteLine("QuerySampleDataFromDB Init");
//             string message = "";
//             string messageLevel = "";
//             string messageType = "";
//             DataSet queryDS = new DataSet();
//             try
//             {
//                 BaseParams param = new BaseParams();
//                 string[] serverInfo = _config.GetSection("AppSettings")["SQLString"].Trim().Split('|');
//                 param.SERVER_NAME = serverInfo[0];
//                 param.DATABASE_NAME = serverInfo[1];
//                 param.LOGIN = serverInfo[2];
//                 param.PASSWORD = serverInfo[3];
//                 param.DATABASE_TYPE = "S";
//                 param.SSAPI_ADDRESS = _config.GetSection("AppSettings")["SSAPI"].Trim();

//                 object[][] queryParam = {
//                         new object[1]  { "" }
//                    };
//                 serverInfo=new ServerInfo(_config).GetServerInfo(param);
                
//                 Console.WriteLine("WebApiClient Init" + JsonConvert.SerializeObject(serverInfo));
//                 queryDS = new WebApiClient(_config).SPExeBatchMultiArr2(serverInfo, "SP_TEST010_Q", queryParam, false , ref message, ref messageLevel, ref messageType);

//                 Console.WriteLine("WebApiClient After"+queryDS.Tables[0].Rows.Count.ToString());
//                 if (message != "")
//                 {
//                     throw new Exception(message + "<br>" + JsonConvert.SerializeObject(queryParam));
//                 }

//                 return (new ResultDTO<DataSet>
//                 {
//                     Message = "",
//                     Data = queryDS,
//                     Result = true
//                 });
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"--> QueryData has error: {ex.Message.Trim()}");
//                 return (new ResultDTO<DataSet>
//                 {
//                     Message = ex.Message,
//                     Data = null,
//                     Result = false
//                 });
//             }

//         }

        
//     }
// }
