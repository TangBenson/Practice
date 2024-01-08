namespace DotnetCore.Models
{
    public class lrent_ResultDTO<T>
    {
        public string Result { get; set; }
        public string ErrorCode { get; set; }
        public string NeedRelogin { get; set; }
        public string NeedUpgrade { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
