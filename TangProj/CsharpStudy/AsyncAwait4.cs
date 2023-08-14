// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;

// namespace CsharpStudy;
// class A
// {
//     public async void M1()
//     {
//         var foo = DateTime.UtcNow.ToString("mm:ss.ffff");
//         Console.WriteLine($"M1 開始時間 : {foo}");
//         Console.WriteLine($"M1-1 {Thread.CurrentThread.ManagedThreadId}");
//         await Task.Delay(3000);
//         Console.WriteLine($"M1-2 {Thread.CurrentThread.ManagedThreadId}");
//         foo = DateTime.UtcNow.ToString("mm:ss.ffff");
//         Console.WriteLine($"M1 結束時間 : {foo}");
//     }
//     public async Task M2()
//     {
//         var foo = DateTime.UtcNow.ToString("mm:ss.ffff");
//         Console.WriteLine($"M2 開始時間 : {foo}");
//         await Task.Delay(3000);
//         foo = DateTime.UtcNow.ToString("mm:ss.ffff");
//         Console.WriteLine($"M2 結束時間 : {foo}");
//     }
// }
// class AsyncAwait4
// {
//     static async Task Main(string[] args)
//     {
//         A objA = new A();

//         Console.WriteLine($"Main-1 {Thread.CurrentThread.ManagedThreadId}");
//         objA.M1();
//         Console.WriteLine($"Main-2 {Thread.CurrentThread.ManagedThreadId}");
//         Console.WriteLine($"Press any key to Exist...{Environment.NewLine}");
//         Console.ReadKey();
//     }
// }
// //讓我們修改測試程式碼，我們在呼叫 M1 方法前後，與在 M1 方法內，當要呼叫 await Task.Delay(3000); 的前後，
// //都顯示出當時執行緒的 ID (這裡顯示的是受管理的執行緒ID) Thread.CurrentThread.ManagedThreadId。