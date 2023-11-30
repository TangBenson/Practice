using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait;
/// <summary>
/// 示範 Task 
/// tip: 關鍵字: wait系列, await, result, getResult 要等, 其他都不用
/// </summary>
class AsyncAwait2
{
    internal static void Main(string[] args)
    {
        //Task.Start() => 不等
        Task slowTask = new Task(() => RunSlowly("【7】我是 SlowTask 我等 1 秒"));
        slowTask.Start();

        //Task.Factory.StartNew() => 不等
        Task.Factory.StartNew(() => RunQuickly("【3】我是 QuickTask 我等 0.3 秒"));
        Console.WriteLine("【1】我是主程式 我不用等 slowTask 跟 quickTask 我會最先跑");
        //【Run → Result】-------------------------------------------------------------------------------
        //Task.Run() => 不等
        var mediumTask = Task.Run<string>(() => { return GetReturn("【4】我是 MediumTask 我等 0.6 秒"); });
        Console.WriteLine("【2】並不是 Task.Run 阻塞主程式");

        //Task.Result => 要等
        Console.WriteLine(mediumTask.Result);
        Console.WriteLine("【5】而是 MediumTask.Result 讓主程式被迫等待");
        //------------------------------------------------------------------------------------------------
        //【Start → Wait】-------------------------------------------------------------------------------
        //Task.Start() => 不等
        Task dbTask = new Task(() => GetDataFromDb("【8】花了 2 秒鐘將資料從 DB 取回"));
        dbTask.Start();
        Console.WriteLine("【6】並不是 Task.Start 阻塞主程式");
        //Task.Wait() => 要等
        dbTask.Wait();
        Console.WriteLine("【9】而是 Task.Wait() 讓主程式被迫等待");
        //------------------------------------------------------------------------------------------------
        Console.Read();
    }

    static void RunQuickly(string str)
    {
        Thread.Sleep(300);
        Console.WriteLine(str);
    }

    static void RunSlowly(string str)
    {
        Thread.Sleep(1000);
        Console.WriteLine(str);
    }

    static string GetReturn(string str)
    {
        Thread.Sleep(600);
        return str;
    }

    static void GetDataFromDb(string str)
    {
        Thread.Sleep(2000);
        Console.WriteLine(str);
    }
}