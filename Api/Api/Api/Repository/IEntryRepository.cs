using Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IEntryRepository
    {
        Task<Entry> Set(Entry entry);
        Task<Entry> Get(long poiID, long userID);
        Task<Entry> GetWithTracking(long entryID);
        Task UpdateEntry();
        Task AddLike(LikeDislikeEntry entryLike);
        Task RemoveLike(LikeDislikeEntry entryLike);
        Task<LikeDislikeEntry> GetLike(long userId, long entryId);
        Task RemoveEntry(Entry entry);
        Task<(List<Comment>  comments, int total)> GetUserComments(long userID, BaseFilter filter);
        Task<(List<Entry>  entries, int total)> GetUserEntries(long userID, BaseFilter filter);
        Task<(List<LikeDislikeEntry>  likes, int total)> GetUserLikes(long userID, BaseFilter filter);
        Task<(List<Entry> list, int total)> GetEntries(FilterEntry filter);

    }
}
