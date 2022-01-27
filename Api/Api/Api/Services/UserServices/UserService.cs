using Api.Model;
using Api.Repository;
using AutoMapper;
using System;
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
        public async Task<UserDto> AddUser(AddUserDto addUserDto)
        {
            var user = _mapper.Map<User>(addUserDto);
            
            user.ApiKey = Guid.NewGuid().ToString();

            await _repository.Add(user);

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
        public async Task<UserDto> UpdateUser(UpdateUserDto updateUserDto, string apiKey)
        {
            var user = await _repository.GetByApiKey(apiKey);

            if(user == null)
            {
                throw new UnauthorizedAccessException("Finns ingen användare");
            }
            
            var updatedUser = _mapper.Map(updateUserDto, user);

            await _repository.UpdateUser();

            return _mapper.Map<UserDto>(updatedUser);
        }

        public async Task<UserDto> RemoveUser(string apiKey)
        {
            var user = await _repository.GetByApiKey(apiKey);

            if(user == null)
            {
                throw new UnauthorizedAccessException("Du kan inte ta bort ditt konto");
            }

            await _repository.RemoveUser(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}
