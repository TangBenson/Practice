// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace CsharpStudy
// {
//     public delegate void Bb(int q);
//     class AsyncAwait0
//     {
//         static async Task Main(string[] args) // static void Main(string[] args)
//         {
//             #region 範例0 (執行Task)
//             // Bb b = new Bb((q) =>
//             // {
//             //     Console.WriteLine("DIABLO IV");
//             //     Console.WriteLine($"執行緒：{Thread.CurrentThread.ManagedThreadId}");
//             //     Thread.Sleep(3000);
//             // });
//             // b.Invoke(5);
//             // Console.WriteLine("End0");


//             // //1.new方式例項化一個 Task，需要通過 Start方法啟動            
//             // Task task = new Task(() =>
//             // {
//             //     Console.WriteLine("A");
//             //     Thread.Sleep(3000);
//             //     Console.WriteLine($" Task1的執行緒ID為{Thread.CurrentThread.ManagedThreadId}");
//             // });
//             // task.Start(); //若要用同步方式則用 task.RunSynchronously();
//             // Console.WriteLine("End1");


//             // //2.Task.Factory.StartNew(Action action)建立和啟動一個 Task            
//             // Task task2 = Task.Factory.StartNew(() =>
//             // {
//             //     Console.WriteLine("B");
//             //     Thread.Sleep(3000);
//             //     Console.WriteLine($" Task2的執行緒ID為{Thread.CurrentThread.ManagedThreadId}");
//             // });
//             // Console.WriteLine("End2");


//             // //3.Task.Run(Action action)將任務放線上程池佇列，返回並啟動一個 Task
//             // Task task3 = Task.Run(() =>
//             // {
//             //     Console.WriteLine("C");
//             //     Thread.Sleep(3000);
//             //     Console.WriteLine($" Task3的執行緒ID為{Thread.CurrentThread.ManagedThreadId}");
//             // });
//             // Console.WriteLine("End3");


//             // //1.new方式例項化一個 Task，需要通過 Start方法啟動
//             // Task<string> task4 = new Task<string>(() =>
//             // {
//             //     return $" Task4的執行緒ID為{Thread.CurrentThread.ManagedThreadId}";
//             // });
//             // task4.Start();


//             // //2.Task.Factory.StartNew(Func func)建立和啟動一個Task
//             // Task<string> task5 = Task.Factory.StartNew<string>(() =>
//             // {
//             //     return $" Task5的執行緒ID為{Thread.CurrentThread.ManagedThreadId}";
//             // });


//             // //3.Task.Run(Func func)將任務放線上程池佇列，返回並啟動一個Task
//             // Task<string> task6 = Task.Run<string>(() =>
//             // {
//             //     return $" Task6的執行緒ID為{Thread.CurrentThread.ManagedThreadId}";
//             // });


//             // Console.WriteLine("執行主執行緒！");
//             // Console.WriteLine(task4.Result); //注意task.Resut獲取結果時會阻塞執行緒，即如果 task沒有執行完成，會等待 task執行完成獲取到 Result，然後再執行後邊的程式碼
//             // Console.WriteLine(task5.Result);
//             // Console.WriteLine(task6.Result);
//             // Console.WriteLine("End~~~~~");
//             // Console.ReadKey();
//             #endregion

//             #region 範例0 (等待/取消Task)
//             // Task.Wait() 表示等待task執行完畢
//             // Task.WaitAll(Task[] tasks) 表示只有所有的 task都執行完成了再解除阻塞 
//             // Task.WaitAny(Task[] tasks) 表示只要有一個 task執行完畢就解除阻塞
//             Console.WriteLine("start");
//             Task task1 = new Task(() =>
//             {
//                 Thread.Sleep(3000);
//                 Console.WriteLine("工作1執行完畢！");
//             });
//             task1.Start();
//             Task task2 = new Task(() =>
//             {
//                 Thread.Sleep(3000);
//                 Console.WriteLine("工作2執行完畢！");
//             });
//             task2.Start();
//             //執行【task1.Wait();task2.Wait();】可以實現相同功能
//             Task.WaitAll(new Task[] { task1, task2 });
//             Console.WriteLine("工作1,2執行完畢！");
//             Console.ReadKey();

