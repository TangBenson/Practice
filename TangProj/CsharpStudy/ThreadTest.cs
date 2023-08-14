// using System;
// using System.Threading;

// namespace CsharpStudy;
// class ThreadTest
// {
//     static void Main(string[] args)
//     {
//         // Thread thread = new Thread(Run);//指派工作給執行緒
//         // thread.Start();//使用thread.Start(); 來開始執行緒工作 
//         // while (true)
//         // {
//         //     Console.WriteLine("防守!防守!");
//         //     Thread.Sleep(500);
//         //     //使用Tread.Sleep()讓執行緒暫時停止工作 單位為毫秒
//         // }




//         // bool isStop = false;
//         // int index = 0;
//         // //開啟一個執行緒執行任務
//         // Thread th1 = new Thread(() =>
//         // {
//         //     while (!isStop)
//         //     {
//         //         Thread.Sleep(1000);
//         //         Console.WriteLine($"第{++index}次執行，執行緒執行中...");
//         //     }
//         // });
//         // th1.Start();
//         // //五秒後取消任務執行
//         // Thread.Sleep(5000);
//         // isStop = true;
//         // Console.ReadKey();



//         // for (int i = 1; i <= 10; i++)
//         // {
//         //     //ThreadPool執行任務                
//         //     ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
//         //     {
//         //         Console.WriteLine($"第{obj}個執行任務");
//         //     }), i);
//         // }
//         // Console.ReadKey();




//         // Thread th11 = new Thread(() =>
//         // {
//         //     Thread.Sleep(500);
//         //     Console.WriteLine("執行緒1執行完畢！");
//         // });
//         // th11.Start();
//         // Thread th2 = new Thread(() =>
//         // {
//         //     Thread.Sleep(1000);
//         //     Console.WriteLine("執行緒2執行完畢！");
//         // });
//         // th2.Start();
//         // //阻塞主執行緒
//         // th11.Join();
//         // th2.Join();
//         // Console.WriteLine("主執行緒執行完畢！");
//         // Console.ReadKey();




//         Console.WriteLine($"程式 start 執行緒:{Thread.CurrentThread.ManagedThreadId}");
//         //1.建立ThreadStart委派，沒有傳遞參數的功能
//         ThreadStart myRun = new ThreadStart(JobA);
//         Thread myThread = new Thread(myRun);
//         myThread.Start();//啟動執行緒
//         //2.建立ParameterizedThreadStart委派，有傳遞參數的功能
//         ParameterizedThreadStart myPar = new ParameterizedThreadStart(JobB);
//         Thread myThread01 = new Thread(myPar);
//         myThread01.Start("綺綺變瘦了");//啟動執行緒並帶入參數
//         Console.WriteLine($"程式 end 執行緒:{Thread.CurrentThread.ManagedThreadId}");

//         // var t1 = new Thread(() => JobA());
//         // var t2 = new Thread(() => JobB("綺綺好可愛"));
//         // t1.Start();
//         // t2.Start();
//         // Console.WriteLine($"魯拉拉:{Thread.CurrentThread.ManagedThreadId}");
//     }

//     static void Run()
//     {
//         while (true)
//         {
//             Console.WriteLine("執行緒大軍進攻拉");
//             Thread.Sleep(1000);
//         }
//     }

//     static void JobA()
//     {
//         for (int i = 0; i < 5; i++)
//         {
//             Console.WriteLine($"執行緒JobA是:{Thread.CurrentThread.ManagedThreadId}");
//         }
//     }
//     static void JobB(object para)
//     {
//         for (int i = 0; i < 5; i++)
//         {
//             Console.WriteLine($"執行緒JobB是:{Thread.CurrentThread.ManagedThreadId}-----{para}");
//         }
//     }
// }

