﻿using Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class EntryRepository : IEntryRepository
    {
        private readonly NIKEContext _context;
        
        public EntryRepository(NIKEContext context)
        {
            _context = context;
        }

        public async Task<Entry> Get(long poiID, long userID)
        {
            return await _context.Entries.AsNoTracking().FirstOrDefaultAsync(x => x.POIID == poiID && x.UserId == userID);

        }

        public async Task<Entry> GetWithTracking(long entryID)
        {
            return await _context.Entries.FirstOrDefaultAsync(x => x.Id == entryID);
        }

        public async Task<Entry> Set(Entry entry)
        {

            await _context.AddAsync(entry);

            await _context.SaveChangesAsync();

            return entry;   
        }

        public async Task UpdateEntry()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RemoveEntry(Entry entryRemove)
        {
            _context.Remove(entryRemove);
            await _context.SaveChangesAsync();
        }

        public async Task AddLike(LikeDislikeEntry entryLike)
        {
            _context.LikeDislikeEntry.Add(entryLike);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLike(LikeDislikeEntry entryLike)
        {
            _context.LikeDislikeEntry.Remove(entryLike);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<Entry> list, int total)> GetEntries(FilterEntry filter)
        {
            var query = _context.Entries.Where(x => x.POI.Name.ToLower() == filter.POI.Replace("+", " ").ToLower()).Include(x => x.LikeDislikeEntries)
                .Include(x => x.User).Include(x => x.Comments).ThenInclude(x => x.User);

            var total = query.Count();

            return (await query.Skip(filter.Offset).Take(filter.Amount).ToListAsync(), total);
        }

        public async Task<LikeDislikeEntry> GetLike(long userId, long entryId)
        {
            return await _context.LikeDislikeEntry.FirstOrDefaultAsync(x => x.EntryId == entryId && x.UserId == userId);
        }

        public async Task<(List<Comment> comments, int total)> GetUserComments(long userID, BaseFilter filter) 
        {
            var query = _context.Comments.Where(x => x.UserId == userID).Include(x => x.Entry).ThenInclude(x => x.User).Include(x => x.Entry).ThenInclude(x => x.POI);

            var total = query.Count();

            return (await query.Skip(filter.Offset).Take(filter.Amount).ToListAsync(), total);
        }

        public async Task<(List<Entry> entries, int total)> GetUserEntries(long userID, BaseFilter filter)
        {
            var query = _context.Entries.Where(x => x.UserId == userID).Include(x => x.User).Include(x => x.POI);

            var total = query.Count();

            return (await query.Skip(filter.Offset).Take(filter.Amount).ToListAsync(), total);
        }

        public async Task<(List<LikeDislikeEntry> likes, int total)> GetUserLikes(long userID, BaseFilter filter)
        {
            var query = _context.LikeDislikeEntry.Where(x => x.UserId == userID).Include(x => x.Entry).ThenInclude(x => x.User).Include(x => x.Entry).ThenInclude(x => x.POI);

            var total = query.Count();

            return (await query.Skip(filter.Offset).Take(filter.Amount).ToListAsync(), total);
        }

    }
}
