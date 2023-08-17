using System.Net.WebSockets;
using WebSocketDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//加入WebSocket處理服務
builder.Services.AddSingleton<WebSocketHandler>();

// 建立 WebApplication 物件，透過 app 設定 Middlewares (HTTP request pipeline)
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) //检查当前环境是否为开发环境，若不是則執行以下區塊
{
    app.UseExceptionHandler("/Error"); //启用异常处理程序，以将所有未处理的异常重定向到“/Error”路径
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); //启用HTTP Strict Transport Security (HSTS)
}

//加入 WebSocket 功能
app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(30) //设置WebSockets的保持连接间隔30秒。TimeSpan表示一个时间间隔
});

app.UseHttpsRedirection();//启用HTTPS重定向
app.UseStaticFiles();//启用静态文件处理，以便应用程序能够提供静态文件（如CSS，JavaScript，图像等）

//覺得用 Controller 接收 WebSocket 太複雜，黑大改在 Middleware 層處理
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws") //当请求的路径为"/ws"时
    {
        if (context.WebSockets.IsWebSocketRequest) //检查请求是否是WebSockets请求
        {
            using (WebSocket ws = await context.WebSockets.AcceptWebSocketAsync())//使用AcceptWebSocketAsync方法接受WebSockets请求
            {
                var wsHandler = context.RequestServices.GetRequiredService<WebSocketHandler>();//使用依赖注入机制获取WebSocketHandler类型的实例
                await wsHandler.ProcessWebSocket(ws);//使用获取的实例调用ProcessWebSocket方法处理WebSockets请求
            }
        }
        else 
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
    }
    else await next();
});

app.UseRouting();

app.UseAuthorization();


app.MapRazorPages();



app.Run();
