namespace DotnetCore.Models
{
    public class ResultDTO<T>
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ResultDTO
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
