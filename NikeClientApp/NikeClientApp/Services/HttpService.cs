using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace NikeClientApp.Services
{
    public class HttpService<T,K>
    {
        private readonly RestSharp.RestClient _restClient; 

        public HttpService()
        {
            _restClient = new RestClient("https://localhost:44393/");
        }
        
        public async Task<K> Post(T obj, string endPoint)
        {
            var request = new RestRequest(endPoint);
            request.Method = Method.Post;
            request.AddBody(obj);
            return await _restClient.PostAsync<K>(request);
        }      
    }
}
