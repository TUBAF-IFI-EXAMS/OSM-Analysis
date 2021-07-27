using System;
using System.Globalization;
using Nominatim.API.Models;
using Nominatim.API.Geocoders;

namespace src
{
    /// <summary>
    /// Klasse zur Suche nach eine Stadt 
    /// </summary>
    class OsmDataSearch
    {

        private BoundingBox box = new BoundingBox();  // Grenzen der zu suchende ort
        public string elementType = "*"; // representiert die delemente Type(Node, Way, relation und map(*))


        /// <summary>
        /// ForwardGeocoder gibt  die Möglichkeit, aus gegeben name(in unser fall Stadt)
        ///Information wie latitude/longitude, Grenzen zu erhalten. 
        /// </summary>
        private ForwardGeocoder forwardGeocoder = new ForwardGeocoder();

        public static GeocodeResponse localInfo; //Ergebnis aus der Suche Speichern

        public BoundingBox Box => box;


        /// <summary>
        /// Suchen nach eine  Stadt, um die Grenzen(BoundingBox) und alle wichtige information zu erhalten 
        /// </summary>
        public void SearchForAdress(string address)
        {

            var geocodeResponses = forwardGeocoder.Geocode(new ForwardGeocodeRequest
            {
                queryString = address,


                ShowExtraTags = true,
                ShowAlternativeNames = true,
                ShowKML = true,
                ViewboxBoundedResults = true,

            });

            // Suchergebniss
            var searchResults = geocodeResponses.Result[0];
            localInfo = searchResults;

            box = (BoundingBox)searchResults.BoundingBox;

        }

        /// <summary>
        ///   Die URL für die Heruterladen der Daten generieren 
        /// </summary>
        /// <returns>Es wird die URL zurückgegeben </returns>
        public string GetCityDataUrl()
        {

            string querybox = $"{box.minLongitude.ToString(CultureInfo.InvariantCulture)},{box.minLatitude.ToString(CultureInfo.InvariantCulture)},{box.maxLongitude.ToString(CultureInfo.InvariantCulture)},{box.maxLatitude.ToString(CultureInfo.InvariantCulture)}";
            Console.WriteLine(querybox);
            // Url zum Download der Daten 
            string url = $"http://www.overpass-api.de/api/xapi?*[bbox={querybox}]";
            Console.WriteLine(url);

            return url;
        }





    }
}