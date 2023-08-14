// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Diagnostics;

// namespace CsharpStudy
// {
//     public partial class AsyncAwait6
//     {
//         private readonly HttpClient httpClient = new HttpClient();
//         private string text = "";

//         static async Task Main(string[] args)
//         {
//             Console.WriteLine("開始~~");

//             AsyncAwait6 a = new AsyncAwait6();
//             Stopwatch stopwatch = new Stopwatch();
//             stopwatch.Start();

//             // a.DownloadSync();
//             await a.DownloadAsync();

//             Console.WriteLine("下載結束");
//             stopwatch.Stop();
//             TimeSpan elapsed = stopwatch.Elapsed;
//             double milliseconds = elapsed.TotalMilliseconds;
//             a.text += $"{milliseconds}---{Environment.NewLine}";
//             Console.WriteLine(milliseconds);
//         }


//         private void DownloadSync()
//         {
//             foreach (var site in Content.Websites)
//             {
//                 var result = DownloadSync(site);
//                 Console.Write(result);
//                 ReportResult(result);
//             }
//         }
//         private string DownloadSync(string url)
//         {
//             var responce = httpClient.GetAsync(url).GetAwaiter().GetResult();
//             var responcePayloadBytes = responce.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
//             return $"{url}-----{responcePayloadBytes.Length}---{Environment.NewLine}";
//         }
//         private async Task<string> DownloadAsync(string url)
//         {
//             var responce = await httpClient.GetAsync(url);
//             var responcePayloadBytes = await responce.Content.ReadAsByteArrayAsync();
//             return $"{url}-----{responcePayloadBytes.Length}---{Environment.NewLine}";
//         }


//         private async Task DownloadAsync()
//         {
//             #region  這作法和同步一樣，因為 await Task.Run跳回至呼叫DownloadAsync的那行，但那行用 await等待，沒有上一層可以跳回去繼續執行，就一直等在那
//             // foreach (var site in Content.Websites)
//             // {
//             //     // 用 Task去執行方法，就變成非同步的方式了。await要求等待完成，await只能放在 async方法中
//             //     // 執行到 await會跳回呼叫此方法的地方，也就是DownloadAsync()
//             //     var result = await Task.Run(() => DownloadSync(site));
//             //     Console.Write(result);
//             //     ReportResult(result);
//             // }
//             #endregion


//             #region 這才是我要的非同步，會同時下載
//             // List<Task<string>> lst = new List<Task<string>>();
//             // foreach (var site in Content.Websites)
//             // {
//             //     lst.Add(Task.Run(() => DownloadSync(site)));
//             // }
//             // var results = await Task.WhenAll(lst);
//             // foreach(var result in results){
//             //     Console.Write(result);
//             //     ReportResult(result);
//             // }
//             #endregion


//             #region 非同步優化版，減少第一次啟動程式的時間
//             /*
//             IO bound程式不要用 Task.Run興起 thread去操作，只有 cpu bound程式才要自建 thread去操作，
//             cpu bound程式的 CPU大量時間用於計算而非等待，例如等待網路、硬碟...，IO bound程式底層已是異步編成的模型，
//             因此不用單獨 thread進行等待，直接使用 await就好，cpu bound程式底層不會提供 async服務，要單獨啟動 thread來完成操作，才要用 Task run興起 Thread
//             */
//             List<Task<string>> lst = new List<Task<string>>();
//             foreach (var site in Content.Websites)
//             {
//                 lst.Add(DownloadAsync(site)); // 不需要額外 thread進行等待，因此不需要 Task.Run，DownloadAsync返回值就是 Task string，不用額外處理
//             }
//             /*
//             設定.ConfigureAwait(false)告訴程式 await之前與之後的程式不需一定要用同一條 thread，因為程式執行到 await會釋放 thread
//             ，等 await執行完後可能會用別的 thread跑後續程式，ConfigureAwait預設都是 true
//             ，若設成 false對於 ui相關的 thread會有問題。

