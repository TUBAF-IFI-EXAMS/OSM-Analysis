using System;
using System.Threading.Tasks;

namespace src
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string address = "freiberg";
            string filename = @"freiberg.xml";
            OsmDataSearch osmDataSearch = new OsmDataSearch();
            osmDataSearch.SearchForAdress(address);
            await Download.ToFile(osmDataSearch.GetCityDataUrl(),filename);
            City cit = new City(filename);
            LoadData loadData = new LoadData(filename);
           cit.GetAllWays();

            try{
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            

        }
    }
}
