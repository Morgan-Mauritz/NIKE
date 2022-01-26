using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#nullable enable

namespace Api.Model
{
    public class Response<T> where T : new()
    {
        public Status Status { get; set; }
        public string? ExceptionMessage { get; set; }
        public T? Data { get; set; }

        public Response(T? data = default)
        {
            Data = data;
            Status = Status.Success;
        }

        public Response(Status status, string message, T? data = default)
        {
            Data = data;
            Status = status;
            ExceptionMessage = message;
        }
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        Success,
        Fail,
        Error,
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
            Status = Status.Success;
            Offset = offset;
            Amount = amount;
            NextPage = nextPage;
            PrevPage = prevPage;
            Total = total;
        }


    }


}