//             // Task.WhenAll(Task[] tasks)  表示所有的 task都執行完畢後再去執行後續的操作
//             // Task.WhenAny(Task[] tasks)  表示任一 task執行完畢後就開始執行後續操作
//             Console.WriteLine("start-2");
//             Task task3 = new Task(() =>
//             {
//                 Thread.Sleep(3000);
//                 Console.WriteLine("工作3執行完畢！");
//             });
//             task3.Start();
//             Task task4 = new Task(() =>
//             {
//                 Thread.Sleep(3000);
//                 Console.WriteLine("工作4執行完畢！");
//             });
//             task4.Start();
//             // Task.WhenAll(task3, task4).ContinueWith((t) =>
//             // {
//             //     Thread.Sleep(3000);
//             //     Console.WriteLine("執行後續操作完畢！");
//             // });
//             await Task.WhenAll(task3, task4); //取代上面 ContinueWith那塊寫法
//             Thread.Sleep(3000);
//             Console.WriteLine("執行後續操作完畢！");
//             Console.WriteLine("工作3,4執行完畢！");
//             Console.ReadKey();

//             //Task.Factory.ContinueWhenAll 同 Task.WhenAll
//             //Task.Factory.ContinueWhenAny 同 Task.WhenAny
//             Console.WriteLine("start-3");
//             Task task5 = new Task(() =>
//             {
//                 Thread.Sleep(3000);
//                 Console.WriteLine("工作5執行完畢！");
//             });
//             task5.Start();
//             Task task6 = new Task(() =>
//             {
//                 Thread.Sleep(3000);
//                 Console.WriteLine("工作6執行完畢！");
//             });
//             task6.Start();
//             //通過TaskFactroy實現
//             // Task.Factory.ContinueWhenAll(new Task[] { task5, task6 }, (t) =>
//             // {
//             //     Thread.Sleep(3000);
//             //     Console.WriteLine("執行後續操作");
//             // });
//             await Task.WhenAll(task5, task6); //推荐使用 Task.WhenAll 方法来等待多个任务完成，而不是 Task.Factory.ContinueWhenAll 方法
//             Thread.Sleep(3000);
//             Console.WriteLine("執行後續操作");
//             Console.WriteLine("工作5,6執行完畢！");
//             Console.ReadKey();

//             //Task中有一個專門的類 CancellationTokenSource  來取消任務執行
//             CancellationTokenSource source = new CancellationTokenSource();
//             int index = 0;
//             //開啟一個task執行任務
//             Task task7 = new Task(() =>
//               {
//                   while (!source.IsCancellationRequested)
//                   {
//                       Thread.Sleep(1000);
//                       Console.WriteLine($"第{++index}次執行，執行中...");
//                   }
//               });
//             task7.Start();
//             //五秒後取消任務執行
//             //延時取消，效果等同於Thread.Sleep(5000);source.Cancel();
//             source.CancelAfter(5000);
//             Console.ReadKey();
//             #endregion

//             #region 範例1
//             // A objA = new A();
//             // Console.WriteLine("請輸入代號：");
//             // string? a_in = Console.ReadLine();
//             // switch (a_in)
//             // {
//             //     case "1":
//             //         objA.M0();
//             //         break;
//             //     case "2":
//             //         objA.M1(); //顯示黃毛毛蟲很煩，若讓 M1有回傳值就不會顯示了，奇怪
//             //         break;
//             //     case "3":
//             //         Task.Run(objA.M1); //先跑此行後面的內容才會去跑 M1
//             //         break;
//             //     case "4":
//             //         await objA.M1(); //等同 case "1"
//             //         break;
//             //     default:
//             //         Console.WriteLine($"沒東西");
//             //         break;
//             // }
//             // Console.WriteLine($"ID__:{Thread.CurrentThread.ManagedThreadId}");
//             // Console.Write($"Press any key to Exist...{Environment.NewLine}");
//             // Console.ReadKey();
//             #endregion

