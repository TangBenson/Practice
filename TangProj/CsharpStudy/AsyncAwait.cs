// // 在比較久以前的.NET Framework版本 大家可能直接操作過Thread，一言以蔽之 在絕大部份情況下，建議大家使用 Task 取代 Thread
// using System;
// using System.Net;
// using System.Threading.Tasks;

// namespace CsharpStudy;
// class AsyncAwait
// {
//     static async Task Main(string[] args)
//     {
//         Console.WriteLine("開始");

//         // 同步
//         // string content = MyDownloadPage("http://huan-lin.blogspot.com");
//         // Console.WriteLine("YY");
//         // Console.WriteLine("1.網頁內容總共為 {0} 個字元。", content.Length);
//         // Console.WriteLine("同步程式結束");

//         // 非同步
//         Task<string> task = MyDownloadPageAsync("https://www.huanlintalk.com/");
//         Console.WriteLine("YY");
//         string content2 = await task; //也可用task.Result，該属性是一个阻塞操作，它会等待任务完成并返回结果。在异步方法中，推荐使用 await 关键字来等待任务的完成，而不是使用 Result 属性
//         Console.WriteLine("2.網頁內容總共為 {0} 個字元。", content2.Length);
//         Console.WriteLine("按任意結束");
//         Console.ReadKey();

//         // 非同步 (差在YY印出時間，想想為何)
//         // string content2 = await MyDownloadPageAsync("https://www.huanlintalk.com/");
//         // Console.WriteLine("YY");
//         // Console.WriteLine("2.網頁內容總共為 {0} 個字元。", content2.Length);
//         // Console.WriteLine("按任意結束");
//         // Console.ReadKey();


//         /*
//         當執行到 await Task.Delay(3000) 時，它會建立一個延遲的非同步操作，並指示執行緒在指定的時間（3000 毫秒）後繼續執行。
//         當執行到 await Task.Delay(3000) 時，它會使該非同步操作進入等待狀態，並將控制權返回給呼叫該方法的程式碼（在這裡是 Main 方法）。
//         由於 Main 方法是 async Task 型別的，當執行到 await Task.Delay(3000) 時，它會將該非同步操作包裝成一個 Task 物件並返回，表示 Main 方法的非同步操作。
//         接著，當 await Task.Delay(3000) 的非同步操作進入等待狀態時，Main 方法繼續執行下一行指令，即 Console.WriteLine("b")。
//         這行指令會立即執行，並在等待時間（3000 毫秒）結束後輸出 "b"

//         請注意，Main 方法是應用程式的進入點，它是一個特殊的方法，不同於其他一般方法。
//         在 Main 方法中使用 async Task 的方式是非同步地執行應用程式的進入點，但它並不會讓應用程式等待所有非同步操作完成。
//         因此，在某些情況下，如果 Main 方法結束而尚未等待所有非同步操作完成，應用程式可能會提前結束並退出。
//         */
//         // Console.WriteLine("a");
//         // await Task.Delay(3000);
//         // Console.WriteLine("b");
//     }

//     static string MyDownloadPage(string url)
//     {
//         Console.WriteLine("同步方法");
//         Thread.Sleep(3000);
//         // 自己宣告HttpClient，不用注入的方式，因為在.net6 console專案我不知道怎麼注入!!!!
//         using (HttpClient client = new HttpClient())
//         {
//             Console.WriteLine("1");
//             using (HttpResponseMessage response = client.GetAsync(url).Result)
//             {
//                 Console.WriteLine("2");
//                 using (HttpContent content = response.Content)
//                 {
//                     var json = content.ReadAsStringAsync().Result;
//                     return json.ToString();
//                 }
//             }
//         }
//     }

//     static async Task<string> MyDownloadPageAsync(string url)
//     {
//         Console.WriteLine("執行MyDownloadPageAsync");
//         /*
//         當執行到 await Task.Delay(3000) 時，程式碼會跳出 MyDownloadPageAsync 方法並返回到 Main 方法繼續執行後續的指令。
//         這是因為 await Task.Delay(3000) 是一個非同步操作，會將控制權返回給呼叫該方法的程式碼。
//         當執行到 await Task.Delay(3000) 時，它會建立一個延遲的非同步操作，並指示執行緒在指定的時間（這裡是 3000 毫秒）後繼續執行。
//         這種非同步等待的好處是，它不會阻塞主執行緒，允許其他工作在等待的同時繼續執行。
//         在延遲時間結束後（這裡是 3000 毫秒），執行緒將再次進入 MyDownloadPageAsync 方法，繼續執行剩餘的程式碼，包括發送 HTTP 請求並處理回應的部分。
//         這種非同步的設計允許主執行緒在等待較長的操作（如延遲、網路請求等）期間不被阻塞，而可以繼續執行其他工作，從而提高應用程式的效能和回應能力。
//         */
//         await Task.Delay(5000);
//         using (HttpClient client = new HttpClient())
//         {
//             Console.WriteLine("7");
//             using (HttpResponseMessage response = await client.GetAsync(url)) // 執行到這行後會跳出該 method回到 Main method繼續往下做
//             {
//                 Console.WriteLine("8");
//                 string responseBody = await response.Content.ReadAsStringAsync(); //async method下，用 await取代.Result，網上說這樣比較好
//                 Console.WriteLine("9");
//                 return responseBody;
//             }
//         }
//     }
// }

