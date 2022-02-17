using AutoMapper;
using System;
using System.Linq;
using static Api.Model.Forecast;

namespace Api.Model.MappingProfiles
{
    public class WeatherResultMapping : Profile
    {
        public WeatherResultMapping()
        {
            CreateMap<WeatherResult, WeatherResultDto>().ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => DateTime.Parse(src.Dt_txt)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Weather.FirstOrDefault().Description))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Weather.FirstOrDefault().Icon))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.Wind.Speed));
        }
    }
}
