using Api.Model;
using System.Threading.Tasks;

namespace Api.Services.EntryServices
{
    public interface IEntryService
    {

        Task<EntryDto> SetEntry(AddEntry entryDto, string apiKey);
        Task<EntryDto> UpdateEntry(UpdateEntry updateEntry, string apiKey, long id);
        Task<EntryDto> RemoveEntry(long id, string apiKey);
    }
}
