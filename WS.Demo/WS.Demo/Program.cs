using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WS.Demo.Controller;
using WebSocketSharp.Server;


namespace WS.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //每250秒加入一個廣播訊息至MessageQueue
            // new Thread表示建立自己的執行緒
            var msgCreater = new Thread(() =>
            {
                while (true)
                {
                    MessageQueueSingleton.Instance().AddMsg();
                    Thread.Sleep(250);
                }
            });
            msgCreater.Start();

            //WebSocketServer 監聽 PORT 55688
            var wssv = new WebSocketServer(55688);
            wssv.AddWebSocketService<BroadcastBehavior>("/Broadcast");
            wssv.Start();
            if (wssv.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", wssv.Port);
                foreach (var path in wssv.WebSocketServices.Paths)
                {
                    Console.WriteLine("- {0}", path);
                }
            }

            //WebSocketServer 停止行為
            Console.WriteLine("\nPress Enter key to stop the server...");
            Console.ReadLine();
            MessageQueueSingleton.Instance().FreeQueue(); //清除MessageQueue內所有資料
            msgCreater.Abort();// 停用廣播訊息產生執行緒
            wssv.Stop();

        }
    }
}
