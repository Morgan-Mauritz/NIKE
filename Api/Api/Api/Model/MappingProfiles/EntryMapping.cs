using AutoMapper;

namespace Api.Model.MappingProfiles
{
    public class EntryMapping : Profile
    {
        public EntryMapping()
        {
            CreateMap<Entry, EntryDto>().ForMember(c => c.POIString, opt => opt.MapFrom(src => src.POI.Name))
                .ForMember(c => c.Username, opt => opt.MapFrom(src => src.User.Username));
            CreateMap<AddEntry, Entry>().ForMember(c => c.POI, opt => opt.Ignore());
            CreateMap<UpdateEntry, Entry>().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcmember) => srcmember != null));

            CreateMap<LikeDislikeEntry, LikeDislikeEntryDto>();
            CreateMap<Comment, CommentDTO>().ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Comment1));


        }
    }
}
