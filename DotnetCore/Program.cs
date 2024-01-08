using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotnetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); //建立及設定 WebHost
        }

        //把Web Server 設置為 Kestral
        //IWebHostBuilder 是為了相容舊版 .Net Core 2.X ,⾮建議使⽤
        //ConfigureWebHostDefaults() 來建⽴⼀個 Kestrel 為預設Web 伺服器,若是⾮ Web 的服務,則應該改⽤ ConfigureServices() 來建⽴
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
