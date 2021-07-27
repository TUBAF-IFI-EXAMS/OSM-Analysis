/*
Aufbauend auf den vorherigen Tests soll diese Klasse das Berechnen der Länge eines Straßennetzes übernehmen. 
*/

using System;
using System.IO;
using System.Linq;
using OsmSharp;
using OsmSharp.Streams;

namespace src
{

    class CalculateDistance : LoadData                                  // Klasse erbt von LoadData --> Source
    {
    // ---------- | Felder | ----------

    const double d_lat = 111.0;                                         // konstanten Abstand zwischen Längengraden
    const double erdradius = 6378.388;                                  // konstanter Erdradius in km 

    // ---------- | Konstruktor | ----------

    public CalculateDistance(string filename):base (filename){}

    // ---------- | Methoden | ----------

    // Hilfsmethode zum Umwandeln einer Gradzahl ins Bogenmaß
    double DegToRad(double angle) => angle * Math.PI/180;

    // Methode zur Berechnung der (kürzensten) Entfernung zweier Knoten mittels Pythagoras und interpoliertem Abstand zwischen Längengraden:
    double EstimateDistancePythagoras(Node Node1, Node Node2)
    {

    // Variablen :

        double dx, dy;                                                  // Abstände P1 <--> P2 in km in kartesischen Koordinaten 
        double d_lon;                                                   // variabler Abstand zweier Längengrade

        double lat1 = Node1.Latitude ?? 0.0D;                           // geographische Breite Knoten 1           (?? da laut OsmSharp Nullable Ausdrücke)
        double lon1 = Node1.Longitude ?? 0.0D;                          // geographische Länge Knoten 1
        double lat2 = Node2.Latitude ?? 0.0D;                           // geographische Breite Knoten 2
        double lon2 = Node2.Longitude ?? 0.0D;                          // geographische Länge Knoten 2

        double abstand;                                                 // kürzester Abstand der beiden Knoten

        d_lon = 111* Math.Cos(DegToRad((lat1+lat2)/2));                 // Berechnung des Abstands zweier Längengrade [Konvertierung in Bogenmaß erforderlich]
        Console.WriteLine(d_lon);
        dx = d_lon * (lon2-lon1);                                       // Differenz der geographischen Längengrade
        dy = d_lat * (lat2-lat1);                                       // Differenz der geographischen Breitengrade
        abstand = Math.Sqrt(dx*dx+dy*dy);                               // Berechnung des Abstands mittels Satz des Pythagoras
        //Console.WriteLine(abstand);
        return abstand;    

    }

    double EstimateDistanceSphericalCoordinates(Node Node1, Node Node2)
    {

        double lat1 = Node1.Latitude ?? 0.0D;                           // geographische Breite Knoten 1                        (?? da laut OsmSharp Nullable Ausdrücke)
        double lon1 = Node1.Longitude ?? 0.0D;                          // geographische Länge Knoten 1
        double lat2 = Node2.Latitude ?? 0.0D;                           // geographische Breite Knoten 2
        double lon2 = Node2.Longitude ?? 0.0D;                          // geographische Länge Knoten 2

        double abstand;                                                 // kürzester Abstand der beiden Knoten

        lon1 = DegToRad(lon1);                                          //                     |        
        lat1 = DegToRad(lat1);                                          //                     |
                                                                        // Umwandlung in Bogenmaß für Kosinussatz                    
        lon2 = DegToRad(lon2);                                          //                     |   
        lat2 = DegToRad(lat2);                                          //                     |

        // Berechnung des Abstands mittels Kosinusseitensatz (Quelle: https://www.kompf.de/gps/distcalc.html):

        abstand = erdradius * Math.Acos(Math.Sin(lat1)*Math.Sin(lat2)+Math.Cos(lat1)*Math.Cos(lat2)*Math.Cos(lon2-lon1));

        return abstand;    

    }

