using Api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class POIRepository : IPOIRepository
    {
        private readonly NIKEContext _context;
        public POIRepository(NIKEContext context)
        {
            _context = context;
        }
        public async Task<POI> Get(double longitude, double latitude, string name)
        {
            var thingToLookat = await _context.POI
                .AsNoTracking().Include(x => x.City).ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(POI => (POI.Longitude >= longitude - 0.03 && POI.Longitude <= longitude + 0.03) 
                && (POI.Latitude >= latitude - 0.03 && POI.Latitude <= latitude + 0.03) && POI.Name == name);
            return thingToLookat; 
        }

        public async Task<(List<POI> poiList, int total)> GetFiltered(FilterPOI filterPOI)
        {
            var query = _context.POI.AsNoTracking().Include(c => c.City).ThenInclude(c => c.Country)
                .Where(x => (string.IsNullOrEmpty(filterPOI.Country) || x.City.Country.Name.ToLower().Contains(filterPOI.Country.ToLower()))
                && (string.IsNullOrEmpty(filterPOI.City) || x.City.Name.ToLower().Contains(filterPOI.City.ToLower())) 
                && (string.IsNullOrEmpty(filterPOI.Name) || x.Name.ToLower().Contains(filterPOI.Name.ToLower())));

            var total = query.Count(); 

            query = filterPOI.Sort switch
            {
                Sort.Name => query.OrderBy(x => x.Name),
                Sort.City => query.OrderBy(x => x.City.Name),
                Sort.Country => query.OrderBy(x => x.City),
                _ => query.OrderBy(x => x.Id) 
            };

            return (await query.Skip(filterPOI.Offset).Take(filterPOI.Amount).ToListAsync(), total); 
        }

        public async Task<POI> Set(POIDto pOIDto)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Name == pOIDto.City);

            if (city == null)
            {
                var country = await _context.Countries.FirstOrDefaultAsync(co => co.Name == pOIDto.Country);
                if (country == null)
                {
                    country = new Country { Name = pOIDto.Country };
                    await _context.Countries.AddAsync(country);
                    await _context.SaveChangesAsync();
                }
                city = new City { CountryId = country.Id, Name = pOIDto.City };
                await _context.Cities.AddAsync(city);
                await _context.SaveChangesAsync();
            }
            var poi = new POI { Name = pOIDto.Name, Longitude = pOIDto.Longitude, Latitude = pOIDto.Latitude, CityID = city.Id };

            await _context.AddAsync(poi);

            await _context.SaveChangesAsync();

            return poi; 
        }
    }
}
