using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Api.Model.MappingProfiles
{
    public class POIMapping : Profile
    {
        public POIMapping()
        {
            //TODO add country name 
            CreateMap<POI, POIDto>().ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AvgRating, opt =>
                {
                    opt.MapFrom(src =>
                        src.Entries.Count() != 0 ? src.Entries.Sum(x => x.Rating) / src.Entries.Count() : null);
                });
        }
    }
}
