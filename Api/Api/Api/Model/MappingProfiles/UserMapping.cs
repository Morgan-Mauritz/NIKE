using AutoMapper;

namespace Api.Model.MappingProfiles
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>().ForMember(c => c.Password, opt => opt.Ignore());
            CreateMap<UserDto, User>();
            CreateMap<User, UserApiDto>();
            CreateMap<UpdateUserDto, User>().ForAllMembers(opt => opt.Condition((src, dest, srcmember) => srcmember != null));
            CreateMap<AddUserDto, User>();
        }
    }
}
