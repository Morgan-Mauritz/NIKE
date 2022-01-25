using Api.Helpers;
using AutoMapper;

namespace Api.Model.MappingProfiles
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>().ForMember(des => des.Password, opt => opt.MapFrom(src => src.Password.GenerateEncryption()));
            CreateMap<User, UserApiDto>(); 
        }
    }
}
