using System.IO;
using System.Threading.Tasks;
using OsmSharp.Streams;

namespace OSM_Analysis
{
    /// <summary>
    /// Daten für die Analyse laden
    /// </summary>
    class LoadData
    {
        private string fileName;
        private FileStream fileStream;
        private XmlOsmStreamSource source;
        public LoadData(string fileName)
        {
            this.fileName = fileName;
        }

        private  XmlOsmStreamSource  Source()
        {
            // Überprüfen ob datei vorhanden ist 
            if (!File.Exists(fileName))
            {
                this.fileStream = File.OpenRead(fileName);
                return  this.source = new XmlOsmStreamSource(fileStream);
                
            }
            else
            {
                
                throw new FileNotFoundException();
            }
         
        }
        


    }
}