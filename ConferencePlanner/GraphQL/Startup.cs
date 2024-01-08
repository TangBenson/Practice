using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
/*
以串接人家的 API 來說，可以看到就是簡單的四個步驟：

製作 Model
撰寫 Service
注冊 Service (在startup.cs)
呼叫 Service (在controller)
*/
namespace ConferencePlanner.GraphQL
{
    public class Startup
    {
        // public Startup(IConfiguration configuration)
        // {
        //     Configuration = configuration;
        // }

        public IConfiguration Configuration { get; }

        // DI的部份
        // 將應用程式所需的「服務」註冊到 DI 容器中。此方法只會在應用程式啟動時執行一次
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddControllers();

            /*
            前面宣告的ApplicationDbContext類別，在這裡透過AddDbContext來注入到服務容器中，
            當我們在接下來設計解析器時，它就可以自動注入到解析器中。解析器是GraphQL中用來對資料做存取的地方，
            Db Context的功能則是對資料的存取提供一系列的方法，因此將Db Context注入到解析器會是很有必要的！
            */
            // services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));
            // 修改寫法為DBContext的池(pooling)，池裡會有多個DBContext的物件，這樣才可以一次查多筆資料
            services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));
 

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();
        }

        // Middleware pipeline 的部份
        // 設定 ASP.NET Core 如何回應用戶端要求，透過 IApplicationBuilder 來設定 Request 進出的Pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            // =>是c#的Lambda表達式，(input-parameters) => expression，箭頭後若有多行則用{}包起
            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers();
                endpoints.MapGraphQL();
            });
        }
    }
}
