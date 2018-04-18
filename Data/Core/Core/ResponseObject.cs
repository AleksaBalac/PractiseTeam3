namespace Core
{
    public class ResponseObject<T>
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; }

        public string Total { get; set; }

        public T Data { get; set; }
    }
}
