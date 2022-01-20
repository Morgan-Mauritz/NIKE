using Api.Model;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IEntryRepository
    {

        Task<Entry> Set(Entry entry);


    }
}
