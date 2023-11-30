using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncAwait;

public class AsyncAwait1
{
    /*
    Task.Delay和 Thread.Sleep是完全不一樣的東西，Thread.Sleep就是對本身自己這個執行緒，
    進行睡眠的功能。而反觀 Task.Delay是指，在本身這個執行緒下再去生出一個新的執行緒，
    並對這個執行緒做時間計算。試想，父執行緒生出一個子執行緒，並且叫子執行緒等候1秒，此時父執行緒的下一行早就被運行了，
    也就是說，子執行緒有沒有跑完根本不關父執行緒的事情了。
    總結：若在非同步方法中需要寫睡眠，就用 await Task.Delay(xxx);
    */
    internal static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("請輸入選項:");
            string? a_in = Console.ReadLine();
            switch (a_in)
            {
                case "1":
                    RunTest_1();
                    break;
                case "2":
                    RunTest_2();
                    break;
                case "3":
                    RunTest_3();
                    break;
                case "4":
                    RunTest_3_1();
                    break;
                case "5":
                    RunTest_4();
                    break;
            }
            Console.WriteLine("----------問世間情為何物----------");
            Console.ReadKey(); //讓自己決定程式結束時間，才能等非同步程式處理完
        }
    }

    static void RunTest_1()
    {
        Console.WriteLine("Step 1.A");
        Thread.Sleep(5000);
        Console.WriteLine("Step 1.B");
    }

    static void RunTest_2()
    {
        Console.WriteLine("Step 2.A");
        Task.Delay(5000); //完全沒屁用
        Console.WriteLine("Step 2.B");
    }

    static async void RunTest_3()
    {
        Console.WriteLine("Step 3.A");
        await Task.Delay(5000); //有用
        Console.WriteLine("Step 3.B");
    }

    static async void RunTest_3_1()
    {
        Console.WriteLine("Step 3_1.A");
        await Delay(5000);
        Console.WriteLine("Step 3_1.B");
    }

    static void RunTest_4()
    {
        Console.WriteLine("Step 4.A");
        Task.Delay(5000).ContinueWith(_ => Console.WriteLine("I'm End."));  //Task.ContinueWith 告訴你，這個 Task 完成後，接下來要做什麼。底線的寫法代表使用lambda時，如果不使用到傳入參數，就要打一個底線。
        Console.WriteLine("Step 4.B");
    }

    static async Task Delay(int iSecond)
    {
        Console.WriteLine("Waiting Start.");
        await Task.Delay(iSecond);
        Console.WriteLine("Waiting End.");
    }
}
