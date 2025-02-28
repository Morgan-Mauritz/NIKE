﻿using Api.Model;
using Api.Exceptions;
using Api.Repository;
using AutoMapper;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

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

        public async Task<EntryDto> SetEntry(AddEntry entryDto, string apiKey)
        {
            var userToCheck = await _userRepository.GetByApiKey(apiKey);
            if (userToCheck == null)
            {
                throw new UnauthorizedAccessException("Du får inte redigera det här inlägget");
            }

            var POI = await _poiRepository.Get(entryDto.POI.Longitude, entryDto.POI.Latitude, entryDto.POI.Name);

            if(POI == null)
            {
                POI = await _poiRepository.Set(entryDto.POI);
            }

            var userEntryCheck = await _entryRepository.Get(POI.Id, userToCheck.Id);
            if (userEntryCheck != null)
            {
                throw new Exception("Du har redan gjort ett inlägg för den här platsen");
            }

            var entry = _mapper.Map<Entry>(entryDto);

            entry.POIID = POI.Id;

            entry.UserId = userToCheck.Id;

           await _entryRepository.Set(entry);

           return new EntryDto() { POIString = POI.Name, Username = userToCheck.Username, Description = entryDto.Description, Rating = entryDto.Rating };
        }

        public async Task<EntryDto> UpdateEntry(UpdateEntry updateEntry, string apiKey)
        {

            var entry = await _entryRepository.GetWithTracking(updateEntry.Id);
            if (entry == null)
            {
                throw new NotFoundException("Kunde inte hitta inlägget");
            }

            var userToCheck = await _userRepository.GetByApiKey(apiKey); 
            if(entry.UserId != userToCheck.Id)
            {
                throw new UnauthorizedAccessException("Du får inte redigera det här inlägget");
            }

            var updatedEntry = _mapper.Map(updateEntry, entry);

            await _entryRepository.UpdateEntry();

            return _mapper.Map<EntryDto>(updatedEntry);
        }
        public async Task<EntryDto> RemoveEntry(long id, string apiKey)
        {

            var entryDelete = await _entryRepository.GetWithTracking(id);

            if (entryDelete == null)
            {
                throw new NotFoundException("Kunde inte hitta inlägget");
            }
            var userToCheck = await _userRepository.GetByApiKey(apiKey);
            if (userToCheck == null || entryDelete.UserId != userToCheck.Id)
            {
                throw new UnauthorizedAccessException("Du får inte ta bort det här inlägget");
            }

            await _entryRepository.RemoveEntry(entryDelete);

            return _mapper.Map<EntryDto>(entryDelete);
        }

        public async Task<LikeDislikeEntryDto> AddLike(long entryId, string ApiKey)
        {
            var entry = await _entryRepository.GetWithTracking(entryId);
            var user = await _userRepository.GetByApiKey(ApiKey);

            if (entry == null)
            {
                throw new NotFoundException("Kunde inte hitta inlägget");
            }
            if (user == null)
            {
                throw new UnauthorizedAccessException("Kunde inte hitta användaren");
            }

            var like = await _entryRepository.GetLike(user.Id, entryId);

            if (like == null)
            {
                var addLike = new LikeDislikeEntry() { EntryId = entryId, UserId = user.Id, Likes = 1};
                await _entryRepository.AddLike(addLike);
                return _mapper.Map<LikeDislikeEntryDto>(addLike);
            }
            else
            {
                await _entryRepository.RemoveLike(like);
                return _mapper.Map<LikeDislikeEntryDto>(like);
            }
        }

        public async Task<(List<CommentDTO> comments, int total)> GetUserComments(string apiKey, BaseFilter filter)
        {
            var user = await _userRepository.GetByApiKey(apiKey);
            if (user == null) 
            {
                throw new UnauthorizedAccessException("Du har inte behörighet att visa detta."); 
            }

            var result = await _entryRepository.GetUserComments(user.Id, filter);

            return (_mapper.Map<List<CommentDTO>>(result.comments), result.total);
        }

        public async Task<(List<LikeDislikeEntryDto> likes, int total)> GetUserLikes(string apiKey, BaseFilter filter)
        {
            var user = await _userRepository.GetByApiKey(apiKey);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Du har inte behörighet att visa detta.");
            }

            var result = await _entryRepository.GetUserLikes(user.Id, filter);

            return (_mapper.Map<List<LikeDislikeEntryDto>>(result.likes), result.total);
        }
        public async Task<(List<EntryDto> entries, int total)> GetUserEntries(string apiKey, BaseFilter filter)
        {
            var user = await _userRepository.GetByApiKey(apiKey);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Du har inte behörighet att visa detta.");
            }

            var result = await _entryRepository.GetUserEntries(user.Id, filter);

            return (_mapper.Map<List<EntryDto>>(result.entries), result.total);
        }

        public async Task<(List<EntryDto> list, int total)> GetEntries(FilterEntry filter)
        {
            var result = await _entryRepository.GetEntries(filter);
            return (_mapper.Map<List<EntryDto>>(result.list), result.total);
        }
    }
}
