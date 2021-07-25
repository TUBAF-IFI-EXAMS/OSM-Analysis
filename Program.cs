using System;
using System.IO;
using System.Linq;
using OsmSharp;
using OsmSharp.Streams;

// Test der Entfernungsmessung

    public class Program
    {

        static void Main(string[] args)
        {
        
         var source = new XmlOsmStreamSource(File.OpenRead("Testmap.osm"));
         var filtered = from osmGeo in source where osmGeo.Type == OsmGeoType.Way select osmGeo;
         var filtered2 = from osmGeo in filtered where osmGeo.Id % 500 == 0 select osmGeo;

         Console.WriteLine($"Anzahl Wege: {filtered2.Count()}");
         Way[] testnetz = new Way[filtered2.Count()];

        for (int i=0; i<filtered2.Count();i++){
            testnetz[i] = (Way)filtered2.ElementAt(i);              
        }

        double length = 0.0;
        var Calc = new CalculateDistance();
        length = Calc.CalculateNetLength(testnetz);
        Console.WriteLine($"Länge = {length} km");

        }

    }