    // Methode zur Berechnung der Länge eines (Straßen)Netzes:
    public double CalculateNetLength(Way[] net)
    {
        double NetLength = 0.0;                                         // Netzlänge in Kilometern (zu Beginn mit 0 km initialisiert)

        int NumberOfWays;                                               // Anzahl der ways im gegebenen Netz
        int NumberOfNodes;                                              // Anzahl der Knoten für einen Weg

        Way currentWay;                                                 // aktueller Weg
        NumberOfWays = net.Length;                                      // Ermittelt Anzahl der ways im Array

        for (int w = 0; w < NumberOfWays; w++)
        {
            currentWay = net[w];
            
            NumberOfNodes = currentWay.Nodes.Length;                    // Ermittelt Anzahl der Knoten im aktuellen Weg

            Console.WriteLine($"Weg: {w}: {NumberOfNodes} nodes");      // Ausgabe für Testzwecke

            long[] NodeArray = currentWay.Nodes;                        // Extrahiert Liste der Knoten-IDs
            
            for (int j = 0; j<NodeArray.Length; j++){
                Console.WriteLine($"Node {j+1}: ID: {NodeArray[j]}");
            }

            Node[] WayNodes = GetNodesFromIDs(NodeArray);               // Liefert vollständige Knoten 
            

            for (int n=1; n<NumberOfNodes; n++){
                
                /*
                Entscheidung, welcher Algorithmus genutzt werden soll: 
                        - Für Längengrade, welche betragsmäßig < 85° sind wird die vereinfachte Methode (Pythagoras mit interpoliertem d_lat) genutzt 
                        - Für Werte nahe den Polen ist die Nutzung eines näherungsweise rechteckigen Gitternetzes unzureichend --> Kugelkoordinaten
                */
                
                

            if ((Math.Abs(WayNodes[n-1].Latitude ??0.0D) > 85) || (Math.Abs(WayNodes[n].Latitude ??0.0D) > 85))
                {
                    NetLength += EstimateDistanceSphericalCoordinates(WayNodes[n-1],WayNodes[n]);

                }
                else
                {
                    NetLength += EstimateDistancePythagoras(WayNodes[n-1],WayNodes[n]);
                    Console.WriteLine($"{n}. Knoten, länge = {NetLength} km");
                }

            }

            
        }

        NetLength = Math.Round(NetLength,3);                            // Rundet Ergebnis auf ganze Meter

        return NetLength;                                               // Gibt Netzlänge zurück

    }

    // Methode zum Auslesen einer Liste vollständiger Node-Objekte aus IDs

    private Node[] GetNodesFromIDs(long[] NodeID)
    {
        Console.WriteLine("Starte Datenauslesung");                                   // Ausgabe zur Zeitmessung  
            
                var extractNodes = (from s in Source()
                        where s.Type == OsmSharp.OsmGeoType.Node
                        join nid in NodeID on s.Id equals nid
                        select s).ToArray();                                         // Extrahieren alle nodes in source deren Ids nodeIDs entsprechen 
        Console.WriteLine("nodecast");

                Node[] nodes= new Node[extractNodes.Length];
                for(int i = 0; i<extractNodes.Length;i++)
                {   
                    nodes[i] = (Node)extractNodes[i];                               // Cast zu Nodes
                    Console.WriteLine($"NodeID nach cast: {i}: ID: {nodes[i].Id}");
                }

                Node[] sortedNodes = new Node[extractNodes.Length];                 // Umsortieren der ausgelesenen Knoten --> müssen mit Reihenfolge im way übereinstimmen

                for (int j=0; j<NodeID.Length; j++){
                for (int k=0; k<nodes.Length; k++){
                
                if(nodes[k].Id == NodeID[j]){
                    sortedNodes[j]=nodes[k];
                }

                }}

        Console.WriteLine("starte Rechnung");

        return sortedNodes;                                                               // Gibt Array von Nodes zurück
    }

    
}    
}



