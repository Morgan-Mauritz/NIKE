using Api.Model;
using Api.Repository;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.POIServices
{
    public class POIService : IPOIService
    {
        private readonly IMapper _mapper;
        private readonly IPOIRepository _POIRepository;
        public POIService(IPOIRepository POIRepository, IMapper mapper)
        {
            _POIRepository = POIRepository;
            _mapper = mapper;
        }
        public async Task<POIDto> GetPOI(double Longitude, double Latitude, string name)
        {
            return _mapper.Map<POIDto>(await _POIRepository.Get(Longitude, Latitude, name));
        }
        public async Task<List<POIDto>> GetPOIList(FilterPOI filterPOI)
        {
            return _mapper.Map<List<POIDto>>(await _POIRepository.GetFiltered(filterPOI)); 
        }

        public async Task<POIDto> SetPOI(POIDto pOIDto)
        {
            return _mapper.Map<POIDto>(await _POIRepository.Set(pOIDto));  
        }
    }
}
