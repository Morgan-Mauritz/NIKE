using Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class EntryRepository : IEntryRepository
    {
        private readonly NIKEContext _context;
        private readonly IPOIRepository _poiRepository;
        public EntryRepository(NIKEContext context)
        {
            _context = context;
        }

        public async Task<Entry> Set(Entry entry)
        {

            await _context.AddAsync(entry);

            await _context.SaveChangesAsync();

            return entry;   


        }
    }
}
