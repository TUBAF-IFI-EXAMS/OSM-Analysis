using System;


    public class Program
    {

        static void Main(string[] args)
        {
        
            // Berechnungsmodell:
            uint model = 1;
            // Punkt 1: Universitätshauptgebäude TUBAF (Quelle: Google Maps)
            double lat1 =  50.9181009;
            double lon1 =  13.3409753;
            // Punkt 2: Universitätshauptgebäude TU Chemnitz (Quelle: Google Maps)
            double lat2 =  50.8398942; 
            double lon2 =  12.9278649;  

            // Anlegen neuer Knoten für die beiden Messpunkte:

            node Node_TUBAF = new node("TUBAF",lat1,lon1);
            node Node_TUCH = new node("TU-Chemnitz",lat2,lon2);
            
            var calc = new Entfernungsberechnung(Node_TUBAF,Node_TUCH);
            calc.distance(model);
            

        }

    }
