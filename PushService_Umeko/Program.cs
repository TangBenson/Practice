using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using PushService_Umeko.Models;
using Microsoft.Extensions.Configuration;

namespace PushService_Umeko
{
    class Program
    {

        private static string _accessToken = "";
        private static JwtSecurityTokenHandler _tokenHandler;

        // private static string connetStr = "Data Source=sssss.ddddd.windows.net,1433;Initial Catalog=IRENT_V2T;Persist Security Info=True;User ID=ffff;Password=dfdfdfdf;Application Name=IRentWeb;";
        private static Mutex mut; //判斷是否程式正在開啟

        //Func 會傳回值，我們會將回傳型態放在最後一項
        private Func<SyncInput_MessageParam, Task<SyncOutput_PushMessageResultParam>> _sendMsg;

        public Func<SyncInput_MessageParam, Task<SyncOutput_PushMessageResultParam>> SetSendMsg
        {
            set => _sendMsg = value; //宣告屬性(property)，就是set; get;那玩意
        }

        static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

            //用TryParse轉換string to int會判斷失敗與否，這樣不會有Exception中斷的問題，若轉成功則將值存到out關鍵字後面宣告的outSleepTimes
            int defaultSleepTimes = 60000;
            int sleepTimes = int.TryParse(config.GetSection("AppSettings")["sleepTimes"], out int outSleepTimes)
                    ? outSleepTimes
                    : defaultSleepTimes;

            bool defaultisSendMsg = true;
            bool isSendMsg = bool.TryParse(config.GetSection("AppSettings")["isSendMsg"], out bool outisSendMsg)
                ? outisSendMsg
                : defaultisSendMsg;

            bool defaultIsContinuedSet = false;
            bool IsContinuedSet = bool.TryParse(config.GetSection("AppSettings")["isContinued"], out bool outisContinuedSet)
                ? outisContinuedSet
                : defaultIsContinuedSet;

            _tokenHandler = new JwtSecurityTokenHandler();

            bool isOnlyOne = false;
            mut = new Mutex(true, "MyPushServiceMutex", out isOnlyOne);

            if (isOnlyOne)
            {
                Console.WriteLine("*********************************************");
                Console.WriteLine("*       歡迎進入 推播發送  作業程式             *");
                Console.WriteLine("*         Copyright (C) 2021/08/11          *");
                Console.WriteLine("*********************************************");
                try
                {
                    Console.WriteLine($"------執行設定-----");
                    Console.WriteLine($"發信:{isSendMsg}");
                    Console.WriteLine($"執行間格時間:{sleepTimes}");
                    Console.WriteLine($"連續發送:{IsContinuedSet}");

                    bool consoleRun = GetContinueType(IsContinuedSet, 0);
                    Console.WriteLine($"發送狀態:{consoleRun}");

                    while (consoleRun)
                    {
                        try
                        {
                            Program p = new Program();

                            if (isSendMsg)
                                p.SetSendMsg = SendMsg;//加上此行會發送推播，若未指定 則預設是以寫入文字檔模擬訊息推播 //呼叫SendMsg要傳入的參數設定在最上面宣告委派SetSendMsg時的set

                            p.PrintMessage("----Start----");

                            p.SendPersonNotification();
                        }
                        catch (Exception err)
                        {
                            NLogHelper.logger.Error(err.ToString());
                        }
                        finally
                        {
                            int second = sleepTimes / 1000;
                            Console.WriteLine($"休息({second})秒鐘開始");
                            Thread.Sleep(sleepTimes);
                            Console.WriteLine($"休息({second})秒鐘結束，再次啟動");
                            consoleRun = GetContinueType(IsContinuedSet, 1);
                            Console.WriteLine($"發送狀態:{consoleRun}");


                        }
                    }
                }
                catch (Exception ex)
                {
                    NLogHelper.logger.Error(ex.ToString());
                }
            }
            else
            {
                Console.WriteLine("已經執行囉!");
                Console.ReadLine();
            }

        }


