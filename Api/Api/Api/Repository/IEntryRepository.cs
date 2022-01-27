using Api.Model;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IEntryRepository
    {
        Task<Entry> Set(Entry entry);
        Task<Entry> Get(long poiID, long userID);
        Task<Entry> GetWithTracking(long entryID);
        Task UpdateEntry();
        Task DeleteEntry(Entry entry);
        Task AddLike(LikeDislikeEntry entryLike);
        Task RemoveLike(LikeDislikeEntry entryLike);
        Task<LikeDislikeEntry> GetLike(long userId, long entryId);
    }
}
