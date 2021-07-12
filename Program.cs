using System;
using System.Net.Http;
using OsmSharp.API;
using OsmSharp.IO.API;
using System.Threading.Tasks;


namespace osm
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
                
        Bounds WashingtonDC = new Bounds()
        {
            MinLongitude = -77.0671918f,
            MinLatitude = 38.9007186f,
            MaxLongitude = -77.00099990f,
            MaxLatitude = 38.98734f
        };


            var clientFactory = new ClientsFactory(null, new HttpClient(),
            "https://master.apis.dev.openstreetmap.org/api/");
            var client = clientFactory.CreateNonAuthClient();
            var node = await client.GetNode(7868);
            Console.WriteLine(node.Tags);


            Task<Osm> GetWashingtonObject()
        {
            return client.GetMap(WashingtonDC);
        }   

            var map = await GetWashingtonObject();
            var nodeId = (map.Nodes.Length)-100;
            var knoten = await client.GetNode(nodeId);
            Console.WriteLine(knoten.Latitude);
            Console.WriteLine(knoten.Longitude);



        }


    }
}