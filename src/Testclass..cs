using Xunit;
using System;
using System.IO;

namespace src
{
public class Testclass
{   

    // Prüft Plausibilität der Einwohnerzahlen
    
    [Theory]
    [InlineData("Freiberg",@"freiberg.xml",30000,50000)]
    [InlineData("Hainichen",@"hainichen.xml",5000,10000)]
    public void Einwohner_test(string adr, string file, int lower, int higher)
    {

        string address = adr;
        string filename = file;
            OsmDataSearch osmDataSearch = new OsmDataSearch();
            osmDataSearch.SearchForAdress(address);
          
        City c = new City(filename);
    
        int result = c.Population();
        Console.WriteLine(result);
        Assert.InRange<int>(result,lower,higher);   
    }
    


}
}