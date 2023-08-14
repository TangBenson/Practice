// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;

// namespace CsharpStudy
// {
//     public class AsyncAwait7_ThisIsWhatIwant
//     {
//         static async Task Main(string[] args)
//         {
//             Console.WriteLine("開始~~");
//             Stopwatch stopwatch = new Stopwatch();
//             stopwatch.Start();

//             #region 同步
//             // List<bool> lst = new List<bool>();
//             // lst.Add(Q());
//             // Console.WriteLine("1");
//             // lst.Add(W());
//             // Console.WriteLine("2");
//             // lst.Add(E());
//             // Console.WriteLine("3");
//             // lst.Add(R());
//             // Console.WriteLine("4");
//             // foreach(var a in lst){
//             //     Console.WriteLine(a); 
//             // }
//             #endregion

//             #region 非同步
//             List<Task<bool>> lst = new List<Task<bool>>();
//             //Task.Run會將 Q()方法包裝成一個非同步的 Task，然後在背景執行。程式碼會立即繼續執行下一行，而不會等待 Q()方法完成
//             lst.Add(Task.Run(() => Q()));
//             Console.WriteLine("1");
//             lst.Add(Task.Run(() => W()));
//             Console.WriteLine("2");
//             lst.Add(Task.Run(() => E()));
//             Console.WriteLine("3");
//             lst.Add(Task.Run(() => R()));
//             Console.WriteLine("4");
//             var results = await Task.WhenAll(lst);
//             foreach (var result in results)
//             {
//                 Console.Write(result);
//             }
//             // foreach (var a in lst) // 等同上面 WhenAll再 foreach
//             // {
//             //     bool result = await a;
//             //     Console.WriteLine(result);
//             // }
//             #endregion

//             #region 非同步優畫版
//             // List<Task<bool>> lst = new List<Task<bool>>();
//             // lst.Add(Qasync());
//             // Console.WriteLine("1");
//             // lst.Add(Wasync());
//             // Console.WriteLine("2");
//             // lst.Add(Easync());
//             // Console.WriteLine("3");
//             // lst.Add(Rasync());
//             // Console.WriteLine("4");
//             // var results = await Task.WhenAll(lst);
//             // foreach (var result in results)
//             // {
//             //     Console.Write(result);
//             // }
//             #endregion

//             stopwatch.Stop();
//             TimeSpan elapsed = stopwatch.Elapsed;
//             double milliseconds = elapsed.TotalMilliseconds;
//             Console.WriteLine(milliseconds);
//         }

//         private static bool Q()
//         {
//             Console.WriteLine("Q");
//             Thread.Sleep(2000);
//             return true;
//         }
//         private static bool W()
//         {
//             Console.WriteLine("W");
//             Thread.Sleep(1500);
//             return false;
//         }
//         private static bool E()
//         {
//             Console.WriteLine("E");
//             Thread.Sleep(2000);
//             return true;
//         }
//         private static bool R()
//         {
//             Console.WriteLine("R");
//             Thread.Sleep(1000);
//             return true;
//         }

//         private static async Task<bool> Qasync()
//         {
//             Console.WriteLine("Qasync");
//             await Task.Delay(2000);
//             return true;
//         }
//         private static async Task<bool> Wasync()
//         {
//             Console.WriteLine("Wasync");
//             await Task.Delay(1500);
//             return false;
//         }
//         private static async Task<bool> Easync()
//         {
//             Console.WriteLine("Easync");
//             await Task.Delay(2000);
//             return true;
//         }
//         private static async Task<bool> Rasync()
//         {
//             Console.WriteLine("Rasync");
//             await Task.Delay(1000);
//             return true;
//         }
//     }
// }