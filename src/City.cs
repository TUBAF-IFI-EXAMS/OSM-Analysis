using System;
using OsmSharp;
using System.Linq;
using OsmSharp.Streams;
using System.Collections.Generic;

namespace src
{
    class City : LoadData
    {
       
        private int postalcode;
        private int population;
        private double wayDistance;
        public int Population()
        {

            foreach (KeyValuePair<string, string> extratags in OsmDataSearch.localInfo.ExtraTags)
            {
                if (extratags.Key == "population")
                {
                    population = int.Parse(extratags.Value);

                }
            }
            

            return population;

        }
        public string Name{get; private set;}

        public string Country{ get; private set;}
       
        public string PostalCode{get; private set;}
        public Way[] GetAllWays()
        {

           
            var wayTemp = (from osmWays in Source()
                           where osmWays.Type == OsmSharp.OsmGeoType.Way
                           select osmWays).ToArray();

            Way[] allWays = new Way[wayTemp.Length];

            Console.WriteLine(wayTemp.Length);

            for (int i = 0; i < allWays.Length; i++)
            {
                allWays[i] = (Way)wayTemp[i];
                Console.WriteLine(allWays[i].Id);
            }

            return allWays;

        }


        public City(string fileName) : base(fileName)
        {
            string[] extractInfo = OsmDataSearch.localInfo.DisplayName.Split(",");
            Country = extractInfo.Last();
            Name = extractInfo.First();

            foreach(var extract in extractInfo)
            {
                if(int.TryParse(extract,out postalcode))    // Identifikation der PLZ durch TryParse (int)
                {
                    PostalCode = extract;                   // gibt string zurück, da führende Nullen in Integer nicht erfasst werden
                    break;    
                }
                else{
                    PostalCode="Keine PLZ im Datensatz";    // Sonderfall: keine PLZ hinterlegt
                }

            } 
        }

    }
}