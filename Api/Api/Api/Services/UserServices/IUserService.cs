using Api.Model;
using System.Threading.Tasks;

namespace Api.Services.UserServices
{
    public interface IUserService
    {
        Task<UserDto> AddUser(UserDto user);
    }
}
