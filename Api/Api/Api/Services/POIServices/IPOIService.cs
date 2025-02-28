﻿using Api.Model;
using System.Threading.Tasks;
using System.Collections.Generic; 

namespace Api.Services.POIServices
{
    public interface IPOIService
    {
        Task<POIDto> GetPOI(double Longitude, double Latitude, string name);
        Task<(List<POIDto> poiList, int total)> GetPOIList(FilterPOI filterPOI);
        Task<POIDto> SetPOI(POIDto poiDto, string apiKey); 
    }
}
