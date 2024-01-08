using System;
using AdamJobSSAPI.Params;
using Microsoft.Extensions.Configuration;

namespace AdamJobSSAPI.Utilities
{
    public class ServerInfo 
    {        
        private readonly IConfiguration _config;        
        public ServerInfo(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string[] GetServerInfo(BaseParams param)
        {        
            string SERVER_NAME = _config.GetSection("AppSettings")["SERVER_NAME"].Trim();
            string DATABASE_NAME = _config.GetSection("AppSettings")["DATABASE_NAME"].Trim();
            string LOGIN = _config.GetSection("AppSettings")["LOGIN"].Trim();
            string PASSWORD = _config.GetSection("AppSettings")["PASSWORD"].Trim();
            string DATABASE_TYPE = _config.GetSection("AppSettings")["DATABASE_TYPE"].Trim();

            string[] rtnString=new String[6] { 
                param != null ? (param.SERVER_NAME ?? SERVER_NAME) : SERVER_NAME, 
                param != null ? param.DATABASE_NAME ?? DATABASE_NAME : DATABASE_NAME, 
                param != null ? param.LOGIN ?? LOGIN : LOGIN, 
                param != null ? param.PASSWORD ?? PASSWORD : PASSWORD, 
                param != null ? param.DATABASE_TYPE ?? DATABASE_TYPE : DATABASE_TYPE ,
                param != null ? (param.SSAPI_ADDRESS!=null?param.SSAPI_ADDRESS:""):""
            };
            return rtnString;
        }
    }
}