        //呼叫Sam大的PushMessage
        public async void SendPersonNotification()
        {
            Console.WriteLine($"SendPersonNotification 1:{Thread.CurrentThread.ManagedThreadId}");
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                // var personNotifications = new PersonNotificationProvider(connetStr).GetSendList().Result;
                var personNotifications = new List<Sync_PersonNotification>(){
                    new Sync_PersonNotification(){NotificationID=1,NType=17,UserToken="1523608",Title="早安呦",Message="",url=""}
                    // ,new Sync_PersonNotification(){NotificationID=1,NType=17,UserToken="1348667",Title="早安妳好，早睡早起",Message="",url=""}
                    // ,new Sync_PersonNotification(){NotificationID=1,NType=17,UserToken="1755825",Title="早安妳好，早睡早起",Message="",url=""}
                };

                //select出所有Ntype不是死杯秀的
                var distinctNTypes = personNotifications
                                        .Where(x => !isSpecialType(x)) //NType=19就是死杯秀
                                        .Select(x => x.NType).ToList().Distinct();
                List<Task<List<Sync_PersonNotification>>> allTask = new List<Task<List<Sync_PersonNotification>>>();
                foreach (var NType in distinctNTypes)
                {
                    var preparePersonNotifications = personNotifications.Where(t => t.NType.Equals(NType));
                    Task<List<Sync_PersonNotification>> task = DoSomething(preparePersonNotifications);
                    allTask.Add(task);
                }

                // select出所有Ntype是死杯秀的
                var specialPersonNotifications = personNotifications.Where(x => isSpecialType(x)).ToList();
                foreach (var PersonalNotification in specialPersonNotifications)
                {
                    var preparePersonNotifications = new List<Sync_PersonNotification> { PersonalNotification };
                    Task<List<Sync_PersonNotification>> task = DoSomething(preparePersonNotifications);
                    allTask.Add(task);
                }

                var results = await Task.WhenAll<List<Sync_PersonNotification>>(allTask);
                List<Sync_PersonNotification> sends = new List<Sync_PersonNotification>();
                foreach (var result in results)
                {
                    sends.AddRange(result);
                    PrintMessage(JsonConvert.SerializeObject(result));
                }
                Console.WriteLine($"SendPersonNotification 2:{Thread.CurrentThread.ManagedThreadId}");
                // var aa = new PersonNotificationProvider(connetStr);
                // var updateToDB = Task<int>.Run(async () =>
                // {
                //     var row = await aa.UpdateData(sends);
                //     return row;
                // });

                // var rows = updateToDB.Result;
                stopwatch.Stop();

                // PrintMessage($"此次影響筆數:{rows} 筆");
                PrintMessage($"此次共花費 {stopwatch.Elapsed} 時間");

            }
            catch (Exception ex)
            {
                NLogHelper.logger.Error(ex.ToString());
            }
        }

        private void PrintMessage(string message)
        {
            NLogHelper.logger.Info(message);
            Console.WriteLine(message);
        }

        //Sam大的PushMessage
        public async Task<List<Sync_PersonNotification>> DoSomething(IEnumerable<Sync_PersonNotification> sendList)
        {
            Console.WriteLine($"DoSomething:{Thread.CurrentThread.ManagedThreadId}");
            //整理訊息
            var registerIds = sendList.Select(x => (long.Parse(x.UserToken))).Distinct().ToArray();

            var title = sendList.FirstOrDefault().Title;
            var msg = sendList.FirstOrDefault().Message;
            var url = sendList.FirstOrDefault()?.url ?? "";
            var ntype = sendList.FirstOrDefault()?.NType.ToString("D2") ?? "01";

            var messageTypeCode = string.IsNullOrEmpty(url) ? "01" : "03";

            var message = new SyncInput_MessageParam
            {
                AppId = 12, // appid 目前測試主機上是7
                RegisterIds = registerIds,
                MessageId = 0, // 永遠設0
                Title = $"{title}",
                Content = $"{msg}",
                MessageTypeCode = messageTypeCode,
                CategoryCode = ntype,
                ImageUrl = "",
                WebUrl = url,
                ShareTo = 0,
                ExternalTitle = ",",
                ExternalUrl = ",",
                Page2Title = ",",
                Page3Title = ",",
                Page3Content = ",",
                Info01 = "",
                Info02 = "",
                Info03 = "",
                Info04 = "",
                Info05 = "",
                Info06 = "",
                Info07 = "",
                Info08 = "",
                Info09 = "",
                Info10 = "",
                EndTime = ToUnixTime(DateTime.Now.AddDays(1)),
                UserId = "",
            };

            _sendMsg = _sendMsg ?? WriteMsg; //若??左邊不為null則返回左，否則返回右
            //發送訊息，這邊才去呼叫SendMsg method
            var result = await Task<SyncOutput_PushMessageResultParam>.Run(() => _sendMsg(message));

            List<Sync_PersonNotification> sends = new List<Sync_PersonNotification>();

            foreach (Sync_PersonNotification send in sendList)
            {
                send.isSend = 2;
                send.NewsID = Convert.ToInt32(result.Data);
                sends.Add(send);

                PrintMessage(JsonConvert.SerializeObject(result));
            }

            return sends;
        }


        private static async Task<SyncOutput_PushMessageResultParam> WriteMsg(SyncInput_MessageParam message)
        {
            await Task.Run(() => NLogHelper.logger.Info(JsonConvert.SerializeObject(message)));

            Random rnd = new Random();
            return new SyncOutput_PushMessageResultParam() { Result = true, Message = "", Data = rnd.Next(5000, 10000) };
        }

        //Sam大的PushMessage，設定要打哪支api，並呼叫下一個method去打api
        private static async Task<SyncOutput_PushMessageResultParam> SendMsg(SyncInput_MessageParam message)
        {
            Console.WriteLine($"SendMsg:{Thread.CurrentThread.ManagedThreadId}");
            var pushServerUrl = " https://xxxxxxxxxx.azurewebsites.net/";
            var pushMessageApi = "api/notification/PushMessage";
            var results = await Task.Run(() => PostRequestAsync<SyncInput_MessageParam>(pushServerUrl, pushMessageApi, message));
            return results;
        }

        private bool isSpecialType(Sync_PersonNotification input)
        {
            List<int> specialNtypes = GetSpecialNtypes();
            return specialNtypes.Exists(t => t.Equals(input.NType));
        }

        private List<int> GetSpecialNtypes() => new List<int> { 19 };

        private static bool GetContinueType(bool IsContinuedSet, int inputSet)
        {
            bool consoleRun = true;

            if (!IsContinuedSet)
            {
                string isContinued = "Y";

                if (inputSet == 1)
                {
                    Console.WriteLine("是否繼續執行?");
                    isContinued = Console.ReadLine();
                }

                consoleRun = (isContinued.ToLower() == "y");
            }
            return consoleRun;
        }
        
        //Sam大的PushMessage，實際執行打api的動作
        static async Task<SyncOutput_PushMessageResultParam> PostRequestAsync<T>(string url, string api, T param)
        {
            var data = new SyncOutput_PushMessageResultParam()
            {
                Result = false,
                Data = 0,
            };
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);
            httpClient.SetBearerToken(await RequestAccessToken());

            var httpContent = new JsonContent(param);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json")
            {
                CharSet = "utf-8"
            };

            var response = await httpClient.PostAsync(api, httpContent);
            string re = "";
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                re = result;
                data = JsonConvert.DeserializeObject<SyncOutput_PushMessageResultParam>(result);
            }
            return data;
        }
        
        //Sam大的method
        private static long ToUnixTime(DateTime dt)
        {
            return ((DateTimeOffset)dt).ToUnixTimeSeconds();
        }
        
        //Sam大的method，取Token
        private static async Task<string> RequestAccessToken()
        {
            if (!JwtIsExpired(_accessToken)) return _accessToken;

            var client = new HttpClient();
            var ids4Address = "https://uuuuuuuuuuu.azurewebsites.net";
            var ids4ClientId = "uuuuuuuuuu";
            var ids4ClientSecret = "uuuuu-uuuuu-uuuu-uuuuu-uuuuuuuuu";
            var ids4Scope = "sspush";

            var disco = await client.GetDiscoveryDocumentAsync(ids4Address);
            if (disco.IsError)
                throw new Exception(disco.Error);

            // request access token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = ids4ClientId,
                ClientSecret = ids4ClientSecret,
                Scope = ids4Scope
            });
            if (tokenResponse.IsError)
                throw new Exception(tokenResponse.Error);

            var accessToken = tokenResponse.AccessToken;
            _accessToken = accessToken;
            return accessToken;
        }
        
        //Sam大的method
        private class JsonContent : StringContent
        {
            public JsonContent(object obj) :
                base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            {
            }
        }
        
        //Sam大的method
        private static bool JwtIsExpired(string jwt)
        {
            if (String.IsNullOrEmpty(jwt)) return true;
            var jwtSecrityToken = _tokenHandler.ReadJwtToken(jwt);
            return jwtSecrityToken.ValidTo < DateTime.Now.AddSeconds(30);
        }
    }
}
