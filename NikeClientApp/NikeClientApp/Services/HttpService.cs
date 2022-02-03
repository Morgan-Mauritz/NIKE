using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;
using NikeClientApp.Models;

namespace NikeClientApp.Services
{
    public class HttpService<T> where T : class
    {
        private readonly RestSharp.RestClient _restClient; 

        public HttpService()
        {
            _restClient = new RestClient("https://localhost:44393/");
        }
        
        public async Task<Response<T>> Post(string endPoint, T obj)
        {
            var request = new RestRequest(endPoint);
            request.Method = Method.Post;
            request.AddBody(obj);
            request.AddHeader("apiKey", "b987c270-e582-4664-bea3-36f47f17dc43");
            return await _restClient.PostAsync<Response<T>>(request);
        }
        
        public async Task<PaginationResponse<List<T>>> GetList(string endPoint, string query)
        {
            var request = new RestRequest(endPoint + query);
            request.Method = Method.Get;
            request.AddHeader("apiKey", "b987c270-e582-4664-bea3-36f47f17dc43");
            return await _restClient.GetAsync<PaginationResponse<List<T>>>(request);
        }

        public async Task<Response<T>> Get(string endPoint, string query)
        {
            var request = new RestRequest(endPoint + query);
            request.Method = Method.Get;
            request.AddHeader("apiKey", "b987c270-e582-4664-bea3-36f47f17dc43");
            return await _restClient.GetAsync<Response<T>>(request);
        }

        public async Task<Response<T>> Update(string endPoint, T obj)
        {
            var request = new RestRequest(endPoint);
            request.Method = Method.Put;
            request.AddBody(obj);
            request.AddHeader("apiKey", UserApi.ApiKey);
            return await _restClient.PutAsync<Response<T>>(request);
        }

        public async Task<Response<T>> Delete(string endPoint)
        {
            var request = new RestRequest(endPoint);
            request.Method = Method.Delete;
            request.AddHeader("apiKey", UserApi.ApiKey);
            return await _restClient.DeleteAsync<Response<T>>(request);
        }
    }
}