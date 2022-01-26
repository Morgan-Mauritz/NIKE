using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#nullable enable

namespace Api.Model
{
    public class Response<T> where T : new()
    {
        public Status Status { get; set; }
        public int? StatusCode  { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public T? Exception { get; set; }

        public Response(T? data = default)
        {
            Data = data;
            Status = Status.Success;
        }
        
        public Response(Status status, int statusCode, string message, T? exception = default)
        {
            Exception = exception;
            Status = status;
            StatusCode = statusCode;
            Message = message; 
        }
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        Fail,
        Error,
        Success
    }
}
