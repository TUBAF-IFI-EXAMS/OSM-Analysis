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
    class OsmData:IFilter
    {

        private BoundingBox box = new BoundingBox();

        private Node[] sourceNode;
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

        // Ergebnis direkt aus OSM API 
        public async Task GetOSMResults()
        {

            var clientFactory = new ClientsFactory(null, new HttpClient(),
           "https://master.apis.dev.openstreetmap.org/api/");
            var client = clientFactory.CreateNonAuthClient();


            // Begrenzung des gesuchtes Objektes
            Bounds c = new Bounds();
            c.MaxLongitude = (float)box.maxLongitude;
            c.MaxLatitude = (float)box.maxLatitude;
            c.MinLatitude = (float)box.minLatitude;
            c.MinLongitude = (float)box.minLongitude;

        
            var map = await client.GetMap(c);
            var firstNode = map.Nodes[0];
            Console.WriteLine("Length " + map.Nodes.Length);
        
            sourceNode = map.Nodes;


            var firstWay = map.Ways[0];


            long nodeId = Convert.ToInt64(firstNode.Id.Value);
            Console.WriteLine("Node ID " + firstNode.Id.Value);

            var nodeHistory = await client.GetNode(nodeId);
            var wayHiystory = await client.GetWay(Convert.ToInt64(firstWay.Id.Value));

            Console.WriteLine(nodeHistory);
            Console.WriteLine(nodeHistory.TimeStamp);
            Console.WriteLine(nodeHistory.Tags);
            Console.WriteLine(nodeHistory.Version);
            Console.WriteLine("lat" + nodeHistory.Latitude);
            Console.WriteLine(nodeHistory.Longitude);

            Console.WriteLine(wayHiystory);
            Console.WriteLine(wayHiystory.TimeStamp);
            Console.WriteLine(wayHiystory.Tags);
            Console.WriteLine(wayHiystory.Version);


        }


        public void FilterData()
        {

            var filtered2 = from osmGeo in sourceNode
                            where osmGeo.Id % 100000 != 0 //  leave only objects with and id not  dividable by 100000.
                            select osmGeo;
            Console.WriteLine("Filtered Nodes:");
            foreach (var osmGeo in filtered2)
            {
                Console.WriteLine(osmGeo.ToString());
            }
        }


    }
}