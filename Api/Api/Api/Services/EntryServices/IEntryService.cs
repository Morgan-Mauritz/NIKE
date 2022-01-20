using Api.Model;
using System.Threading.Tasks;

namespace Api.Services.EntryServices
{
    public interface IEntryService
    {

        Task<EntryDto> SetEntry(AddEntry entryDto);



    }
}
