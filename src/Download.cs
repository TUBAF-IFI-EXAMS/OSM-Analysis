using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OSM_Analysis
{
    /// <summary>
    /// Daten aus Overpass API Herunterladen
    /// </summary>
    public static class Download
    {
        /// <summary>
        /// Daten Herunterladen falls Datei nicht exiestiert
        /// </summary>
        public static async Task ToFile(string url, string filename)
        {
            if (!File.Exists(filename))
            {
                var client = new HttpClient();
                await using var stream = await client.GetStreamAsync(url);
                await using var outputStream = File.OpenWrite(filename);
                await stream.CopyToAsync(outputStream);
            }
        }
    }
}