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
         var filtered = (from osmGeo in source where osmGeo.Type == OsmGeoType.Way && osmGeo.Id % 100 == 0 select osmGeo).ToArray();
         
         Console.WriteLine($"Anzahl Wege: {filtered.Length}");
         Way[] testnetz = new Way[filtered.Length];

        for (int i=0; i<filtered.Length;i++){
            testnetz[i] = (Way)filtered[i];   
            Console.WriteLine($"Way {i} einfügen...");         
        }

        double length = 0.0;
        var Calc = new CalculateDistance();
        length = Calc.CalculateNetLength(testnetz);
        Console.WriteLine($"Länge = {length} km");

        }

    }
