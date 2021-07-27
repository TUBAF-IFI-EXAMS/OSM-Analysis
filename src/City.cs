using System;
using OsmSharp;
using System.Linq;
using OsmSharp.Streams;
using System.Collections.Generic;

namespace src
{
    /// <summary>
    /// Klasse die Eine Stadt(Objekt) representiert
    /// </summary>
    class City : LoadData
    {

        private int postalcode;
        private int population;
        private double wayDistance;


        /// <summary>
        ///    Anzahl der Einwohner 
        /// </summary>
        /// <returns>Als Rückgabewert ist die ANzahl der Einwohner </returns>
        public int Population()
        {

            /// <summary>
            ///   OsmDataSearch.localInfo.ExtraTags gibt eine Collection zurück
            ///  Die aus viele  Infos besteht, aus dieser wird nur die  Anzahl der Einwohner ////extrahiert
            /// </summary>

            foreach (KeyValuePair<string, string> extratags in OsmDataSearch.localInfo.ExtraTags)
            {
                if (extratags.Key == "population")
                {
                    population = int.Parse(extratags.Value);

                }
            }


            return population;

        }
        public string Name { get; private set; }

        public string Country { get; private set; }

        public string PostalCode { get; private set; }



        /// <summary>
        ///    Versuchen alle Wege in einer Stadt aus der "Datenbank" zu lesen 
        /// </summary>
        /// <returns>Als Rückgabewert sind Alle Wege in der Stadt </returns>
        public Way[] GetAllWays()
        {

            // Wege au der Datenbank filtern (OSMGEO Type )
            var wayTemp = (from osmWays in Source()
                           where osmWays.Type == OsmSharp.OsmGeoType.Way
                           select osmWays).ToArray();

            Way[] allWays = new Way[wayTemp.Length];

            Console.WriteLine(wayTemp.Length);

            // COnvertieren Wegarray von OSMGEO format  in Way-Type
            for (int i = 0; i < allWays.Length; i++)
            {
                allWays[i] = (Way)wayTemp[i];
                Console.WriteLine(allWays[i].Id);
            }

            return allWays;

        }

        /// <summary>
        ///     Konstruktor 
        /// </summary>
        /// <param name="filename"></param>
        public City(string fileName) : base(fileName)
        {

            /// <summary>
            ///  OsmDataSearch.localInfo.DisplayName liefert eine  lange string rüruck
            /// Aus dieser wird die PLZ, Name der Stadt und Zugehöriges Land extrahiert.
            /// </summary>

            string[] extractInfo = OsmDataSearch.localInfo.DisplayName.Split(",");

            Country = extractInfo.Last();

            Name = extractInfo.First();

            foreach (var extract in extractInfo)
            {
                if (int.TryParse(extract, out postalcode))    // Identifikation der PLZ 
                {
                    PostalCode = extract;                   // gibt string zurück, da führende Nullen in Integer nicht erfasst werden
                    break;
                }
                else
                {
                    PostalCode = "Keine PLZ im Datensatz";    // Sonderfall: keine PLZ hinterlegt
                }

            }
        }

    }
}