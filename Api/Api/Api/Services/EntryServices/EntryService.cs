using Api.Model;
using Api.Repository;
using AutoMapper;
using System.Threading.Tasks;
using System;


namespace Api.Services.EntryServices
{
    public class EntryService : IEntryService
    {
        private readonly IMapper _mapper;
        private readonly IEntryRepository _entryRepository;
        private readonly IPOIRepository _poiRepository;
        private readonly IRepository<User> _userRepository;
        public EntryService(IEntryRepository entryRepository, IMapper mapper, IPOIRepository poiRepository, IRepository<User> userRepository)
        {
            _entryRepository = entryRepository;
            _poiRepository = poiRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<EntryDto> SetEntry(AddEntry entryDto)
        {
            var user = await _userRepository.Get(entryDto.UserName);

            if (user == null)
            {
                throw new UnauthorizedAccessException();    
            }

            var POI = await _poiRepository.Get(entryDto.POI.Longitude, entryDto.POI.Latitude, entryDto.POI.Name);

            if(POI == null)
            {
                POI = await _poiRepository.Set(entryDto.POI);
            }


            var entry = _mapper.Map<Entry>(entryDto);

            entry.POIID = POI.Id;

            entry.UserId = user.Id;


            return _mapper.Map<EntryDto>(await _entryRepository.Set(entry));
        }
    }
}
