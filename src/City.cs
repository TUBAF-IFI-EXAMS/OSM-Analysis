using System;
using OsmSharp;
using System.Linq;
using OsmSharp.Streams;

namespace src
{
    class City : LoadData
    {
        protected string name;
        protected int postcode;
        protected int population;
        protected string country;
        protected double wayDistance;



        public Way[] GetAllWays()
        {

            var source = Source();
            var wayTemp = (from osmWays in source
                           where osmWays.Type == OsmSharp.OsmGeoType.Way
                           select osmWays).ToArray();

            Way[] allWays = new Way[wayTemp.Length];

            Console.WriteLine(wayTemp.Length);

            for (int i = 0; i < allWays.Length; i++)
            {
                allWays[i] = (Way)wayTemp[i];
            }

            return allWays;

        }


        public City(string fileName) : base(fileName)
        {

        }

    }
}