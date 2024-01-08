
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Xml;
using CommonService.Dtos;
using CommonService.Params;
using CommonService.Utilities.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//串接pds的服務，交通部公開平台撈取經緯度地址轉換的服務
namespace CommonService.Utilities
{
    public class OpenDataService : IOpenDataService
    {
        public ResultDTO<JObject> GetOpenData(OpenDataParam param)
        {
            try
            {
                HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(BaseAddress);

                // Add an Accept header for JSON format.
                if (param.Format.ToLower() == "json")
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                else
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                }
                HttpResponseMessage resp;
                switch (param.Method == null ? "" : param.Method.ToLower())
                {
                    case "post":
                        resp = client.PostAsJsonAsync(param.Url, JsonConvert.DeserializeObject<Object>(param.PostData)).Result;
                        break;
                    case "get":
                        resp = client.GetAsync(param.Url).Result;
                        break;
                    default:
                        resp = client.GetAsync(param.Url).Result;
                        break;
                }

                Stream respons = resp.Content.ReadAsStreamAsync().Result;
                StreamReader sr = new StreamReader(respons, Encoding.GetEncoding(param.Encoding));
                string response = sr.ReadToEnd();
                JObject reader;
                if (param.Format.ToLower() == "xml")
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(response);
                    reader = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeXmlNode(xml.LastChild));
                }
                else
                {
                    reader = new JObject(
                        new JProperty("Records",
                            new JArray(
                                JsonConvert.DeserializeObject<JArray>(response)
                                )
                            )
                    );
                }
                return (new ResultDTO<JObject>
                {
                    Result = true,
                    Message = "",
                    Data = reader
                });

            }
            catch (Exception e)
            {
                return (new ResultDTO<JObject>
                {
                    Result = false,
                    Message = e.Message,
                    Data = null
                });
            }
        }
    }
}