using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using GisCommonService.Dtos;
using GisTransService.Models.Params;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GisTransService.Services
{
    public class Map8DirectionService : IMap8DirectionService
    {
        private readonly IConfiguration _config;

        // private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Map8DirectionService(IConfiguration config)
        {
            _config = config;
        }
        public ResultDTO<JObject> getMap8DirectionData(Map8DirectionParam param)
        {
            try
            {
                if (_config["Map8ApiKey"] == null || _config["Map8ApiKey"] == "")
                {
                    throw new Exception("參數錯誤：Map8ApiKey 尚未設定！");
                }
                if (_config["Map8DirectionBaseUrl"] == null || _config["Map8DirectionBaseUrl"] == "")
                {
                    throw new Exception("參數錯誤：Map8DirectionBaseUrl 尚未設定！");
                }
                if (param.origin == null || param.origin == "")
                {
                    throw new Exception("參數錯誤：出發地參數 origin 不可以為空白！");
                }
                if (param.destination == null || param.destination == "")
                {
                    throw new Exception("參數錯誤：目的地參數 destination 不可以為空白！");
                }

                Map8GeoCodeParam startParam = new Map8GeoCodeParam();
                Map8GeoCodeParam endParam = new Map8GeoCodeParam();
                startParam.address = param.origin;
                ResultDTO<string> startResult = getMap8GeoCodeString(startParam);
                if (startResult.Result)
                {
                    param.origin = startResult.Data;
                }
                else
                {
                    throw new Exception("起始地址無法取得經緯度資料！" + startResult.Message);
                }

                endParam.address = param.destination;
                ResultDTO<string> endResult = getMap8GeoCodeString(endParam);
                if (endResult.Result)
                {
                    param.destination = endResult.Data;
                }
                else
                {
                    throw new Exception("目的地址無法取得經緯度資料！" + endResult.Message);
                }

                if (param.waypoints != "")
                {
                    string[] waypointsArray = param.waypoints.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    param.waypoints = "";
                    for (int i = 0; i < waypointsArray.Length; i++)
                    {
                        Map8GeoCodeParam addPointParam = new Map8GeoCodeParam();
                        addPointParam.address = waypointsArray[i];
                        ResultDTO<string> addPointResult = getMap8GeoCodeString(addPointParam);
                        if (addPointResult.Result)
                        {
                            param.waypoints += addPointResult.Data + ";";
                        }
                        else
                        {
                            throw new Exception("加點地址無法取得經緯度資料！" + addPointResult.Message);
                        }
                    }
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_config["Map8DirectionBaseUrl"].Trim());

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string directionUrl = "";

                directionUrl += "car/";
                directionUrl += param.origin + ";";
                if (param.waypoints != "")
                {
                    directionUrl += param.waypoints;
                }
                directionUrl += param.destination;
                directionUrl += ".json?key=" + (param.key == null ? _config["Map8ApiKey"].Trim() : param.key);

                HttpResponseMessage resp = client.GetAsync(directionUrl).Result;

                Stream respons = resp.Content.ReadAsStreamAsync().Result;
                StreamReader sr = new StreamReader(respons);
                string response = sr.ReadToEnd();

                JObject reader = JsonConvert.DeserializeObject<JObject>(response);
                //logger.Debug(response);

                return (new ResultDTO<JObject>
                {
                    Message = "",
                    Data = reader,
                    Result = resp.StatusCode == HttpStatusCode.OK ? true : false
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("getMap8Direction:" + ex.Message.Trim() + ";param:" + JsonConvert.SerializeObject(param));
                return (new ResultDTO<JObject>
                {
                    Message = "getMap8Direction:" + ex.Message,
                    Data = { },
                    Result = false
                });
            }
        }

        private ResultDTO<string> getMap8GeoCodeString(Map8GeoCodeParam param)
        {
            try
            {
                ResultDTO<JObject> result = new ResultDTO<JObject>();
                result = getMap8GeoCodeData(param);

                string returnString = "";

                if (result.Data["status"].ToString().ToUpper().Trim() == "OK")
                {
                    JToken location = result.Data["results"][0]["geometry"]["location"];
                    returnString = location["lng"].ToString().Trim() + "," +
                                   location["lat"].ToString().Trim();
                }

                return (new ResultDTO<string>
                {
                    Message = "getMap8GeoCodeString:" + result.Data["status"].ToString().ToUpper().Trim() == "OK" ? "" : (result.Data["error_message"] != null ? result.Data["error_message"].ToString() : ""),
                    Data = returnString,
                    Result = result.Data["status"].ToString().ToUpper().Trim() == "OK" ? true : false
                });
            }
            catch (Exception ex)
            {
                return (new ResultDTO<string>
                {
                    Message = "getGeoCodeString:" + ex.Message,
                    Data = "",
                    Result = false
                });
            }
        }

        public ResultDTO<JObject> getMap8GeoCodeData(Map8GeoCodeParam param)
        {
            try
            {
                if (_config["Map8ApiKey"] == null || _config["Map8ApiKey"] == "")
                {
                    throw new Exception("參數錯誤：Map8ApiKey 尚未設定！");
                }
                if (_config["Map8GeocodingBaseUrl"] == null || _config["Map8GeocodingBaseUrl"] == "")
                {
                    throw new Exception("參數錯誤：Map8GeocodingBaseUrl 尚未設定！");
                }
                //if (param.address == null || param.address == "")
                //{
                //    throw new Exception("參數錯誤：街道地址 address 不可以為空白！");
                //}

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_config["Map8GeocodingBaseUrl"].Trim());
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["Map8ApiKey"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string directionUrl = "json?key=" + (param.key == null ? _config["Map8ApiKey"].Trim() : param.key);

                if (param.address != null) directionUrl += "&address=" + HttpUtility.UrlEncode(param.address);
                if (param.latlng != null) directionUrl += "&latlng=" + param.latlng;
                if (param.postcode) directionUrl += "&postcode=" + param.postcode.ToString();
                if (param.formatted_address_embed_postcode) directionUrl += "&formatted_address_embed_postcode=" + param.formatted_address_embed_postcode.ToString();

                HttpResponseMessage resp = client.GetAsync(directionUrl).Result;

                Stream respons = resp.Content.ReadAsStreamAsync().Result;
                StreamReader sr = new StreamReader(respons);
                string response = sr.ReadToEnd();

                JObject reader = JsonConvert.DeserializeObject<JObject>(response);
                //logger.Debug(response);

                return (new ResultDTO<JObject>
                {
                    Message = "",
                    Data = reader,
                    Result = reader["status"].ToString().ToUpper().Trim() == "OK" ? true : false
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("getMap8GeoCodeData:" + ex.Message.Trim() + ";param:" + JsonConvert.SerializeObject(param));
                return (new ResultDTO<JObject>
                {
                    Message = "getMap8GeoCodeData:" + ex.Message,
                    Data = { },
                    Result = false
                });
            }
        }
    }
}
