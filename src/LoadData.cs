using System.IO;
using OsmSharp.Streams;

namespace src
{
    /// <summary>
    /// Klasse zum Laden der zu analysierende Daten
    /// </summary>
    class LoadData
    {

        private string fileName;  // Dateiname
        private FileStream fileStream; //Stream zum öffnen und Lesen eine Datei
        private OsmStreamSource source; // Datenquelle



        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="filename"></param>
        public LoadData(string fileName)
        {
            this.fileName = fileName;
        }



        /// <summary>
        ///    Versuchen eine Datei zu öffnen und lesen 
        /// </summary>
        /// <returns>Es wird eine StreamSource in  xml oder pbf Format zurückgegeben falls das  öffnen, lesen erfolgreich ist </returns>
        protected OsmStreamSource Source()
        {

            /// <summary>
            /// Überprüfen ob datei vorhanden ist 
            /// <summary>

            if (File.Exists(fileName))
            {

                this.fileStream = File.OpenRead(fileName);

                /// <summary>
                /// Überprüfen, ob der Datei die in der OsmStreamSource vorhandenen 
                // Extension unterstützt!
                /// <summary>

                if (Path.GetExtension(fileName) == ".xml")
                {
                    return source = new XmlOsmStreamSource(fileStream);        // Dateiendung .xml [xml]
                }

                else if (Path.GetExtension(fileName) == ".pbf")                    // Dateiendung .osm.pbf [binary]
                {
                    return source = new PBFOsmStreamSource(fileStream);
                }

                else
                {
                    throw new InvalidDataException();
                }
            }
            else
            {

                throw new FileNotFoundException();
            }

        }



    }
}