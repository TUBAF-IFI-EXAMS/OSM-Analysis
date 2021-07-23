using System;

// Klasse zur abstrakten Darstellung von geographischen Koordinaten
public class node
{

// Felder:

private static uint _nodeCounter = 1;            // Zählt Knoten

// Eigenschaften:

public string description { get; set; }         // Beschreibung des Punktes via String
public double lat { get; set; }                // Breitengrad des Punktes
public double lon { get; set; }                // Längengrad des Punktes

// Konstruktoren:

public node(string Description, double Latitude, double Longitude)
{
    description =   Description;
    lat         =   Latitude;
    lon         =   Longitude;
}

public node(double Latitude, double Longitude)
{
    description =   "P" + Convert.ToString(_nodeCounter);
    lat         =   Latitude;
    lon         =   Longitude;

    _nodeCounter += 1;
}
}

public class Entfernungsberechnung{

// Felder:

node _node1;
node _node2;

// Eigenschaften:

public string name { get; set; }

// Konstruktoren:

public Entfernungsberechnung(string Name, node Node1, node Node2){

name = Name;
_node1 = Node1;
_node2 = Node2;

}

public Entfernungsberechnung(node Node1, node Node2){

_node1 = Node1;
_node2 = Node2;

}

// ----- |Methode zur Berechnung (kürzensten) Entfernung zweier Knoten | -----
public double distance(uint method)
{

// Variablen:

    double dx, dy;                          // Abstände P1 <--> P2 in km in kartesischen Koordinaten 
    double d_lat, d_lon;                    // Abstand zweier Breiten- bzw. Längengrade

    double lat1 = _node1.lat;               // geographische Breite Knoten 1
    double lon1 = _node1.lon;               // geographische Länge Knoten 1
    double lat2 = _node2.lat;               // geographische Breite Knoten 2
    double lon2 = _node2.lon;               // geographische Länge Knoten 2

    double laenge;                          // kürzester Abstand der beiden Knoten

    double erdradius = 6378.388;            // konstanter Erdradius in km

    /* 
    Auswahl der Berechnungsmethode:
            1 - einfache Berechnung mittels Pythagoras
            2 - erweiterte Berechnung (planare Trigonometrie mit variablem Abstand der Längengrade)
            3 - Berechnung mittels Kugelkoordinaten
    */

    
    switch (method)
    {
        case 1: 

// Methode mittels planarer Trigonometrie (Satz des Pythagoras)

            d_lat = 111;                                        // fester Wert (konstant da "perfekte" Kugel)
            d_lon = 71.5;                                       // fester Wert (angepasst auf deutsche Längengrade)

            dx = d_lon * (lon2-lon1);                           // Differenz der geographischen Längengrade
            dy = d_lat * (lat2-lat1);                           // Differenz der geographischen Breitengrade
            laenge = Math.Sqrt(dx*dx+dy*dy);                    // Berechnung des Abstands mittels Satz des Pythagoras
            break;

        case 2:

// erweiterte Methode mit variablem Abstand der Längengrade

            d_lat = 111;                                        // Abstand der Breitengrade weiterhin konstant
            d_lon = 111* Math.Cos(DegToRad((lat1+lat2)/2));     // Berechnung des Abstands zweier Längengrade [Konvertierung in Bogenmaß erforderlich]
            dx = d_lon * (lon2-lon1);                           // Differenz der geographischen Längengrade
            dy = d_lat * (lat2-lat1);                           // Differenz der geographischen Breitengrade
            laenge = Math.Sqrt(dx*dx+dy*dy);                    // Berechnung des Abstands mittels Satz des Pythagoras
            break;
        
        case 3:

// Berechnung der Länge als Großkreisbogen mittels Kugelkoordinaten 

            lon1 = DegToRad(lon1);                              //                     |        
            lat1 = DegToRad(lat1);                              //                     |
                                                                // Umwandlung in Bogenmaß für Kosinussatz                    
            lon2 = DegToRad(lon2);                              //                     |   
            lat2 = DegToRad(lat2);                              //                     |

            laenge = erdradius * Math.Acos(Math.Sin(lat1)*Math.Sin(lat2)+Math.Cos(lat1)*Math.Cos(lat2)*Math.Cos(lon2-lon1));

            break;

        default: 
            throw new ArgumentException("Invalid method! Choose either 1, 2 or 3!");
            
    }

    return laenge;
    // Console.WriteLine($"Methode {method}: Entfernung zwischen {_node1.description} und {_node2.description}: { laenge } km");   
}
    public double DegToRad(double angle) => angle * Math.PI/180;
}


