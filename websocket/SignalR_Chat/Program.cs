using SignalR_Chat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddCors();

// builder.Services.AddCors(options =>
//     options.AddDefaultPolicy(builder => builder.WithOrigins(urls)
//     .AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// 加入同源政策支援(CORS)，要在 app.UseRouting之前
app.UseCors(builder =>
{
    builder.AllowAnyHeader().AllowAnyMethod()
    .SetIsOriginAllowed(_ => true).AllowCredentials();
});

app.UseRouting();

// .Net 6 可不用再寫 app.UseEndpoints，已經默認添加到 pipeline了
// app.UseEndpoints(endpoints =>
// {
app.UseAuthorization();
app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");
// });

app.Run();
