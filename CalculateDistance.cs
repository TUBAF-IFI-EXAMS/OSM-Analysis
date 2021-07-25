/*
Aufbauend auf den vorherigen Tests soll diese Klasse das Berechnen der Länge eines Straßennetzes übernehmen. 
*/

using System;
using System.IO;
using System.Linq;
using OsmSharp;
using OsmSharp.Streams;


public class CalculateDistance
{
// ---------- | Felder | ----------

public const double d_lat = 111.0;                         // konstanten Abstand zwischen Längengraden
const double erdradius = 6378.388;                          // konstanter Erdradius in km 

// ---------- | Konstruktor | ----------

public CalculateDistance(){}

// ---------- | Methoden | ----------

// Hilfsmethode zum Umwandeln einer Gradzahl ins Bogenmaß
public double DegToRad(double angle) => angle * Math.PI/180;

// Methode zur Berechnung der (kürzensten) Entfernung zweier Knoten mittels Pythagoras und interpoliertem Abstand zwischen Längengraden:
public double EstimateDistancePythagoras(Node Node1, Node Node2)
{

// Variablen :

    double dx, dy;                                                  // Abstände P1 <--> P2 in km in kartesischen Koordinaten 
    double d_lon;                                                   // variabler Abstand zweier Längengrade

    double lat1 = Node1.Latitude ?? 0.0D;                           // geographische Breite Knoten 1                        (?? da laut OsmSharp Nullable Ausdrücke)
    double lon1 = Node1.Longitude ?? 0.0D;                          // geographische Länge Knoten 1
    double lat2 = Node2.Latitude ?? 0.0D;                           // geographische Breite Knoten 2
    double lon2 = Node2.Longitude ?? 0.0D;                          // geographische Länge Knoten 2

    double abstand;                                                 // kürzester Abstand der beiden Knoten

    d_lon = 111* Math.Cos(DegToRad((lat1+lat2)/2));                 // Berechnung des Abstands zweier Längengrade [Konvertierung in Bogenmaß erforderlich]
    dx = d_lon * (lon2-lon1);                                       // Differenz der geographischen Längengrade
    dy = d_lat * (lat2-lat1);                                       // Differenz der geographischen Breitengrade
    abstand = Math.Sqrt(dx*dx+dy*dy);                               // Berechnung des Abstands mittels Satz des Pythagoras
    
    return abstand;    

}

public double EstimateDistanceSphericalCoordinates(Node Node1, Node Node2)
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
    Node Node1, Node2;                                              // aktuelle Knoten
    long NodeID1,NodeID2;                                           // aktuelle Knoten-IDs 
    
    NumberOfWays = net.Length;                                      // Ermittelt Anzahl der ways im Array
    
    for (int w = 0; w < NumberOfWays; w++)
    {
        currentWay = net[w];
       
        NumberOfNodes = currentWay.Nodes.Length;                    // Ermittelt Anzahl der Knoten im aktuellen Weg

        for (int n = 1; n < NumberOfNodes; n++)
        {
            NodeID1 = currentWay.Nodes[n-1];
            NodeID2 = currentWay.Nodes[n];

            var source = new XmlOsmStreamSource(File.OpenRead("Testmap.osm"));

            // Nodes aus nodeIDs auslesen
            
            Node1 = GetNodeFromNodeID(source,NodeID1);
            Node2 = GetNodeFromNodeID(source,NodeID2);
           
            /* Entscheidung, welcher Algorithmus genutzt werden soll: 
                    - Für Längengrade, welche betragsmäßig < 85° sind wird die vereinfachte Methode (Pythagoras mit interpoliertem d_lat) genutzt 
                    - Für Werte nahe den Polen ist die Nutzung eines näherungsweise rechteckigen Gitternetzes unzureichend --> Kugelkoordinaten
            */
            
           if ((Math.Abs(Node1.Latitude ??0.0D) > 85) | (Math.Abs(Node2.Latitude ??0.0D) > 85))
            {
                NetLength += EstimateDistanceSphericalCoordinates(Node1,Node2);

            }
            else
            {
                NetLength += EstimateDistancePythagoras(Node1,Node2);
            }

        }
        
    }

    NetLength = Math.Round(NetLength,3);                            // Rundet Ergebnis auf ganze Meter

    return NetLength;                                               // Gibt Netzlänge zurück

}

// Methode zum Auslesen eines vollständigen Node-Objekts aus der ID
private Node GetNodeFromNodeID(XmlOsmStreamSource source, long NodeID)
{

var Nodes = from osmGeo in source where osmGeo.Type == OsmGeoType.Node select osmGeo;       // filtert nach Objekt-Typ --> liefert nur Nodes
var searchedNode = from osmGeo in Nodes where osmGeo.Id == NodeID select osmGeo;            // filtert nach gesuchter NodeID

var searchedNodeComplete = searchedNode.ToComplete();                                       // wandelt NodeID in komplettes Node-Objekt um

Node nodeObject = (Node)searchedNodeComplete.ElementAt(0);                                  // Cast zum Node

return nodeObject;                                                                          // gibt Knoten zurück

}

}



