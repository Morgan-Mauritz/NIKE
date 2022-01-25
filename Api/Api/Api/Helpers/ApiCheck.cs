using Api.Repository;
using System;
using Api.Model;
using System.Threading.Tasks;

namespace Api.Helpers
{ 
    public class ApiCheck
    {
        private readonly IRepository<User> _userRepository;

        public ApiCheck(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> ApiKeyCheck(string apiKey)
        {
            var userToCheck = await _userRepository.GetByApiKey(apiKey);
            if (userToCheck == null)
            {
                throw new UnauthorizedAccessException("Du har inte rättigheter");
            }
            return userToCheck; 
        }
    }

}

