using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CallAPI
{
    public class Program2
    {
        static void Main(string[] args)
        {
            // DateTime now = DateTime.Now;
            // while (now.Hour < 26)
            // {
            //     Program2.RunApi();
            //     Thread.Sleep(15000);
            // }
            Program2.RunApi();
        }

        public static void RunApi()
        {
            //以下兩行是解決SSL憑證錯誤的訊息，宣告client時要多傳入clientHandler
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient client = new HttpClient(clientHandler))
            {
                // using (System.IO.StreamWriter file =  //StreamWriter默認會覆蓋原先文件并重寫一份
                //       new System.IO.StreamWriter(@"C:\Users\benson922\Desktop\CALLAPITEST.txt", true))
                // {
                    try
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        client.Timeout = TimeSpan.FromSeconds(20); //設定 Timeout 屬性，避免資源浪費
                        ApiInput param = new ApiInput()
                        {

                        };
                        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(param));
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "9A0328137E18E4892645EAC71E63C6258D1D4DEE1A12ED1BA020F944408C1911");
                        HttpResponseMessage resp = client.PostAsync("https://irenttestffffffffff.azurefd.net/api/News", httpContent).Result;
                        Stream rsp = resp.Content.ReadAsStreamAsync().Result;
                        StreamReader sr = new StreamReader(rsp);
                        string response = sr.ReadToEnd();
                        sw.Stop();
                        string f = (JsonConvert.DeserializeObject<ApiOutput>(response).Result) == "1" ? "成功" : "失敗";
                        // file.WriteLine($"{f} ----{DateTime.Now}---- {sw.ElapsedMilliseconds}ms");
                        Console.WriteLine($"{f} ----{DateTime.Now}---- {sw.ElapsedMilliseconds}ms");
                    }
                    // catch (Exception ex)
                    // {
                    //     file.WriteLine($"有問題--{DateTime.Now}----{ex}");
                    //     Console.WriteLine($"有問題--{DateTime.Now}----{ex}");
                    // }
                    catch (TimeoutException ex)
                    {
                        // file.WriteLine($"有問題Timeout--{DateTime.Now}----{ex}");
                        Console.WriteLine("Request timed out: " + ex.Message);
                    }
                    catch (HttpRequestException ex)
                    {
                        // file.WriteLine($"有問題HttpRequest--{DateTime.Now}----{ex}");
                        Console.WriteLine("Request failed: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        // file.WriteLine($"有問題--{DateTime.Now}----{ex}");
                        Console.WriteLine($"有問題--{DateTime.Now}----{ex}");
                    }
                // }
            }
        }
    }

    public class ApiInput
    {
        // public string IDNO { get; set; } = "A170170983";
        // public string DeviceID { get; set; } = "29a92aa362e933b8cb8674f0bedc247dd";
        // public string RefrashToken { get; set; } = "68E1476FE80781CB9D3E4590AFB4BC507891A3CBB9A36134416020E024D88011";
        // public int app { get; set; } = 0;
        // public string appVersion { get; set; } = "3.0.1";
        // public string PushREGID { get; set; } = "1523608";
    }
    public class ApiOutput
    {
        public string? Result { get; set; }
        public string? ErrorCode { get; set; }
        public int NeedRelogin { get; set; }
        public int NeedUpgrade { get; set; }
        public string? ErrorMessage { get; set; }
        public object? Data { get; set; }
    }
}