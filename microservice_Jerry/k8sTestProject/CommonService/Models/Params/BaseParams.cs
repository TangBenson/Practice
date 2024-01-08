namespace CommonService.Params
{
    //ssapi固定吃這6個參數
    public class BaseParams
    {
        public string SERVER_NAME { get; set; }
        public string DATABASE_NAME { get; set; }
        public string LOGIN { get; set; }
        public string PASSWORD { get; set; }
        public string DATABASE_TYPE { get; set; }
        public string SSAPI_ADDRESS { get; set; }
    }
}