//             題外話：
//             var results = await Task.WhenAll(lst)改成 var results = Task.WhenAll(lst).Result或 .GetAwaiter().GetResult()
//             ，表示將 thread阻塞在這裡，就是沒有釋放 thread，會一直等待 Task完成返回結果，這寫法非常糟糕，會變成 dead lock
//             ，解法是 DownloadAsync的兩個 await加上.ConfigureAwait(false)即可。

//             至於 Task.WhenAll(lst).Result 和 Task.WhenAll(lst).GetAwaiter().GetResult() 都是用於等待並取得多個任務完成的結果，
//             但它們之間存在一些微妙的差異。
//             選擇 .Result 或 .GetAwaiter().GetResult() 的情境：
//             .Result 是 Task<TResult> 的屬性，可以直接存取。如果你處理的是非泛型的 Task，則只能使用 .Result。
//             .GetAwaiter().GetResult() 是對 Task 的擴充方法，可用於所有類型的 Task，包括泛型和非泛型的。它可以提供更細緻的控制，例如處理取消和異常等。
//             錯誤處理：
//             .Result 在取得結果時，如果任務遇到異常，會拋出 AggregateException。你需要進行錯誤處理，例如使用 try-catch 區塊捕獲異常。
//             .GetAwaiter().GetResult() 在取得結果時，如果任務遇到異常，會直接拋出原始的異常類型。你可以針對具體的異常類型進行捕獲和處理。
//             總結來說，如果你只是簡單地等待並取得多個任務的結果，並不需要額外的錯誤處理，那麼使用 .Result 是較簡潔的選擇。如果你需要更細緻的錯誤處理，可以使用 .GetAwaiter().GetResult()，以便直接處理特定的異常類型。
//             然而，請注意，在某些情況下，使用 .Result 或 .GetAwaiter().GetResult() 可能會導致阻塞（blocking），特別是在單執行緒環境下。在異步程式設計中，建議使用 await 來等待異步操作完成，以獲得更好的效能和可靠性。
//             */
//             var results = await Task.WhenAll(lst);
//             foreach (var result in results)
//             {
//                 Console.Write(result);
//                 ReportResult(result);
//             }
//             #endregion
//         }
//         private void ReportResult(string result)
//         {
//             text += result;
//         }
//     }

//     public class Content
//     {
//         public static readonly IEnumerable<string> Websites = new string[]
//         {
//             "https://www.infoq.cn/article/bwj0-wltb8enw7r0iocp",
//             "https://kknews.cc/zh-tw/tech/lzbv669.html",
//             "https://ithelp.ithome.com.tw/articles/10248883?sc=iThelpR",
//             "https://blog.csdn.net/qq_39312554/article/details/126302880",
//             "https://blog.csdn.net/qq_39312554/article/details/126302880",
//             "https://blog.csdn.net/qq_39312554/article/details/126302880",
//             "https://blog.csdn.net/qq_39312554/article/details/126302880",
//             "https://blog.csdn.net/qq_39312554/article/details/126302880",
//             "https://blog.csdn.net/qq_39312554/article/details/126302880",
//             "https://blog.csdn.net/qq_39312554/article/details/126302880",
//             "https://blog.csdn.net/qq_39312554/article/details/126302880",
//             "https://learn.microsoft.com/zh-tw/azure/event-hubs/event-hubs-kafka-connect-tutorial",
//             "https://www.mobile01.com/",
//             "https://www.mobile01.com/topicdetail.php?f=336&t=6796503",
//             "https://tw.yahoo.com/",
//             "https://www.gamer.com.tw/",
//             "https://www.irentcar.com.tw/iRentSchool/",
//             "https://www.irentcar.com.tw/",
//             "https://www.hotcar.com.tw/",
//             "https://www.hotcar.com.tw/",
//             "https://www.hotcar.com.tw/",
//             "https://www.hotcar.com.tw/",
//             "https://www.hotcar.com.tw/",
//         };
//     }
// }