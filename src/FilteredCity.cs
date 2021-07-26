using System;
using System.IO;
using System.Linq;
using OsmSharp;
using OsmSharp.Tags;
using OsmSharp.Streams;


namespace src
{
    class FilteredCity : City
    {
        public Way[] FilterWays(string waytype, string highway = "highway")
        {
            Tag tag = new Tag();
            tag.Key = highway;
            tag.Value = waytype;

            var waysToFilter = (from osmGeo in Source()
                                where osmGeo.Type == OsmSharp.OsmGeoType.Way &&
                                 (osmGeo.Tags != null && osmGeo.Tags.Contains(tag))
                                select osmGeo).ToArray();

            Way[] filteredWays = new Way[waysToFilter.Length];

            Console.WriteLine(filteredWays.Length);

            for (int i = 0; i < filteredWays.Length; i++)
            {
                filteredWays[i] = (Way)waysToFilter[i];
                Console.WriteLine(filteredWays[i].Id);

            }
            var cmp = filteredWays.ToComplete();


            return filteredWays;

        }


        public Way[] GetspecificWay(string wayname, string key = "name")
        {

            //var source = Source();
            Tag tag = new Tag();
            tag.Key = key;
            tag.Value = wayname;

            var waysToFilter = (from osmGeo in Source()
                                where osmGeo.Type == OsmSharp.OsmGeoType.Way &&
                                 (osmGeo.Tags != null && osmGeo.Tags.Contains(tag))
                                select osmGeo).ToArray();


            Way[] specificWay = new Way[waysToFilter.Length];

             for (int i = 0; i < specificWay.Length; i++)
            {
                specificWay[i] = (Way)waysToFilter[i];
                Console.WriteLine(specificWay[i].Id);

            }

            return specificWay;
        }

        public FilteredCity(string filename) : base(filename)
        {

        }
    }
}