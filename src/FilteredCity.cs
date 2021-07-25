using System;
using System.Linq;
using OsmSharp;
using OsmSharp.Tags;

namespace src
{
    class FilteredCity : City
    {
        public Way[] FilterWays(Tag[] waytype)
        {
            Tag[] tags = new Tag[waytype.Length];

            var source = Source();


            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = waytype[i];
            }

            Way[] filteredWays = new Way[2];
            Console.WriteLine(filteredWays.Length);

            for (int i = 0; i < filteredWays.Length; i++)
            {
                filteredWays[i] = (Way)filteredWays[i];
            }


            return filteredWays;


        }

        public Way GetspecificWay(string wayname, string key = null)
        {

            var source = Source();
            Tag tag = new Tag();
            tag.Key = key;
            tag.Value = wayname;

            var filter = from osmGeo in source
                         where osmGeo.Type == OsmSharp.OsmGeoType.Way &&
                         osmGeo.Tags.Contains(tag)
                         select osmGeo;

            Way specificWay = (Way)filter;

            return specificWay;
        }
        public FilteredCity(string filename) : base(filename)
        {

        }
    }
}