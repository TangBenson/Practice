// ref : 黑暗執行緒
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 展示用途：訊息存於記憶體，不考慮程序異常資料遺失問題
var msgQueue = new ConcurrentQueue<string>();
// 延遲 5 秒執行，期間累積的訊息一次處理
var debouncePrint = new DebouncedJob(TimeSpan.FromSeconds(5));

app.MapPost("/alert", (HttpRequest request) =>
{
    string msg = request.Form["msg"].ToString();
    if (!string.IsNullOrEmpty(msg))
    {
        msgQueue.Enqueue(msg);

        // TODO: 若怕新訊息源源不絕一直 Delay 下去，可加入訊息數上限
        // 當 msgQueue 累積數量達上限時，不透過 DebouncedJob 直接執行

        debouncePrint.Run(() =>
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Debounce Print: {DateTime.Now:mm:ss}");
            Console.ResetColor();
            while (msgQueue.TryDequeue(out string m))
            {
                Console.WriteLine("  " + m);
            }
        });
    }
    return Results.Content("OK");
});

app.MapGet("/", () => Results.Content(@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>DebouncedJob</title>
</head>
<body>
    <form action=/alert method=post target=result id=frm >
        <input type=hidden name=msg id=msg />
    </form>
    <iframe name=result style=display:none></iframe>
    <button onclick='test()' >Run Test</button>
    <ul id=log></ul>
    <script>
    let delays = [3, 1, 2, 3, 1, 4, 6, 1, 1, 7, 1];
    function test() {
        send();
    }
    function send() {
        let m = `Sent on ${new Date().toISOString().split('T')[1].substr(3, 5)}`;
        document.getElementById('log').innerHTML += `<li>${m}</li>`;
        document.getElementById('msg').value = m;
        document.getElementById('frm').submit();
        if (delays.length) {
            setTimeout(send, delays.shift() * 1000);
        }
    }
    </script>
</body>
</html>", "text/html"));

app.Run();


public class DebouncedJob
{
    private CancellationTokenSource _cts = new();
    private readonly object _lock = new();
    private readonly TimeSpan _delay;

    public DebouncedJob(TimeSpan delay)
    {
        _delay = delay;
    }

    public void Run(Action action)
    {
        Console.WriteLine(0);
        lock (_lock)
        {
            Console.WriteLine(1);
            // 取消上一次的執行
            // 概念上類似 JavaScript debounce 的 clearTimeout() 技巧
            _cts.Cancel();
            _cts.Dispose();
        }

        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        Task.Delay(_delay, token).ContinueWith(task =>
        {
            Console.WriteLine(2);
            // 執行到這裡有兩種情況：
            // 1. 延遲時間到
            // 2. 延遲時間未到，CancellationToken 被取消
            // 後者不執行 action
            if (!token.IsCancellationRequested)
            {
                action();
            }
        });
    }
}