//             #region 範例2
//             // for (int r = 0; r < 1; r++)
//             // {
//             //     // Task task1 = new Task(CalApi.TestSleepAsync);
//             //     // task1.Start();
//             //     Task task1 = Task.Run(CalApi.TestSleepAsync);//等同上面兩行
//             //     Console.WriteLine("1");
//             //     Task task2 = Task.Run(() => TestSleepAsync());
//             //     Console.WriteLine("2");

//             //     Console.WriteLine("慢慢睡我先走");
//             // }
//             // Console.WriteLine($"Press any key to Exist...");
//             // Console.ReadKey();
//             #endregion

//             #region 範例3
//             // Console.WriteLine("Start1");
//             // Task task1 = Task.Run(CalApi.TestSleepAsync);
//             // Console.WriteLine("Start2");
//             // Task task2 = Task.Run(() => TestSleepAsync());
//             // Console.WriteLine("End");
//             // Task.WhenAll(task1, task2).Wait(); //遇到這行程式會暫停，待非同步都執行完再執行下一行
//             // Console.WriteLine("EndAll");
//             #endregion

//             #region 範例4
//             // Console.WriteLine("Start");
//             // Task task1 = Task.Run(CalApi.TestSleepAsync);
//             // Console.WriteLine("End");
//             // task1.Wait(); //遇到這行程式會暫停，待非同步都執行完再執行下一行
//             // Console.WriteLine("End2");
//             #endregion

//             #region 範例5
//             // Console.WriteLine("Start");
//             // Task<string> task1 = Task.Run<string>(MyMethod1); // 同 var task1 = Task.Run(MyMethod11);
//             // Task<int> task2 = Task.Run<int>(() => MyMethod2("-")); //同 var task2 = Task.Run(() => MyMethod21("-"));
//             // Task.WhenAll(task1, task2).Wait();
//             // Console.WriteLine();
//             // Console.WriteLine($"task1 的執行結果為 {task1.Result}");
//             // Console.WriteLine($"task1 的執行結果為 {task2.Result}");
//             #endregion

//             #region 範例6
//             // Console.WriteLine("Start");
//             // Task<string> task1 = Task.Run<string>(MyMethod1);
//             // Console.WriteLine("1");
//             // task1.GetAwaiter().OnCompleted(() =>
//             // {
//             //     // GetAwaiter():等待完成, OnCompleted():完成後要做的事。
//             //     // 這和 wait有啥差?=>應該是能特別指定非同步工作結束要做的事，不然若寫wait會造成後面程式全都不會執行。會一起執行?=>Yes
//             //     Console.WriteLine($"task1.Result:{task1.Result}");
//             //     Task<int> task2 = new Task<int>(()=>MyMethod2("-"));
//             //     task2.Start();
//             //     var result = task2.GetAwaiter().GetResult(); // GetAwaiter():等待完成, GetResult():取得結果
//             //     Console.WriteLine();
//             //     Console.WriteLine($"result:{result}");
//             // });
//             // Console.WriteLine("2");
//             // task1.Wait();
//             // Console.WriteLine();
//             // Console.WriteLine("結束");
//             // Console.ReadLine();
//             #endregion

//             #region 範例7 (與範例8為對比組)
//             // Task subThread1 = new Task(() => createTask(1));
//             // Task subThread2 = new Task(() => createTask(2));
//             // Task subThread3 = new Task(() => createTask(3));
//             // Task subThread4 = new Task(() => createTask(4));
//             // static void createTask(int threadNum)
//             // {
//             //     Thread.Sleep(2000);
//             //     Console.WriteLine($"I am subThread{threadNum}!");
//             // }
//             // subThread2.Start();
//             // subThread1.Start();
//             // Console.WriteLine("A");
//             // subThread1.GetAwaiter().OnCompleted(() =>
//             // {
//             //     subThread4.Start();
//             //     subThread3.Start();
//             // });
//             // Console.WriteLine("B");
//             // //這裡沒有回傳值，所以不宣告變數來接，你問為何不要寫GetResult就好? 因為這樣GetAwaiter也會無效，超妙
//             // subThread2.GetAwaiter().GetResult();
//             // // 等待線程完成才繼續往下走
//             // Console.WriteLine("C");
//             // subThread3.Wait();
//             // subThread4.Wait();
//             // Console.WriteLine("D");
//             #endregion

