using AutoMapper;
using static Api.Model.Forecast;

namespace Api.Model.MappingProfiles
{
    public class ForecastMapping : Profile
    {
        public ForecastMapping()
        {
            CreateMap<Forecast, ForecastDto>().ForMember(x => x.City, opt => opt.MapFrom(c => c.City.Name))
                .ForMember(src => src.Country, opt => opt.MapFrom(c => c.City.Country))
                .ForMember(src => src.WeatherList, opt => opt.MapFrom(c => c.List));
        }
    }
}
