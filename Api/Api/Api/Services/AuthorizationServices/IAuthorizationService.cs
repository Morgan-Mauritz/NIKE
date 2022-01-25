using Api.Model;
using System.Threading.Tasks;

namespace Api.Services.AuthorizationServices
{
    public interface IAuthorizationService
    {
        public Task<UserDto> SignIn(LogInModel loginModel); 
    }
}
