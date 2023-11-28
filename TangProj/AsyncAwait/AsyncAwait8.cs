using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class AsyncAwait8
    {

        internal static void Main(string[] args)
        {
            Console.WriteLine("開始");

            # region 情境一 等待呼叫Peggy而無法先執行Console.WriteLine("結束")
            // await Peggy();
            // Console.WriteLine("先做其它事");
            #endregion

            # region 情境二 改成不呆等Peggy，先往後執行，結束前再等待
            Task a = Peggy();
            Console.WriteLine("先做其它事");
            Task.WaitAll(a); // 也可寫成 await a;
            Console.WriteLine("結束");
            #endregion
        }

        private static async Task Peggy()
        {
            Console.WriteLine("寶貝刷牙");

            await Task.Delay(5000); // 使用异步方式实现延迟，它会在指定的延迟时间内释放当前线程，允许其他操作继续执行
            // Thread.Sleep(5000); // 使用传统的线程阻塞方式暂停执行，这会导致当前线程完全停止执行，不会释放线程资源，也不会允许其他操作在此期间进行
            Console.WriteLine("寶貝洗臉");

            await Task.Run(() =>
            {
                Console.WriteLine("寶貝抱抱");
            });
        }
    }
}