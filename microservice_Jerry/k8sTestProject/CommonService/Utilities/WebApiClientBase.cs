using System;
using System.IO;
using System.Web;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace CommonService.Utilities
{
    public class WebApiClientBase
    {        
        private static string Token { set; get; }
        private static string baseAddress { set; get; } = "";

        protected static JObject SpRetBase(Object result, string apiUrl,string ssapiBaseAddress)
        {
            HttpClient client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.BaseAddress = new Uri(ssapiBaseAddress);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string token = GetToken();
            // Send the request. 
            var jsonString = JsonConvert.SerializeObject(result);
            var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage resp = client.PostAsync(apiUrl + "?Token=" + HttpUtility.UrlEncode(token, System.Text.Encoding.UTF8) , content).Result;

            Stream respons = resp.Content.ReadAsStreamAsync().Result;
            StreamReader sr = new StreamReader(respons);
            string response = sr.ReadToEnd();

            JObject reader = JsonConvert.DeserializeObject<JObject>(response);

            return reader;
        }

        protected static string GetToken()
        {
            string token = "VfaU+LJXyYZp7Nr3mFhCQtBfZ/rL2AQmOjkOW4W1uZVumEKn0wIHcD/RsdkmgB8di2Y9HFgUS/7HFxHm4m9eACLvfBCTdBEGoGqcd6RDUeZNSwlOrVeFarS9bEalGyz6";

            return token;
        }
        
    }
}
