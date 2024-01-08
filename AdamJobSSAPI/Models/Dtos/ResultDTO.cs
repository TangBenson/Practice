namespace AdamJobSSAPI.Dtos
{
    public class ResultDTO<T>
    {
        public string Message { get; set; }
        public T Data { get; set; } //T泛型，指定任何型態，DataTable、DataSet、陣列.....隨妳爽
        public bool Result { get; set; }
        public string RtnCode { get; set; }
    }
}
