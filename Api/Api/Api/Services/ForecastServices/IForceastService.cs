using System.Threading.Tasks;
using static Api.Model.Forecast;

namespace Api.Services.ForecastServices
{
    public interface IForceastService
    {
        public Task<ForecastDto> GetForecast(double longitude, double latitude);
    }
}
