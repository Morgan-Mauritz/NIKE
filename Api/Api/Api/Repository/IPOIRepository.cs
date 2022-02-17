using Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IPOIRepository
    {
        Task<POI> Get(double Longitude, double Latitude, string name);
        Task<(List<POI> poiList, int total)> GetFiltered(FilterPOI filterPOI); 
        Task<POI> Set(POIDto pOIDto);
    }
}
