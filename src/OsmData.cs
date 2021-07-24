using System;
using System.Threading.Tasks;
using Nominatim.API.Models;
using Nominatim.API.Geocoders;
using System.Collections.Generic;
using OsmSharp.IO.API;
using System.Net.Http;
using OsmSharp.API;
using OsmSharp;
using System.Linq;


namespace osm
{
    class OsmData
    {

        private BoundingBox box = new BoundingBox();

     
        private ForwardGeocoder forwardGeocoder = new ForwardGeocoder();

         public BoundingBox Box => box;

        // Adress suchen und alle wichtige information ausgeben 
        public void SearchForAdress(string adr)
        {


            var geocodeResponses = forwardGeocoder.Geocode(new ForwardGeocodeRequest
            {
                queryString = adr,

                //PostalCode = "09599",
                ShowExtraTags = true,
                ShowAlternativeNames = true,
                ShowKML = true,
                ViewboxBoundedResults = true,

            });

            // Suchergebniss
            var SearchResults = geocodeResponses.Result[0];

            box = (BoundingBox)SearchResults.BoundingBox;

            //Ausgabe der Ergebnisse 
            Console.WriteLine($"Freiberg ID {SearchResults.PlaceID}");

            Console.WriteLine($"maxlat = {box.maxLatitude} maxlong = {box.maxLongitude}      minlong  = {box.minLongitude} minlat = {box.minLatitude}");

            Console.WriteLine($"OSmid {SearchResults.OSMID}, OSMType: {SearchResults.OSMType}, Class :{SearchResults.Class} ClassType :  {SearchResults.ClassType} Category:  {SearchResults.Category}");


            foreach (KeyValuePair<string, string> elements in SearchResults.AlternateNames)
            {
                Console.WriteLine("{0} and {1}", elements.Key, elements.Value);
            }

            foreach (KeyValuePair<string, string> extratags in SearchResults.ExtraTags)
            {
                Console.WriteLine("{0} and {1}", extratags.Key, extratags.Value);
            }


            // GeoKML Liefert  die  Geometrie von way, node oder relation in KML format
            // Die Geometrie enthält  Longtude, Latitude und beschreibung des objektes

            char[] sepators = { '<', '>', ' ', ',', '/' };

            string[] polygonsplited = SearchResults.GeoKML.Split(sepators);

            double temp;
            polygonsplited = polygonsplited.Where(x => double.TryParse(x, out temp)).ToArray();

            // Extration von Latitude und  UND Longitude aus GeoKML
            List<double> latitude = new List<double>();
            List<double> longitude = new List<double>();
            for (int i = 1; i <= polygonsplited.Length; i += 2)
            {
                longitude.Add(double.Parse(polygonsplited[i - 1]));
                latitude.Add(double.Parse(polygonsplited[i]));
            }

        }




    }
}