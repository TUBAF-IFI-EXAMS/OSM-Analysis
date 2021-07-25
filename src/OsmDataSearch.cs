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

            box = (BoundingBox)searchResults.BoundingBox;

            //Ausgabe der Ergebnisse 
            Console.WriteLine($"Freiberg ID {searchResults.PlaceID}");

            Console.WriteLine($"maxlat = {box.maxLatitude} maxlong = {box.maxLongitude}      minlong  = {box.minLongitude} minlat = {box.minLatitude}");

            Console.WriteLine($"OSmid {searchResults.OSMID}, OSMType: {searchResults.OSMType}, Class :{searchResults.Class} ClassType :  {searchResults.ClassType} Category:  {searchResults.Category}");


            foreach (KeyValuePair<string, string> elements in searchResults.AlternateNames)
            {
                Console.WriteLine("{0} and {1}", elements.Key, elements.Value);
            }


         
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