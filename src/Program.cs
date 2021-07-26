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
          
            City cit = new City(filename);
            
            Console.WriteLine("Einwohner: " + cit.Population());
            Console.WriteLine("Land: " + cit.Country);
            Console.WriteLine("Stadt: " + cit.Name);
            Console.WriteLine("PLZ: " + cit.PostalCode);
            //cit.GetAllWays();
             await Download.ToFile(osmDataSearch.GetCityDataUrl(),filename);
            FilteredCity filteredCity = new FilteredCity(filename);
   
            
           


            //LoadData loadData = new LoadData(filename);
            //cit.GetAllWays();

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
