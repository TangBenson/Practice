namespace GisCommonService.Dtos
{
    public class ResultDTO<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Result { get; set; }
        public string RtnCode { get; set; }
    }
}
