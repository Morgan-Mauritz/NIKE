using Api.Helpers;
using Api.Model;
using Api.Repository;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace Api.Services.AuthorizationServices
{
    public class AuthorizationService : IAuthorizationService
    { 
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper; 
        public AuthorizationService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper; 
        }
        public async Task<UserApiDto> SignIn(LogInModel loginModel)
        {
            var passwordHash = loginModel.Password.GenerateEncryption();
            var user = await _repository.GetByLogin(loginModel.Email, passwordHash); 
            
            if(user == null)
            {
                throw new UnauthorizedAccessException("There is no user for this login!"); 
            }

            return _mapper.Map<UserApiDto>(user); 
        }
    }
}
