using System;
using System.Threading.Tasks;

namespace src
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string address = "Hainichen";
            string filename = @"Hainichen.xml";
            OsmDataSearch osmDataSearch = new OsmDataSearch();
            osmDataSearch.SearchForAdress(address);
          
            City cit = new City(filename);
            
            Console.WriteLine("Einwohner: " + cit.Population());
            Console.WriteLine("Land: " + cit.Country);
            Console.WriteLine("Stadt: " + cit.Name);
            Console.WriteLine("PLZ: " + cit.PostalCode);
            //cit.GetAllWays();
             await Download.ToFile(osmDataSearch.GetCityDataUrl(),filename);
            FilteredCity filteredCity = new FilteredCity(filename);
   
            filteredCity.GetspecificWay("Johanna-Römer-Straße");
           


            //LoadData loadData = new LoadData(filename);
            //cit.GetAllWays();

    // Berechnung der Straßenlänge
    /*
        double length = 0.0;
        var Calc = new CalculateDistance();
        length = Calc.CalculateNetLength(testnetz);
        Console.WriteLine($"Länge = {length} km");
      */      

        }
    }
}
