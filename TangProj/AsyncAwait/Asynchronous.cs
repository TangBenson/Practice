using System;
using System.Net;
using System.Threading.Tasks;

namespace AsyncAwait;
class Asynchronous
{
    internal static void Main(string[] args)
    {
        Console.WriteLine("程式開始");
        List<int> apiInput = new List<int> { 1, 2 };
        apiInput.Add(3);
        
        Task<bool> b = CheckInput(apiInput);
        Console.WriteLine("1結束");

        Asynchronous a = new Asynchronous();
        a.Inslog(apiInput);
        Console.WriteLine("2結束");

        Task testtask = Task.Run(CallSql);
        Console.WriteLine("3結束");

        Task gy = EatDrink(); //無回傳值，但若不這樣寫，vscode會顯示黃毛毛蟲，看了噁心
        Console.WriteLine("4結束");

        Console.ReadKey();
    }
    static async Task<bool> CheckInput(List<int> a)
    {
        Console.WriteLine("第1個method");
        await Task.Delay(1000);
        Console.WriteLine("第1個method done");
        return true;
    }
    bool Inslog(List<int> a)
    {
        Console.WriteLine("第2個method");
        Thread.Sleep(2000);
        Console.WriteLine("第2個method done");
        return true;
    }
    static List<int> CallSql()
    {
        Console.WriteLine("第3個method");
        Thread.Sleep(2000);
        Console.WriteLine("第3個method done");
        return new List<int> { 4, 5, 6 };
    }
    static async Task EatDrink()
    {
        Console.WriteLine("第4個method");
        await Task.Delay(2000);
        Console.WriteLine("第4個method done");
    }
}