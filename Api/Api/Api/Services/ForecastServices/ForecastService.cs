using Api.Model;
using System;
using System.Threading.Tasks;
using RestSharp;
using AutoMapper;
using static Api.Model.Forecast;

namespace Api.Services.ForecastServices
{
    public class ForecastService : IForceastService
    {
        private readonly IMapper _mapper;
        private readonly RestClient client;
        private string query;
        public ForecastService(IMapper mapper)
        {
            client = new RestClient("http://api.openweathermap.org/data/2.5/forecast");
            client.AddDefaultHeader("Content-Type", "application/json");
            query = "?appid=abe847e305174899f79c5f3544d18d28&lang=SV";
            _mapper = mapper;
        }
        public async Task<ForecastDto> GetForecast(double longitude, double latitude)
        {
            try
            {
                query += $"&lat={latitude}&lon={longitude}&units=metric";
                var request = new RestRequest(query);
                var forecastResult = await client.GetAsync<Forecast>(request);
                return _mapper.Map<ForecastDto>(forecastResult);
            }
            catch(Exception ex) 
            {
                return null; //vet ej vad detta blir.
            }
        }
    }
}
