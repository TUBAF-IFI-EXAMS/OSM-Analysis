using System;
using System.Linq;
using OsmSharp;
using OsmSharp.Tags;



namespace src
{
    class FilteredCity : City
    {
        /// <summary>
        ///   Aus der Datenbank extrahieren Wege die bestimmt eingenschaft erfüllen
        ///Zum Beispiel(Fußwege, Auto,Fahrad u usw.)
        /// </summary>
        /// <returns>Es wird die Alle wege gleiche Eigenschaft zurückgegeben </returns>
        public Way[] FilterWays(string waytype, string highway = "highway")
        {

            Tag tag = new Tag(); // Zu suchende Weg spezifizieren 
            tag.Key = highway;
            tag.Value = waytype;

            // Wege aus der Datenbank filtern OSMGEO Type
            var waysToFilter = (from osmGeo in Source()
                                where osmGeo.Type == OsmSharp.OsmGeoType.Way &&
                                 (osmGeo.Tags != null && osmGeo.Tags.Contains(tag))
                                select osmGeo).ToArray();

            Way[] filteredWays = new Way[waysToFilter.Length];

            // COnvertieren Wegarray von OSMGEO format  in Way-Type
            for (int i = 0; i < filteredWays.Length; i++)
            {
                filteredWays[i] = (Way)waysToFilter[i];

            }


            return filteredWays;

        }


        /// <summary>
        ///   Aus der Datenbank extrahieren einen bestimmten Weg
        ///    In der Datenbank  ein Weg ist in Wegstück unterteilt
        /// </summary>
        /// <returns>Es wird ein Weg zurückgegeben </returns>
        public Way[] GetspecificWay(string wayname, string key = "name")
        {


            Tag tag = new Tag(); // Zu suchende Weg spezifizieren 
            tag.Key = key;
            tag.Value = wayname;

            // Weg aus der Datenbank filtern OSMGEO Type
            var waysToFilter = (from osmGeo in Source()
                                where osmGeo.Type == OsmSharp.OsmGeoType.Way &&
                                 (osmGeo.Tags != null && osmGeo.Tags.Contains(tag))
                                select osmGeo).ToArray();


            Way[] specificWay = new Way[waysToFilter.Length];
            // COnvertieren Wegarray von OSMGEO format  in Way-Type
            for (int i = 0; i < specificWay.Length; i++)
            {
                specificWay[i] = (Way)waysToFilter[i];
            }

            return specificWay;
        }

        public FilteredCity(string filename) : base(filename)
        {

        }
    }
}