using Api.Model;
using Api.Repository;
using AutoMapper;
using System.Threading.Tasks;

namespace Api.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _repository;
        public UserService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<UserDto> AddUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
           
            await _repository.Add(user);

            return userDto;
        }
    }
}
