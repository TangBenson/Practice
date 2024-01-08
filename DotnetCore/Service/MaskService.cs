using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DotnetCore.Models;
/*
MaskService 會透過建構式注入 HttpClient 執行個體，
之後只是發出 GetAsync() 請求取回資料並與 MaskInfo 物件繫結
*/
namespace DotnetCore.Service
{
    public class MaskService
    {
        public HttpClient Client { get; }

        public MaskService(HttpClient client)
        {
            // 另一個資料來源。
            // 健康保險資料開放服務：https://data.nhi.gov.tw
            client.BaseAddress = new Uri("https://quality.data.gov.tw/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "QueryMask Sample");
            Client = client;
        }

        public async Task<IEnumerable<MaskInfo>> GetMaskInfo()
        {
            var response = await Client.GetAsync("dq_download_json.php?nid=116285&md5_url=2150b333756e64325bdbc4a5fd45fad1");
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<MaskInfo>>(responseStream);
        }
    }
}