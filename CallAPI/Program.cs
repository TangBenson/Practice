// using Newtonsoft.Json;
// using System.Threading.Tasks;

// CalApi a = new CalApi();

// for (int j = 0; j < 10; j++)
// {
//     for (int i = 0; i < 5; i++)
//     {
//         a.RunApi(j, i);
//         Console.WriteLine($"yo~~");
//     }
// }

// Console.WriteLine("結束了");
// Console.ReadLine();

// // for (int r = 0; r < 5; r++)
// // {
// //     Task task1 = new Task(CalApi.TestSleepAsync);
// //     task1.Start();
// //     Console.WriteLine(r);
// // }
// // Console.ReadLine();

// public class CalApi
// {
//     // public static void TestSleepAsync()
//     // {
//     //     Thread.Sleep(3000);
//     //     Console.WriteLine("F");
//     // }

//     public async Task RunApi(int j, int i)
//     {
//         Console.WriteLine($"第{j}位客人呼叫第{i}次");
//         //以下兩行是解決SSL憑證錯誤的訊息，宣告client時要多傳入clientHandler
//         HttpClientHandler clientHandler = new HttpClientHandler();
//         clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

//         using (HttpClient client = new HttpClient(clientHandler))
//         {
//             client.Timeout = TimeSpan.FromSeconds(30); //設定 Timeout 屬性，避免資源浪費
//                                                        // TakeiRentToken param = new TakeiRentToken();
//                                                        // param.limit = 5;
//             ApiInput param = new ApiInput()
//             {

//             };
//             HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(param));
//             httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
//             // HttpResponseMessage resp = client.PostAsync("https://xxxxxxxx-irent.net/api/GetMotorParkingData", httpContent).Result;
//             HttpResponseMessage resp = await client.PostAsync("https://xxxxxxxx-irent.net/api/GetMotorParkingData", httpContent);
//             // HttpResponseMessage resp = await client.GetAsync("https://xxxxxxxx-irent.net/api/CityList");
//             Stream rsp = resp.Content.ReadAsStreamAsync().Result; //非同步的方式取得 HTTP Response 的 Content
//             StreamReader sr = new StreamReader(rsp);
//             string response = sr.ReadToEnd();
//             Console.WriteLine(response);
//         }
//     }
// }


// public class ApiInput
// {
//     public int ShowALL { get; set; } = 0;
//     public double Latitude { get; set; } = 24.150060;
//     public double Longitude { get; set; } = 120.650882;
//     public int Mode { get; set; } = 2;
//     public double Radius { get; set; } = 3.5;
// }