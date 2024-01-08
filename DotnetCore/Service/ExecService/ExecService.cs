using DotnetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCore.Service.ExecService
{
    public class ExecService : IExecService
    {
        //private static string connetStr = "";
        private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private static string iRentApiUrl = "";

        public ExecService(IConfiguration configuration) //IHttpContextAccessor httpContextAccessor
        {
            _configuration = configuration;
            //_httpContextAccessor = httpContextAccessor;
            //connetStr = _configuration["IRent"];
            iRentApiUrl = _configuration["iRentApiUrl"];
        }

        public ResultDTO<loveCodeDTO> loveCodeList(loveCode_Params param)
        {
            var resultDTO = new ResultDTO<loveCodeDTO>();
            var lrent_ResultDTO = new lrent_ResultDTO<loveCodeDTO>();
            resultDTO.Result = false;
            resultDTO.Message = "";

            try
            {
                string strJson = "";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = HttpWebRequest.Create(iRentApiUrl + "api/LoveCodeList") as HttpWebRequest;

                request.Method = "POST";
                //Authorization
                //request.Headers.Add("Authorization", common.HMACSHA256(JsonConvert.SerializeObject(param)));
                request.ContentType = "application/json; charset=UTF-8";


                using (StreamWriter requestWriter = new StreamWriter(request.GetRequestStream()))
                {
                    requestWriter.Write(JsonConvert.SerializeObject(param));
                    requestWriter.Flush();
                    requestWriter.Close();
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            strJson = myStreamReader.ReadToEnd();
                            //拆解JSON
                            lrent_ResultDTO = JsonConvert.DeserializeObject<lrent_ResultDTO<loveCodeDTO>>(strJson);
                        }
                    }
                }

                resultDTO.Result = true;
                resultDTO.Message = "";
                resultDTO.Data = lrent_ResultDTO.Data;
            }
            catch (Exception ex)
            {
                resultDTO.Result = false;
                resultDTO.Message = ex.Message; //回傳訊息
            }

            return resultDTO;
            //throw new NotImplementedException();
        }
    }
}
