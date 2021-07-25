using System.IO;
using System.Threading.Tasks;
using OsmSharp.Streams;

namespace OSM_Analysis
{
    /// <summary>
    /// Class zum Laden der zu analysierende Daten
    /// </summary>
    class LoadData
    {
        private string fileName;
        private FileStream fileStream;
        private OsmStreamSource source;
    
        public LoadData(string fileName)
        {
            this.fileName = fileName;
        }


        private  OsmStreamSource  Source()
        {
            // Überprüfen ob datei vorhanden ist 
            if (File.Exists(fileName))
            {
                
                this.fileStream = File.OpenRead(fileName);

                // Überprufen ob der Datei die in der OsmStreamSource vorhandenen 
                // Extension unterstützt!

                if(Path.GetExtension(fileName)==".xml")
                {
                    return  this.source = new XmlOsmStreamSource(this.fileStream);
                }

                else if(Path.GetExtension(fileName)==".osm.pbf") 
                {
                    return  this.source = new PBFOsmStreamSource(this.fileStream);
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