using Api.Model;
using System.Threading.Tasks;

namespace Api.Services.AuthorizationServices
{
    public interface IAuthorizationService
    {
        public Task<UserApiDto> SignIn(LogInModel loginModel); 
    }
}
