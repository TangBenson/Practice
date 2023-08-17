using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Text;

//這是主要的 WebSocket 處理邏輯
namespace WebSocketDemo.Models
{
    public class WebSocketHandler 
    {

        public WebSocketHandler(ILogger<WebSocketHandler> logger)
        {
            // this.logger = logger;
        }

        //REF: https://radu-matei.com/blog/aspnet-core-websockets-middleware/
        //先將 WebSocket 放入 ConcurrentDictionary<int, WebSocket> 方便對所有人廣播
        ConcurrentDictionary<int, WebSocket> WebSockets = new ConcurrentDictionary<int, WebSocket>(); //ConcurrentDictionary是C#中的一種多線程(執行續)字典類型

        // private readonly ILogger<WebSocketHandler> logger;

        //ProcessWebSocket 會接入 WebSocket 物件，每個 WebSocket 物件會對映一條已建立的連線
        public async Task ProcessWebSocket(WebSocket webSocket) 
        {
            WebSockets.TryAdd(webSocket.GetHashCode(), webSocket);
            var buffer = new byte[1024 * 4];
            var res = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var userName = "anonymous";

            //while 迴圈跑 ReceiveAsync() 等待客戶端傳來指令，使用 /USER 語法宣告該連線使用者名稱，其餘視為訊息對所有成員廣播。
            //WebSocketReceiveResult.CloseStatus.HasValue偵測連線中斷，中斷後將 WebSocket 移出 ConcurrentDictionary<int, WebSocket> 並對剩下成員廣播
            //廣播是用 Parallel.ForEach 平行執行呼叫 SendAsync() 傳送訊息
            while (!res.CloseStatus.HasValue) {
                var cmd = Encoding.UTF8.GetString(buffer, 0, res.Count);
                if (!string.IsNullOrEmpty(cmd)) {
                    // logger.LogInformation(cmd);
                    if (cmd.StartsWith("/USER ")) //使用 /USER 語法宣告該連線使用者名稱，其餘視為訊息對所有成員廣播
                        userName = cmd.Substring(6);
                    else
                        Broadcast($"{userName}:\t{cmd}");
                }
                res = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(res.CloseStatus.Value, res.CloseStatusDescription, CancellationToken.None);
            WebSockets.TryRemove(webSocket.GetHashCode(), out var removed);
            Broadcast($"{userName} left the room.");
        }
        
        public void Broadcast(string message) 
        {
            var buff = Encoding.UTF8.GetBytes($"{DateTime.Now:MM-dd HH:mm:ss}\t{message}");
            var data = new ArraySegment<byte>(buff, 0, buff.Length);

            //廣播是用 Parallel.ForEach 平行執行呼叫 SendAsync() 傳送訊息
            Parallel.ForEach(WebSockets.Values, async (webSocket) =>
            {
                if (webSocket.State == WebSocketState.Open)
                    await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            });
        }
    }
}