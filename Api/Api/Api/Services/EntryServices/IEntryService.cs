using Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.EntryServices
{
    public interface IEntryService
    {

        Task<EntryDto> SetEntry(AddEntry entryDto, string apiKey);
        Task<EntryDto> UpdateEntry(UpdateEntry updateEntry, string apiKey);
        Task<LikeDislikeEntryDto> AddLike(long entryId, string ApiKey);
        Task<EntryDto> RemoveEntry(long id, string apiKey);
        Task<(List<CommentDTO> comments, int total)> GetUserComments(string apiKey, BaseFilter filter);
        Task<(List<EntryDto> entries, int total)> GetUserEntries(string apiKey, BaseFilter filter);
        Task<(List<LikeDislikeEntryDto> likes, int total)> GetUserLikes(string apiKey, BaseFilter filter);
        Task<(List<EntryDto> list, int total)> GetEntries(FilterEntry filter);
    }
}
