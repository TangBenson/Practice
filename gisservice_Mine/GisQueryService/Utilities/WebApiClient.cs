using System;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace GisCommonService.Utilities
{
    public class WebApiClient : WebApiClientBase
    {
        private readonly IConfiguration _config;
        public WebApiClient(IConfiguration configuration)
        {
            _config = configuration;
        }
        static readonly string SPRETB_API = "API/SPRetB";
        readonly string SPEXEBATCHMULTIARR2_API = "API/SPExeBatchMultiArr2";

        public DataSet SPRetB(string[] serverName, string spName, object[] parms, ref string message, ref string messageLevel, ref string messageType)
        {
            string ssapiBaseUrl = _config.GetSection("AppSettings")["SSAPI"].Trim();
            try
            {
                var result = new
                {
                    SPNM = spName,
                    SVRNM = serverName,
                    PARMS = parms,
                    Token = GetToken()
                };

                JObject reader = new JObject();
                //20180614 add by William 增加SSAPI參數
                if (serverName.Length == 6 && serverName[5] != "")
                {
                    reader = SpRetBase(result, SPRETB_API, serverName[5]);
                }
                else
                {
                    reader = SpRetBase(result, SPRETB_API, ssapiBaseUrl);
                }

                DataSet ds = JsonConvert.DeserializeObject<DataSet>(reader["DATA"].ToString());
                message = reader["MESSAGE"].ToString();
                messageLevel = reader["MESSAGELEVEL"].ToString();
                messageType = reader["MESSAGETYPE"].ToString();
                return ds;
            }
            catch (Exception ex)
            {
                message = "WebApiClient error:" + ex.Message;
                messageLevel = "1";
                messageType = "";
                return new DataSet();
            }
        }

        public DataSet SPExeBatchMultiArr2(string[] serverName, string spName, object[][] parms, bool isTransaction, ref string message, ref string messageLevel, ref string messageType)
        {
            string ssapiBaseUrl = _config.GetSection("AppSettings")["SSAPI"].Trim();
            try
            {
                var result = new
                {
                    SPNM = spName,
                    SVRNM = serverName,
                    PARMS = parms,
                    Token = "",
                    IsTransaction = isTransaction
                };

                JObject reader = new JObject();
                if (serverName.Length == 6 && serverName[5] != "")
                {
                    reader = SpRetBase(result, SPEXEBATCHMULTIARR2_API, serverName[5]);
                }
                else
                {
                    reader = SpRetBase(result, SPEXEBATCHMULTIARR2_API, ssapiBaseUrl);
                }

                DataSet ds = JsonConvert.DeserializeObject<DataSet>(reader["DATA"].ToString());
                message = reader["MESSAGE"].ToString();
                messageLevel = reader["MESSAGELEVEL"].ToString();
                messageType = reader["MESSAGETYPE"].ToString();
                return ds;
            }
            catch (Exception ex)
            {
                message = "WebApiClient error:" + ex.Message;
                messageLevel = "1";
                messageType = "";

                return new DataSet();
            }

        }
    }
}
