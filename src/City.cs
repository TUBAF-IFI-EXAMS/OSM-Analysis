using System;
using OsmSharp;
using System.Linq;
using OsmSharp.Streams;

namespace src
{
    class City :LoadData
    {
        private string name;
        private int postcode;
        private int population;
        private string country;
        private double wayDistance;


                
        public Way[] GetAllWays()
        {
           
            var wayTemp = from osmWays in source
                          where osmWays.Type ==OsmSharp.OsmGeoType.Way
                          select osmWays;

            var wayTempToComplete = wayTemp.ToComplete();

            Way[] allWays = new Way[wayTempToComplete.Count()];


            for(int i= 0; i<= allWays.Length; i++)
            {
                allWays[i] = (Way)wayTempToComplete.ElementAt(i);
            }
            Console.WriteLine("Es sind + " + allWays.Length + "Ways");


            return allWays;

        }
        
        
        public City(string fileName) : base( fileName)
        {

        }

    }
}