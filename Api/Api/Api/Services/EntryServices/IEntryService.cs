using Api.Model;
using System.Threading.Tasks;

namespace Api.Services.EntryServices
{
    public interface IEntryService
    {

        Task<EntryDto> SetEntry(AddEntry entryDto);
        Task<EntryDto> UpdateEntry(UpdateEntry updateEntry, long id);
        Task<EntryDto> DeleteEntry(long id);



    }
}
