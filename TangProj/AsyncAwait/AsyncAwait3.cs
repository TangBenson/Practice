using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait;
class Agg
{
    public async void M1()
    {
        var foo = DateTime.UtcNow.ToString("mm:ss.ffff");
        Console.WriteLine($"M1 開始時間 : {foo}");
        await Task.Delay(3000);
        foo = DateTime.UtcNow.ToString("mm:ss.ffff");
        Console.WriteLine($"M1 結束時間 : {foo}");
    }
    public async Task M2()
    {
        var foo = DateTime.UtcNow.ToString("mm:ss.ffff");
        Console.WriteLine($"M2 開始時間 : {foo}");
        await Task.Delay(3000);
        foo = DateTime.UtcNow.ToString("mm:ss.ffff");
        Console.WriteLine($"M2 結束時間 : {foo}");
    }
}
class AsyncAwait3
{
    internal static void Main(string[] args)
    {
        Agg objA = new Agg();

        objA.M1();
        Console.WriteLine($"Press any key to Exist...{Environment.NewLine}");
        Console.ReadKey();
    }
}
// 當呼叫 M1 方法之後，接著， M1 方法似乎就以另外一個執行緒的方式進行執行，所以， Main 這個方法內的程式碼，
// 沒有等到 M1 執行完畢，就直接執行 objA.M1() 之後的所有敘述，而當三秒鐘之後，就會顯示 M1 方法已經結束的時間。
// 這是因為我們使用了 async void 的方式來宣告這個方法，因此，我們沒有任何方法，可以等候 M1 方法執行完成之後，才要繼續執行 objA.M1() 之後的所有敘述。
