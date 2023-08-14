// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace CsharpStudy
// {
//     public class AsyncAwait11
//     {
//         static async Task Main(string[] args)
//         {
//             Console.WriteLine("開始");

//             #region 測試情境一
//             // // 模擬 MessageBusSubscriber不斷收到新消息並呼叫 EventProcessor去打 API
//             // int count = 0;
//             // while (count < 5)
//             // {
//             //     count++;
//             //     Task.Run(() => EventProcessor());
//             //     // EventProcessor2(); // 效果同上
//             //     Console.WriteLine($"完成{count}");
//             // }
//             // Console.ReadKey();
//             #endregion

//             #region 測試情境二 (用await等待回傳一個Task物件 & 用Task.wait而不用await)
//             // string a = await Test2();
//             // Console.WriteLine(a);

//             Task<string> task = Test2();
//             task.Wait();
//             Console.WriteLine(task.Result);
//             #endregion
//         }
//         static void EventProcessor()
//         {
//             //模擬打 API
//             PushMessage();
//         }

//         static void PushMessage()
//         {
//             Thread.Sleep(3000);
//             Console.WriteLine("完成");
//         }

//         static async Task EventProcessor2()
//         {
//             //模擬打 API
//             await PushMessage2();
//         }

//         static async Task PushMessage2()
//         {
//             await Task.Delay(3000);
//             Console.WriteLine("完成");
//         }

//         static async Task<string> Test2(){
//             await Task.Delay(3000);
//             return "dsff";
//         }
//     }
// }