// /*
// 你不會在 C# 的任何一個介面上看到 public async Task<Result> IXxx(); 正確應該是 public Task<Result> IXxx(); 因為 async 是非同步的實作細節，你不該也不需要在介面上定義實作細節

// 宣告方法時加上關鍵字 async，即告訴 C# 編譯器：我要在這個方法內進行一個非同步的任務，而且我需要在這個方法內等待某個非同步任務完成後，繼續進行之後的任務。
// async方法內會用到 await 關鍵字，有了 await，Compiler 才知道非同步的斷點在哪裡
// 原先同步版本的方法是傳回 string ，此處改為 Task<string>。非同步方法若不需要傳回值，則回傳型別應宣告為 Task，而不要寫 void
// 雖然非同步方法也可以宣告為 void，但此寫法通常只用於事件處理常式。一般而言，應避免使用 void 來宣告沒有回傳值的非同步方法。
// 無回傳值的非同步方法不應宣告為 async void 的一個重要原因，是這種寫法會令呼叫端捕捉不到非同步方法所拋出的異常

// 有用到 await 的函式都必須在宣告時加上 async 關鍵字，Main 方法都是宣告為 void，
// 這使得我們無法在 Main 方法中使用 await 語法來獲取非同步工作的執行結果，而得用 Task.Result 屬性。
// C# 7.1 開始加入的 Async Main 語法便解決了這個問題，讓我們從程式一開始的
// 進入點就能使用 async 和 await 語法。

// 如果今天沒有 async關鍵字，你就必須要自己把任務做成一個一個的 Callback，並且把這些任務放到 Task裡，然後不停的寫 ContinueWith。
// 當你寫了 async時，Compiler就會大聲說「別擔心，讓我替你處理各種麻煩事」。Compiler會自動幫你生成一個狀態機，而是有了 await，
// Compiler 才知道非同步的斷點在哪裡，才有辦法幫你生成狀態機，當下次再進來繼續執行時，接續下去。如果你完全不需要寫 await，Compiler也不需要幫你生成狀態機，
// 你的程式只要順順執行下去，那寫 async幾乎是不必要的。

// 關於 task，我們還需要多知道兩個常用的方法與特性，一個是 Task 的 Wait() , 一個是 Task 的 .Result。
// 這兩個方法都是用在等待任務完成，Wait 不會有回傳值，而 Result 會有任務完成的回傳結果。但不論是哪種方式，呼叫這兩個，都會造成當前的 thread被 block住，
// 直到任務完成，才能繼續執行。

// 非同步方法簽名的返回值有以下三種：
// 1. Task<T>：如果呼叫方法想通過呼叫非同步方法獲取一個 T型別的返回值，那麼簽名必須為Task<TResult>；
// 2. Task:如果呼叫方法不想通過非同步方法獲取一個值，僅僅想追蹤非同步方法的執行狀態，那麼我們可以設定非同步方法簽名的返回值為 Task;
// 3. void:如果呼叫方法僅僅只是呼叫一下非同步方法，不和非同步方法做其他互動，我們可以設定非同步方法簽名的返回值為 void，這種形式也叫做“呼叫並忘記”

// async/await是基於 Task的，而 Task是對 ThreadPool的封裝改進，主要是為了更有效的控制執行緒池中的執行緒 (ThreadPool中的執行緒，我們很難通過程式碼控制其執行順序，任務延續和取消等等)；
// ThreadPool基於 Thread的，主要目的是減少 Thread建立數量和管理Thread的成本。async/await Task是C#中更先進的，也是微軟大力推廣的特性，
// 我們在開發中可以嘗試使用 Task來替代 Thread/ThreadPool，處理本地 IO和網路 IO任務是儘量使用 async/await來提高任務執行效率
// */
