using System;
using System.Threading.Tasks;

namespace src
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string address = "Dortmund";
            string filename = @"freiberg.xml";
            OsmDataSearch osmDataSearch = new OsmDataSearch();
            osmDataSearch.SearchForAdress(address);
            //await Download.ToFile(osmDataSearch.GetCityDataUrl(),filename);
            City cit = new City(filename);
            
            Console.WriteLine("Einwohner: " + cit.Population());
            Console.WriteLine("Land: " + cit.Country);
            Console.WriteLine("Stadt: " + cit.Name);
            Console.WriteLine("PLZ: " + cit.PostalCode);
            //cit.GetAllWays();
            
            FilteredCity filteredCity = new FilteredCity(filename);
   
            filteredCity.GetspecificWay("Johanna-Römer-Straße");
           


            //LoadData loadData = new LoadData(filename);
            //cit.GetAllWays();

            

        }
    }
}
