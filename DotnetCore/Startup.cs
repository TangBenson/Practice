using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCore.Service;
using DotnetCore.Service.BonusQuery;
using DotnetCore.Service.ExecService;
using DotnetCore.Service.TestDIService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
/*
以串接人家的 API 來說，可以看到就是簡單的四個步驟：

製作 Model
撰寫 Service
注冊 Service (在startup.cs)
呼叫 Service (在controller)
*/
namespace DotnetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // DI的部份，⼀般來說沒有順序性
        // 將應用程式所需的「服務」註冊到 DI 容器中。此方法只會在應用程式啟動時執行一次(This method gets called by the runtime. Use this method to add services to the container.)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // 建.net5專案才會預設加入，.net3.1就沒有
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetCore", Version = "v1" });
            // });

            //我：註冊 HttpClient 服務，它才會把 HttpClient 執行個體給注入到 MaskService 裡，且TangApiController也能用
            services.AddHttpClient<MaskService>();
            //services.AddHttpClient(); //這樣寫MaskService就不能用httpclient，但TangApiController依然可以

            //我：BonusQuery API用
            services.AddHttpContextAccessor();
            services.AddScoped<IBonusQuery, BonusQuery>(); //AddScoped可以很多個

            //我：ExecService API用
            // services.AddHttpContextAccessor();
            services.AddScoped<IExecService, ExecService>();

            //我
            services.AddScoped<TestDIService>();

            //我:DI注入方式有下面三種，學自凱歌寫程式9_4集
            services.AddSingleton<SingletonService>(); //從程式⼀開始到結束,完全都是同⼀個 => Sam：處理⼤家共⽤的資料且只能有⼀份的情形,例如:共⽤連線(HttpClient)
            services.AddScoped<ScopedService>();       //每個Request為同一個實例，黑大：整個 Request 期間共用一份 => Sam：注⼊物件很⼩,需要紀錄個別狀態時
            services.AddTransient<TransientService>(); //每次程式運行都注入一個實例，黑大：每次需要時就建一顆新的 => Sam：在無狀態/⻑時/⾼負荷運作的情形下，也是系統預設
        }

        // Middleware pipeline 的部份，有順序性
        // 設定 ASP.NET Core 如何回應用戶端要求，透過 IApplicationBuilder 來設定 Request 進出的Pipeline
        // IWebHostEnvironment: 繼承⾃ IHostEnvironment , 提供程式在運⾏環境下的⼀些相關資訊
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // 建.net5專案才會預設加入，.net3.1就沒有
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetCore v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // =>是c#的Lambda表達式，(input-parameters) => expression，箭頭後若有多行則用{}包起
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // app.Run不會再呼叫下⼀個 Middleware
            // app.Use()類似 app.Run(),但它可以呼叫下⼀個 Middleware
            // app.Map()可依據請求的網址來處理
        }
    }
}
