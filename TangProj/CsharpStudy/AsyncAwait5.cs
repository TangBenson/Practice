// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;

// namespace CsharpStudy;
// //除了是在綁定事件的方法內，其他任何情況下，對於您開發的工作式非同步模式方法，就不需要使用async void這樣的回傳值標示。
// class A
// {
//     //M1 是我們使用 async void 的方式建立的方法
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
//     //M2 則是正常的工作式非同步模式方法的程式碼寫法
//     public async Task M2()
//     {
//         var foo = DateTime.UtcNow.ToString("mm:ss.ffff");
//         Console.WriteLine($"M2 開始時間 : {foo}");
//         await Task.Delay(1000);
//         foo = DateTime.UtcNow.ToString("mm:ss.ffff");
//         Console.WriteLine($"M2 結束時間 : {foo}");
//     }
// }
// class AsyncAwait5
// {
//     static async Task Main(string[] args)
//     {
//         A objA = new A();

//         Console.WriteLine($"Main-1 {Thread.CurrentThread.ManagedThreadId}");
//         objA.M1();
//         await objA.M2(); //加await表示執行完M2後再跳回Main繼續執行
//         Console.WriteLine($"Main-2 {Thread.CurrentThread.ManagedThreadId}");
//         Console.WriteLine($"Press any key to Exist...{Environment.NewLine}");
//         Console.ReadKey();
//     }
// }
// //objA.M1() 方法一執行到 await Task.Delay(3000);敘述之後，就立即返回到 Main 方法內，接著執行 await objA.M2();，
// //不過，此時，整個程式將會等候到 objA.M2() 方法執行完後，才會繼續進行下去，而在這個時候， M1 的方法，也持續在等候 Task.Delay 的甦醒時間