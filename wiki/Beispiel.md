Ein kleines Beispiel zur Anwendung des Projekts:

```
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
```

Hier wird ein Objekt der Klasse **OsmDataSearch** erstellt, mit deren Hilfe der Datensatz der Stadt Freiberg gesucht wird. Anschließend wird dieser Datensatz im neuen **City**- Objekt abgelegt und der Datensatz heruntergeladen. 

Am Beispiel der Bestimmung der Länge einer Straße in FG wird diese Funktionalität getestet. Dafür muss zunächst eine Filterung (_GetspecificWay_) stattfinden, welche die _ways_ mit dem Straßennamen beinhalten. Diese werden dann an die _CalculateNetlength()_-Methode übergeben, welche die Länge der Straße zurückgibt.