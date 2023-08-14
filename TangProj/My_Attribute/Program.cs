var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 建立 WebApplication 物件，透過 app 設定 Middlewares (HTTP request pipeline)
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!"); // 允許在特定的路徑上處理 GET 請求，並定義要執行的處理程序

// app.UseEndpoints是用於設置端點的方法。它是 .NET 6 中的新功能，取代了之前版本中的 app.UseMvc 方法
// app.UseEndpoints可以設置多個端點，例如 MapGet、MapPost、MapPut 等，並定義它們的處理程序
// UseRouting要在 UseEndpoints之前
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", () => "Hello World!");
});

// 設置 MVC 控制器路由的方法
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// app.Run不會再呼叫下⼀個 Middleware
// app.Use()類似 app.Run(),但它可以呼叫下⼀個 Middleware
// app.Map()可依據請求的網址來處理
