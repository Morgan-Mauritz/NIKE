using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Model;
using Api.Repository;
using AutoMapper;

namespace Api.Services.POIServices
{
    public class POIService : IPOIService
    {
        private readonly IMapper _mapper;
        private readonly IPOIRepository _POIRepository;
        private readonly IRepository<User> _userRepository;
        public POIService(IPOIRepository POIRepository, IMapper mapper, IRepository<User> userRepository)
        {
            _POIRepository = POIRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<POIDto> GetPOI(double Longitude, double Latitude, string name)
        {
            return _mapper.Map<POIDto>(await _POIRepository.Get(Longitude, Latitude, name));
        }
        public async Task<(List<POIDto> poiList, int total)> GetPOIList(FilterPOI filterPOI)
        {
            var (poiList, total) = await _POIRepository.GetFiltered(filterPOI);
            return (_mapper.Map<List<POIDto>>(poiList), total);
        }
        public async Task<POIDto> SetPOI(POIDto pOIDto, string apiKey)
        {

            var userToCheck = await _userRepository.GetByApiKey(apiKey);
            if (userToCheck == null)
            {
                throw new UnauthorizedAccessException("Du behöver logga in för att lägga till en sevärdhet");
            }

            var checkIfExists = await _POIRepository.Get(pOIDto.Longitude, pOIDto.Latitude, pOIDto.Name);
            if (checkIfExists != null)
            {
                throw new Exception("Du har redan gjort ett inlägg för den här platsen");
            }

            return _mapper.Map<POIDto>(await _POIRepository.Set(pOIDto));
        }
    }
}
