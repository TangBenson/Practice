// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace CsharpStudy
// {
//     public class AsyncAwait_deadlock
//     {
//         private static object lockA = new object();
//         private static object lockB = new object();

//         public static void Main()
//         {
//             Thread threadA = new Thread(DoWorkA);
//             Thread threadB = new Thread(DoWorkB);
//             Console.WriteLine("Q");
//             threadA.Start();
//             threadB.Start();
//             Console.WriteLine("Y");
            
//             // 等待两个线程执行完成
//             threadA.Join();
//             threadB.Join();

//             Console.WriteLine("程序结束");
//         }

//         private static void DoWorkA()
//         {
//             lock (lockA)
//             {
//                 Console.WriteLine("thread A獲取了 lock A");

//                 // 线程A等待一段时间，让线程B有机会获取锁B
//                 Thread.Sleep(1000);

//                 Console.WriteLine("thread A嘗試獲取 lock B");

//                 lock (lockB)
//                 {
//                     Console.WriteLine("thread A獲取了 lock B");
//                 }
//             }
//         }

//         private static void DoWorkB()
//         {
//             lock (lockB)
//             {
//                 Console.WriteLine("thread B獲取了 lock B");

//                 Console.WriteLine("thread B嘗試獲取 lock A");

//                 lock (lockA)
//                 {
//                     Console.WriteLine("thread B獲取了 lock A");
//                 }
//             }
//         }
//     }
// }