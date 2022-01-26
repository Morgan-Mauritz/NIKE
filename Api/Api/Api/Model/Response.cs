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

    public class PaginationResponse<T> : Response<T> where T : new()
    {
        public int Offset { get; set; }
        public int Amount { get; set; }
        public string NextPage { get; set; }
        public string PrevPage { get; set; }
        public int Total { get; set; }


        public PaginationResponse(T? data, int offset, int amount, string nextPage, string prevPage, int total) : base(data)
        {
            Offset = offset;
            Amount = amount;
            NextPage = nextPage;
            PrevPage = prevPage;
            Total = total;
        }


    }


}
