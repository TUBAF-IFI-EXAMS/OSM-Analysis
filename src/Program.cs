using System;
using System.IO;
using System.Threading.Tasks;

namespace src
{
    class Program
    {
        static async Task Main(string[] args)
        {
           try 
           {
            string address = "Freiberg";
            string filename = @"freiberg.xml";
            OsmDataSearch osmDataSearch = new OsmDataSearch();
            osmDataSearch.SearchForAdress(address);
          
            City city = new City(filename);
            
            Console.WriteLine("Einwohner: " + city.Population());
            Console.WriteLine("Land: " + city.Country);
            Console.WriteLine("Stadt: " + city.Name);
            Console.WriteLine("PLZ: " + city.PostalCode);
            
            await Download.ToFile(osmDataSearch.GetCityDataUrl(),filename);
            FilteredCity filteredCity = new FilteredCity(filename);
   

        // Berechnung der Straßenlänge
    
        double length = 0.0;
        var Calc = new CalculateDistance(filename);
        length = Calc.CalculateNetLength(filteredCity.GetspecificWay("Gustav-Zeuner-Straße"));
        Console.WriteLine($"Länge = {length} km");
           
           }
           catch(FileNotFoundException ex)
           {
               Console.WriteLine(ex);
           }
        
        }
    }
}
