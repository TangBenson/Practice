using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// using Domain.SP.Output;
// using Domain.SP.Sync.Input;
// using Domain.Sync.Input;
// using Domain.Sync.Output;
// using Domain.TB.Sync;
using IdentityModel.Client;
using Newtonsoft.Json;
// using Reposotory.Implement.Sync;
// using WebCommon;

namespace PushService
{
    class Program
    {
        private static string _accessToken = "";
        private static JwtSecurityTokenHandler _tokenHandler;
        private static int NowCount = 0;
        private static int MaxCount = 10;
        // private static string connetStr = ConfigurationManager.ConnectionStrings["IRent"].ConnectionString;
        static System.Threading.Thread PushMessageThread;
        static System.Threading.ThreadStart ts = new System.Threading.ThreadStart(new Program().DoPushMessage);
        public static int restartCount = 0;
        static void Main(string[] args)
        {
            PushMessageThread = new System.Threading.Thread(ts);
            PushMessageThread.Start();
        }
        private void checkThread()
        {
            Thread waitThread = new Thread(checkThread);
            if (false == PushMessageThread.IsAlive)
            {
                PushMessageThread = new Thread(ts);
                PushMessageThread.Start();
                //  Console.WriteLine("重啟thread");
                if (waitThread.IsAlive)
                {
                    waitThread.Abort();
                }
            }
            else
            {
                // Thread.Sleep(1000);

                waitThread.Start();
            }
        }
        private async void DoPushMessage()
        {
            if (NowCount >= MaxCount)
            {
                Console.WriteLine("休息一分鐘開始");
                Thread.Sleep(60000);
                Console.WriteLine("休息一分鐘結束");
                NowCount = 0;
            }
            try
            {
                //SendMsgRepository _repository = new SendMsgRepository(connetStr);
                //List<Sync_SendMessage> lstData = _repository.GetMessage();
                List<Sync_SendMessage> lstData = new List<Sync_SendMessage>()
                {
                    //new Sync_SendMessage(){NotificationID=0,NType=18,UserToken="1348667",STime=DateTime.Now,Message="個人推撥測試",url="https://www.ooooooo.com.tw/ppppp/web/index.html"},
                    new Sync_SendMessage(){NotificationID=0,NType=18,UserToken="1523608",STime=DateTime.Now,Message="個人推撥測試2",url="https://www.oooooo.com.tw/ppppp/web/index.html"}
                    //new Sync_SendMessage(){NotificationID=0,NType=18,UserToken="2747",STime=DateTime.Now,Message="個人推撥測試",url="https://www.oooooo.com.tw/oooooo/web/index.html"}
                };

                if (lstData != null)
                {
                    int len = lstData.Count();
                    for (int i = 0; i < len; i++)
                    {
                        if (lstData[i].UserToken != "0")
                        {
                            await PushMessage(lstData[i].UserToken, lstData[i].Message, lstData[i].url, lstData[i].NotificationID, lstData[i].NType);
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Console.WriteLine("休息五秒鐘開始");
                Thread.Sleep(5000);
                if (NowCount >= MaxCount)
                {
                    NowCount = 0;
                }
                else
                {
                    NowCount++;
                }
                Console.WriteLine("休息五秒鐘結束，再次啟動");
                checkThread();
            }

        }
        private static async Task PushMessage(string registerIdsT, string messageContent, string weburlContent, Int64 NewsID, int Type)
        {
            var startDate = DateTime.Now;
            //推播web api url

            var pushServerUrl = "https://iiiiiiiiiiii.azurewebsites.net";
            //var pushServerUrl = "http://localhost:5010";
            var pushMessageApi = "api/notification/PushMessage";
            var registerIds = (Type != 0 && registerIdsT != "0") ? registerIdsT.Split(',').Select(long.Parse).ToArray() : null; // 推播的接收者id

            //string messageContent = "Test aaa";
            // string weburlContent = "";

            // for test 1600 registers from 243 to 1842
            //for (int i = 243; i <= 1342; i++)
            //{
            //    registerIds.Add(i);
            //}

            var httpClient = new HttpClient();
            var message = new SyncInput_MessageParam()
            {
                AppId = 12, // appid 目前測試主機上是7
                RegisterIds = registerIds,
                MessageId = 0, // 永遠設0
                Title = messageContent,
                Content = messageContent,
                MessageTypeCode = "03",
                CategoryCode = "01",
                ImageUrl = "",
                WebUrl = weburlContent,
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
                ListType = (Type == 0) ? "01" : ""

            };
            var httpContent = new JsonContent(message);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json")
            {
                CharSet = "utf-8"
            };
            httpClient.BaseAddress = new Uri(pushServerUrl);
            httpClient.SetBearerToken(await RequestAccessToken());
            // httpClient.DefaultRequestHeaders.Accept.Clear();

            var response = await httpClient.PostAsync(pushMessageApi, httpContent);
            string resultString;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SyncOutput_ResultParam>(result);
                resultString = $"message: {data.Message}, result: {data.Result}";

                // string spName = "usp_UpdNotification";
                // List<ErrorInfo> lstError = new List<ErrorInfo>();
                // SPInput_SYNC_UpdNotification spInput = new SPInput_SYNC_UpdNotification()
                // {
                //     isSend = 1,
                //     PushTime = DateTime.Now.AddHours(8),
                //     NotificationID = NewsID
                // };
                // SPOutput_Base spOut = new SPOutput_Base();
                // SQLHelper<SPInput_SYNC_UpdNotification, SPOutput_Base> sqlHelp = new SQLHelper<SPInput_SYNC_UpdNotification, SPOutput_Base>(connetStr);
                //bool flag = sqlHelp.ExecuteSPNonQuery(spName, spInput, ref spOut, ref lstError);

            }
            else
            {
                resultString = $"Http request error, status code: {response.StatusCode}";
            }

            Console.WriteLine("response:" + resultString);

            var endDate = DateTime.Now;
            Console.WriteLine("total time:" + (endDate - startDate).Seconds + "secs");
        }

        private static long ToUnixTime(DateTime dt)
        {
            return ((DateTimeOffset)dt).ToUnixTimeSeconds();

        }

        private static async Task<string> RequestAccessToken()
        {
            if (!JwtIsExpired(_accessToken)) return _accessToken;

            var client = new HttpClient();
            var ids4Address = "https://yyyyyyyyyyyyy.azurewebsites.net";
            var ids4ClientId = "yyyyyyyyyyyyy";
            var ids4ClientSecret = "yyyy-yyyy-4b00-yyyy-yyyyyy";
            var ids4Scope = "sspush";

            var disco = await client.GetDiscoveryDocumentAsync(ids4Address);
            if (disco.IsError)
                // _logger.LogError(disco.Error);
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
                // _logger.LogError(tokenResponse.Error);
                throw new Exception(tokenResponse.Error);

            var accessToken = tokenResponse.AccessToken;
            _accessToken = accessToken;
            return accessToken;
        }

        private class JsonContent : StringContent
        {
            public JsonContent(object obj) :
                base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            {
            }
        }

        private static bool JwtIsExpired(string jwt)
        {
            if (String.IsNullOrEmpty(jwt)) return true;
            var jwtSecrityToken = _tokenHandler.ReadJwtToken(jwt);
            return jwtSecrityToken.ValidTo < DateTime.Now.AddSeconds(30);
        }
    }

    public class Sync_SendMessage
    {
        public Int64 NotificationID { set; get; }
        /// <summary>
        /// 0是全推
        /// </summary>
        public Int16 NType { set; get; }
        public string UserToken { set; get; }
        public DateTime STime { set; get; }
        public string Message { set; get; }
        public string url { set; get; }
    }

    public class SyncInput_MessageParam
    {
        public int AppId { get; set; } // APP編號
        public long[] RegisterIds { get; set; } // 使用者的註冊編號(推播對像)
        public long MessageId { get; set; } // 一律填0
        public string Title { get; set; } // 推播訊息標題
        public string Content { get; set; } // 推播訊息內容
        public string MessageTypeCode { get; set; } // 固定填"02"
        public string CategoryCode { get; set; } // 固定填"00"
        public string ImageUrl { get; set; } // 固定填""
        public string WebUrl { get; set; } // 跳轉網址
        /// <summary>
        /// 01推給全部
        /// </summary>
        public string ListType { get; set; }
        public int ShareTo { get; set; } // 固定填0
        public string ExternalTitle { get; set; } // 固定填""
        public string ExternalUrl { get; set; } // 固定填""
        public string Page2Title { get; set; } // 固定填""
        public string Page3Title { get; set; } // 固定填""
        public string Page3Content { get; set; } // 固定填""
        public string Info01 { get; set; } // 固定填""
        public string Info02 { get; set; } // 固定填""
        public string Info03 { get; set; } // 固定填""
        public string Info04 { get; set; } // 固定填""
        public string Info05 { get; set; } // 固定填""
        public string Info06 { get; set; } // 固定填""
        public string Info07 { get; set; } // 固定填""
        public string Info08 { get; set; } // 固定填""
        public string Info09 { get; set; } // 固定填""
        public string Info10 { get; set; } // 固定填""

        public long EndTime { get; set; } // 息訊保留截止日,如果APP移除後重新安裝,在此日期內的所有訊息會重新下載到APP端

        // 因此,如果想保留當日訊息,請在此填入隔日日期(本日+1天)的unixtime 
        public string UserId { get; set; } // 保留未用,定填""
    }

    public class SyncOutput_ResultParam
    {
        public string Message { get; set; }
        public bool Data { get; set; }
        public bool Result { get; set; }
    }
}
