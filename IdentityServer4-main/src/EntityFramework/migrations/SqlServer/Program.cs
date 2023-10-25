using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SqlServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args); //构建 ASP.NET Core Web 主机（IWebHost）
            SeedData.EnsureSeedData(host.Services);
        }

        //构建 ASP.NET Core Web 主机
        //IWebHost是.net core 2.x用的，3.0開始引入更完善的IHostBuilder
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args) //使用 ASP.NET Core 的默认主机构建器创建一个 Web 主机。默认构建器将自动配置许多常见的 Web 主机设置。
                .UseStartup<Startup>() //指定了应用程序的启动类为 Startup
                .Build(); //构建 Web 主机
    }
}
