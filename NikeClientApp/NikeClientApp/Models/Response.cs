namespace NikeClientApp.Models
{
    public class Response<T> where T : class
    {
        public string Status { get; set; }
        public string ExceptionMessage { get; set; }
        public T Data { get; set; }

        public Response()
        {

        }
    }
    public class PaginationResponse<T> : Response<T> where T : class
    {
        public PaginationResponse()
        {

        }

        public int Offset { get; set; }
        public int Amount { get; set; }
        public string NextPage { get; set; }
        public string PrevPage { get; set; }
        public int Total { get; set; }
    }
}
