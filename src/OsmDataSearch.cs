using System;
using System.Threading.Tasks;
using Nominatim.API.Models;
using Nominatim.API.Geocoders;
using System.Collections.Generic;
using System.Linq;


namespace src
{
    class OsmDataSearch
    {
        
          // Grenzen der zu suchende ort
        private BoundingBox box = new BoundingBox();
        public string elementType = "*";
        private ForwardGeocoder forwardGeocoder = new ForwardGeocoder();

        public static GeocodeResponse localInfo;
         public BoundingBox Box => box;

        // Ort suchen  
        public void SearchForAdress(string address)
        {

            var geocodeResponses = forwardGeocoder.Geocode(new ForwardGeocodeRequest
            {
                queryString = address,

                
                ShowExtraTags = true,
                ShowAlternativeNames = true,
                ShowKML = true,
                ViewboxBoundedResults = true,

            });

            // Suchergebniss
            var searchResults = geocodeResponses.Result[0];
            localInfo =searchResults;
            
            box = (BoundingBox)searchResults.BoundingBox;
           

         
        }

        public string GetCityDataUrl()
        {
            
            string querybox = $"{box.minLongitude},{box.minLatitude},{box.maxLongitude},{box.maxLatitude}]";

            // Url zum Download der Daten 
            string url = $"http://www.overpass-api.de/api/xapi?{elementType}[bbox={querybox}";

            return url;
        }
        




    }
}