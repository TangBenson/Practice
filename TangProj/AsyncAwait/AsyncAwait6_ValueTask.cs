using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Caching.Memory;

// ValueTask用於對內存極為講究的時刻，平常不太需要使用，所以我他媽的花一堆時間寫這支.......辛酸
// ValueTask可以判斷當資料在內存時就不去非同步呼叫，不然宣告 async Task都會占用內存
// 執行這支要用dotnet build -c Release 和 dotnet run --configuration Release

namespace AsyncAwait
{
    public class AsyncAwait6_ValueTask
    {
        internal static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchMarkService>();
        }
    }

    [MemoryDiagnoser] // BenchmarkDotNet的玩意，可以获取关于性能基准测试期间内存使用情况的详细信息
    public class BenchMarkService
    {
        private readonly IEnumerable<string> webSites = new string[]{
            "https://kknews.cc/zh-tw/tech/lzbv669.html",
            "https://ithelp.ithome.com.tw/articles/10248883?sc=iThelpR",
            "https://blog.csdn.net/qq_39312554/article/details/126302880"
        };

        [Benchmark]
        public async Task LoadDataTask()
        {
            DownloadService downloadService = new DownloadService();

            for (int i = 0; i < 100000; i++)
            {
                foreach (var webSite in webSites)
                {
                    await downloadService.DownloadDataTask(webSite);
                }
            }
        }

        [Benchmark]
        public async Task LoadDataValueTask()
        {
            DownloadService downloadService = new DownloadService();

            for (int i = 0; i < 100000; i++)
            {
                foreach (var webSite in webSites)
                {
                    await downloadService.DownloadDataValueTask(webSite);
                }
            }
        }
    }

    public class DownloadService
    {
        private readonly MemoryCache cache;
        private readonly HttpClient httpClient;
        private readonly MemoryCacheEntryOptions cacheEntryOptions;

        public DownloadService()
        {
            this.cache = new MemoryCache(new MemoryCacheOptions());
            this.httpClient = new HttpClient();
            this.cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromDays(1) // 设置滑动过期时间为1天
            };
        }

        public async Task<object> DownloadDataTask(string webSite)
        {
            if (cache.TryGetValue(webSite, out var response))
            {
                return response;
            }

            var responce = await httpClient.GetAsync(webSite);
            cache.Set(webSite, responce, cacheEntryOptions);

            return responce;
        }

        public async ValueTask<object> DownloadDataValueTask(string webSite)
        {
            if (cache.TryGetValue(webSite, out var response))
            {
                return cache.Get(webSite);
            }

            var responce = await httpClient.GetAsync(webSite);
            cache.Set(webSite, responce, cacheEntryOptions);

            return responce;
        }
    }
}