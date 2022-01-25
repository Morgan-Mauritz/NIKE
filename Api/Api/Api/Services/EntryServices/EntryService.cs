using Api.Model;
using Api.Exceptions;
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

            var userEntryCheck = await _entryRepository.Get(POI.Id, user.Id);
            if (userEntryCheck != null)
            {
                throw new Exception("Du har redan gjort ett inlägg för den här platsen");
            }

            var entry = _mapper.Map<Entry>(entryDto);

            entry.POIID = POI.Id;

            entry.UserId = user.Id;

           await _entryRepository.Set(entry);

           return new EntryDto() { POI = POI.Name, UserName = user.Username, Description = entryDto.Description, Rating = entryDto.Rating };
        }


        public async Task<EntryDto> UpdateEntry(UpdateEntry updateEntry, string apiKey, long id)
        {

            var entry = await _entryRepository.GetWithTracking(id);
            if (entry == null)
            {
                throw new NotFoundException("Kunde inte hitta inlägget");
            }

            var userToCheck = await _userRepository.GetByApiKey(apiKey); 
            if(userToCheck == null)
            {
                throw new UnauthorizedAccessException("Du måste vara inloggad för att göra detta"); 
            }
            if(entry.UserId != userToCheck.Id)
            {
                throw new UnauthorizedAccessException("Du får inte redigera det här inlägget");
            }

            var updatedEntry = _mapper.Map(updateEntry, entry);

            await _entryRepository.UpdateEntry();

            return _mapper.Map<EntryDto>(updatedEntry);
        }

        public async Task<EntryDto> DeleteEntry(long id)
        {

            var entryDelete = await _entryRepository.GetWithTracking(id);

            if (entryDelete == null)
            {
                throw new NotFoundException("Kunde inte hitta inlägget");
            }


            //TODO: API nyckel fix!!
            //if(entry.UserId != 1)
            //{
            //    throw new UnauthorizedAccessException();
            //}

            await _entryRepository.DeleteEntry(entryDelete);

            return _mapper.Map<EntryDto>(entryDelete);



        }



    }
}
