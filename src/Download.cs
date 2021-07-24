using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace osm
{
    /// <summary>
    /// Contains code to download test files.
    /// </summary>
    public static class Download
    {
        /// <summary>
        /// Downloads a file if it doesn't exist yet.
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