using System;
using System.Threading.Tasks;



using System.Linq;

namespace osm
{


    class Program
    {

        static async Task Main(string[] args)
        {

            string adress = "Berlin";

            OsmData osmData = new OsmData();
            osmData.SearchForAdress(adress);
            await osmData.GetOSMResults();
            osmData.FilterData();
            

            Overpass overpass = new Overpass();
            overpass.path = @"freiberg.xml";
            overpass.filename = "freiberg.xml";

            await Download.ToFile(overpass.ReturnURL(), overpass.filename);
            await overpass.FilterData();


        }


    }
}
