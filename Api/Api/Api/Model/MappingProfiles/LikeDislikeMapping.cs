using AutoMapper;

namespace Api.Model.MappingProfiles
{
    public class LikeDislikeMapping : Profile
    {
        public LikeDislikeMapping()
        {
            CreateMap<LikeDislikeEntry, LikeDislikeEntryDto>();
        }
    }
}
