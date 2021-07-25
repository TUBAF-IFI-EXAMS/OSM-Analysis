using System;
using OsmSharp;
namespace OSM_Analysis
{
    class City :LoadData
    {
        private string name;
        private int postcode;
        private int population;
        private string country;
        private double wayDistance;


        /*        
        public Way[] GetAllWays()
        {

        }
        */
        
        public City(string fileName) : base( fileName)
        {

        }

    }
}