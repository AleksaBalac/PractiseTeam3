using System;

namespace Core
{
    public class ResponseObject<T>
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; }

        public string Total { get; set; }

        public T Data { get; set; }

        public StatusCode StatusCode { get; set; }
    }

    public enum StatusCode
    {
        Ok = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        NotFound = 404,
        Unauthorized = 401
    }
}