//             #region 範例8 (與範例7為對比組)
//             // Console.WriteLine("Start");
//             // int n = await main(); // 若程式進入點 Main()沒有宣告成async Task，則要改 int n = main().GetAwaiter().GetResult();
//             // Console.WriteLine("FF XV");
//             // Console.WriteLine(n);
//             // Console.WriteLine("FF XVI");
//             // static async Task createTask(int threadNum)
//             // {
//             //     await Task.Delay(2000);
//             //     Console.WriteLine($"I am subThread{threadNum}!");
//             // }
//             // static async Task<int> main()
//             // {
//             //     Task subThread1 = createTask(1);//創建且執行
//             //     Task subThread2 = createTask(2);
//             //     Console.WriteLine("A");
//             //     await subThread1; // 等待1號任務完成，= subThread1.GetAwaiter().GetResult();
//             //     Console.WriteLine("B");
//             //     Task subThread3 = createTask(3);
//             //     Task subThread4 = createTask(4);
//             //     // 等待以下任務完成後 return 
//             //     await subThread2;
//             //     await subThread3;
//             //     await subThread4;
//             //     return 1;
//             // }
//             #endregion

//             #region 範例9
//             #endregion

//             #region 範例10
//             #endregion

//             #region 範例11
//             #endregion

//             #region 範例12
//             #endregion

//             #region 範例13
//             #endregion

//             //範例?
//             // for (int r = 0; r < 3; r++)
//             // {
//             //     await CalApi.TestSleepAsync2();      
//             //     Console.WriteLine("慢慢睡我先走");
//             // }
//             // Console.ReadLine();


//         }
//         public static void TestSleepAsync()
//         {
//             Console.WriteLine("睡3");
//             Thread.Sleep(3000);
//             Console.WriteLine("睡3秒結束");
//         }
//         static string MyMethod1()
//         {
//             Thread.Sleep(3000);
//             for (int i = 0; i < 800; i++)
//             {
//                 Console.Write("*");
//             }
//             return "loop is 800";
//         }
//         static int MyMethod2(object message)
//         {
//             Thread.Sleep(3000);
//             for (int i = 0; i < 500; i++)
//             {
//                 Console.Write(message.ToString());
//             }
//             return 500;
//         }
//     }
//     class A
//     {
//         internal void M0()
//         {
//             Console.WriteLine($"M0 開始時間 : {DateTime.UtcNow.ToString("mm:ss.ffff")}");
//             Console.WriteLine($"M0 Thread ID:{Thread.CurrentThread.ManagedThreadId}");
//             Thread.Sleep(5000);
//             Console.WriteLine($"M0 Thread ID_:{Thread.CurrentThread.ManagedThreadId}");
//             Console.WriteLine($"M0 結束時間 : {DateTime.UtcNow.ToString("mm:ss.ffff")}");
//         }
//         internal async Task M1()
//         {
//             Console.WriteLine($"M1 開始時間 : {DateTime.UtcNow.ToString("mm:ss.ffff")}");
//             Console.WriteLine($"M1 Thread ID:{Thread.CurrentThread.ManagedThreadId}");
//             await Task.Delay(5000);
//             Console.WriteLine($"M1 Thread ID_:{Thread.CurrentThread.ManagedThreadId}");
//             Console.WriteLine($"M1 結束時間 : {DateTime.UtcNow.ToString("mm:ss.ffff")}");
//         }
//     }

//     class CalApi
//     {
//         static void TestSleepAsync()
//         {
//             Console.WriteLine("睡5");
//             Thread.Sleep(5000);
//             Console.WriteLine("睡5秒結束");
//         }
//         // public static Task TestSleepAsync2()
//         // {
//         //     Thread.Sleep(4000);
//         //     Console.WriteLine("睡4秒結束");
//         // }
//         // static Task<string> Err1()
//         // {
//         //     Thread.Sleep(4000);
//         //     return "Hello";
//         // }
//     }
// }