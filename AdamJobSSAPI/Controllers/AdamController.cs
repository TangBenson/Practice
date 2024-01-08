using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdamJobSSAPI.Dtos;
using AdamJobSSAPI.Params;
using AdamJobSSAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdamJobSSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdamController : Controller
    {
        private readonly IConfiguration _config;

        public AdamController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IActionResult Index()
        {
            int year = 2021;
            DateTime date_1 = new DateTime(year, 01, 01);
            DateTime date_2 = new DateTime(year, 09, 30);
            int diffDay = ((date_2 - date_1).Days) + 1;
            string[] arrayDay = new string[diffDay];
            int j = 0;
            for (int i = int.Parse(year.ToString() + "0101"); i < int.Parse(year.ToString() + "0931"); i++)
            {
                if ((int.Parse(i.ToString().Substring(4, 2)) % 2 == 0) && (int.Parse(i.ToString().Substring(4, 2)) < 8))
                {
                    if ((int.Parse(i.ToString().Substring(4, 2)) == 2) && i.ToString().Substring(6)=="29")
                        i += 72;
                    else
                    {
                        if(i.ToString().Substring(6)=="31")
                            i += 70;
                    }
                }
                else if ((int.Parse(i.ToString().Substring(4, 2)) % 2 == 1) && (int.Parse(i.ToString().Substring(4, 2)) < 8))
                {
                    if(i.ToString().Substring(6)=="32")
                        i += 69;
                }
                else if ((int.Parse(i.ToString().Substring(4, 2)) % 2 == 0) && (int.Parse(i.ToString().Substring(4, 2)) >= 8))
                {
                    if(i.ToString().Substring(6)=="32")
                        i += 69;
                }
                else
                {
                    if(i.ToString().Substring(6)=="31")
                        i += 70;
                }

                arrayDay[j] = i.ToString();
                j++;
            }

            foreach (var q in arrayDay)
            {
                Console.WriteLine(q);
                // using (System.IO.StreamWriter file =  //StreamWriter默認會覆蓋原先文件并重寫一份
                //       new System.IO.StreamWriter(@"C:\Users\benson922\Desktop\AdamJobRecord.txt", true))
                // {
                DateTime localDate = DateTime.Now;
                // file.WriteLine($"開始：{q}__{localDate.ToString()}");
                Console.WriteLine($"開始：{q}__{localDate.ToString()}");

                //SSAPI
                string message = "";
                string messageLevel = "";
                string messageType = "";
                DataSet queryDS = new DataSet(); //用DataSet接收多表回傳，但大部分情境應該用單一表的DataTable就好
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
                            // new object[2]  { "01",q } //對應到sp參數1:1數順序對下來，若sp是收table則傳入json
                            new object[2]  { "01",q }                  
                        };
                    serverInfo = new ServerInfo(_config).GetServerInfo(param);

                    Console.WriteLine("WebApiClient Init" + JsonConvert.SerializeObject(serverInfo));
                    //false表示放棄transaction的控制，把控制放在資料庫端而非服務端。服務端控制就是用MSDTC。
                    //只會呼叫ssapi的2個func，一個是query，一個是table parameter無上限傳入，後者會傳回DataSet，可同時存不同data來源(陣列)，可在一個交易內對db做存檔，
                    //好處是因為用datatable parameter，幾萬筆資料只要呼叫一次資料庫，不用像以前元件call迴圈一筆一筆呼叫。
                    queryDS = new WebApiClient(_config).SPExeBatchMultiArr2(serverInfo, "SP_LSE120_NONIRENT", queryParam, false, ref message, ref messageLevel, ref messageType);
                    // usp_BE_GetAuditImage_Tang_20210708
                    // SP_LSE120_CARTYPE_IRENT_JOB    TB_LES120_CARTYPE_IRENT
                    // SP_LSE120_CARTYPE_NOIRENT_JOB    TB_LES120_CARTYPE_NOIRENT
                    // SP_LSE120_IRENT_JOB   TB_LES120_IRENT
                    // SP_LSE120_NONIRENT   TB_LES120_NOIRENT
                    //固定寫法end------------------    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"錯誤_{q}");
                }
                // }
                Thread.Sleep(10000); //等待60秒
            }
            return Ok("Good Job !!");
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}