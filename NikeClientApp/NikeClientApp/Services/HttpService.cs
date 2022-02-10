using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;
using NikeClientApp.Models;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace NikeClientApp.Services
{
    public class HttpService<T> where T : class
    {
        private readonly RestClient _restClient;

        public HttpService()
        {
            _restClient = new RestClient("https://localhost:44393/");
        }
        
        public async Task<Response<T>> Post(string endPoint, T obj = default, bool useApiKey = true)
        {
            var request = new RestRequest(endPoint);
            request.Method = Method.Post;
            if (obj != null)
            {
                request.AddBody(obj);
            }
            if (useApiKey)request.AddHeader("apiKey", UserApi.ApiKey); 
            return await _restClient.PostAsync<Response<T>>(request);
        }

        public async Task<PaginationResponse<ObservableCollection<T>>> GetList(string endPoint, string query)
        {
            var request = new RestRequest(endPoint + query);
            request.Method = Method.Get;
            return await _restClient.GetAsync<PaginationResponse<ObservableCollection<T>>>(request);
        }

        public async Task<Response<T>> Get(string endPoint, string query)
        {
            var request = new RestRequest(endPoint + query);
            request.Method = Method.Get;
            request.AddHeader("apiKey", UserApi.ApiKey);
            return await _restClient.GetAsync<Response<T>>(request);
        }

        public async Task<Response<T>> Update(string endPoint, T obj)
        {
            var request = new RestRequest(endPoint);
            request.Method = Method.Put;
            request.AddBody(obj);
            request.AddHeader("apiKey", UserApi.ApiKey);

            var result = await _restClient.PutAsync(request);


            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Du har ej tillgång till denna åtgärd");
            }


            return result as Response<T>;

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