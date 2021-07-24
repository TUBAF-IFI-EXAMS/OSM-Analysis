using System;
using System.IO;
using OsmSharp.Streams;
using System.Threading.Tasks;
using System.Linq;
using OsmSharp;



namespace osm
{
    class Overpass
    {
        string adress = "Freiberg";
        public string path;
        public string filename;

        //Elemente zum Herunterladen (z.b Node,Way, Relation, * für alle type)
        public string elemente = "*";
        private OsmData osmData = new OsmData();

         
        public string ReturnURL()
        {
            osmData.SearchForAdress(adress);
            string url = $"http://www.overpass-api.de/api/xapi?{elemente}[bbox={osmData.Box.minLongitude},{osmData.Box.minLatitude},{osmData.Box.maxLongitude},{osmData.Box.maxLatitude}]";

            return url;
        }
        
        public async Task FilterData()
        {
            await using var fileStream = File.OpenRead(path);

            // create source stream.
            XmlOsmStreamSource source = new XmlOsmStreamSource(fileStream);

            // filter all power lines and keep all nodes.
            var filtered = from osmGeo in source
                           where osmGeo.Type == OsmSharp.OsmGeoType.Node ||
                                 (osmGeo.Type == OsmSharp.OsmGeoType.Way && osmGeo.Tags != null && osmGeo.Tags.Contains("power", "line"))
                           select osmGeo;



            // convert to complete stream.
            var complete = filtered.ToComplete();

            // enumerate result.
            foreach (var osmGeo in complete)
            {
                if (osmGeo.Type == OsmSharp.OsmGeoType.Way)

                {
                    // the nodes are still in the stream, we need them to construct the 'complete' objects.
                    Console.WriteLine(osmGeo.ToString());
                }
            }


            var filtered2 = from osmGeo in source
                            where osmGeo.Id % 100000 == 0 //  leave only objects with and id dividable by 100000.
                            select osmGeo;
            foreach (var osmGeo in filtered2)
            {
                Console.WriteLine(osmGeo.ToString());
            }

            ExtractNodesFromWay(source);

        }

        private void ExtractNodesFromWay(XmlOsmStreamSource source )
        {   

             var  extractways =(from os in source
                             where os.Type == OsmSharp.OsmGeoType.Way 
                             select os).Take(5);
            
            
            Way testway = (Way)extractways.ElementAt(1);

            var testwayToComplete = extractways.ToComplete();   
            
            long[] nodeIDs = testway.Nodes;

            //Extrahieren alle nodes in source deren Ids   nodeIDs entsprechen      
            var extractNodes = from s in source
                    where s.Type == OsmSharp.OsmGeoType.Node
                    join nid in nodeIDs on s.Id equals nid
                    select s;

            var nodesToComplete = extractNodes.ToComplete();
            
            Node[] nodes= new Node[nodesToComplete.Count()];

            for(int i = 0; i<nodesToComplete.Count();i++)
            {
                nodes[i] = (Node)nodesToComplete.ElementAt(i);
            }

            Console.WriteLine("Ergebniss");
            foreach (var osmGeo in nodes)
            {
                {
                    // Ausgabe 
                   Console.WriteLine(osmGeo.Latitude.ToString());
                }
            }
            
            
            
        }



    }

}





