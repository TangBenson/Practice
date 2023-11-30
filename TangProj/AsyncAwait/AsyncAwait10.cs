using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class AsyncAwait10
    {
        internal static async Task Main(string[] args)
        {
            List<Task> tasks = new List<Task>();
            
            for (int i = 0; i < 100; i++)
            {
                int localI = i; // 建立區域變數
                Console.WriteLine(i);

                // 不是我要的效果
                await Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    Console.WriteLine(localI + 1000);
                });

                // 使用 Task.Run 包裝非同步執行
                // Task task = Task.Run(async () =>
                // {
                //     await Task.Delay(1000);
                //     Console.WriteLine(localI + 1000);
                // });
                // tasks.Add(task);
            }
            // 等待所有任務完成
            await Task.WhenAll(tasks);
        }
    }
}