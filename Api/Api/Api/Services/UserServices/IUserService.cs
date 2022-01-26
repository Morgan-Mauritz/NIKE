using Api.Model;
using System.Threading.Tasks;

namespace Api.Services.UserServices
{
    public interface IUserService
    {
        Task<UserDto> AddUser(AddUserDto addUserDto);
        Task<UserDto> UpdateUser(UpdateUserDto user, string apiKey);

        Task<UserDto> RemoveUser(string apiKey);
    }
}
