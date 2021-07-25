using System;
using System.Threading.Tasks;

namespace src
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string address = "freiberg";
            string filename = "freiberg.xml";
            OsmDataSearch osmDataSearch = new OsmDataSearch();
            osmDataSearch.SearchForAdress(address);
            await Download.ToFile(osmDataSearch.GetCityDataUrl(),filename);
            

        }
    }
}
