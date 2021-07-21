using System;

// Test der unterschiedlichen Berechnungsmodelle zur Bestimmung des Abstands zweier Punkte auf der Erdoberfläche

    public class Program
    {

        static void Main(string[] args)
        {
        
            // Berechnungsmodell:
            
            // Punkt 1: Universitätshauptgebäude TUBAF (Quelle: Google Maps)
            double lat1 =  50.9181009;
            double lon1 =  13.3409753;

            // Punkt 2: Universitätshauptgebäude TU Chemnitz (Quelle: Google Maps)
            double lat2 =  50.8398942; 
            double lon2 =  12.9278649;  

            // Punkt 3: Haus Metallkunde(Quelle: Openstreetmap.org)
            double lat3 = 50.9243187;
            double lon3 = 13.3316376;

            // Punkt 4: Lampadiusbau (Quelle: Openstreetmap.org)
            double lat4 = 50.9247312;
            double lon4 = 13.3302340;

            // Punkt 5: Oxford University (Quelle: Openstreetmap.org)
            double lat5 = 51.7589021;
            double lon5 = -1.2536706;

            // Anlegen neuer Knoten für die fünf Messpunkte:

            node Node_TUBAF = new node("TUBAF",lat1,lon1);
            node Node_TUCH = new node("TU-Chemnitz",lat2,lon2);
            node Node_Metallkunde = new node("Haus Metallkunde",lat3,lon3);
            node Node_Lampadiusbau = new node("Lampadiusbau",lat4,lon4);
            node Node_Oxford = new node("Oxford University", lat5, lon5);

            var Entfernung_kurz = new Entfernungsberechnung("kurze Strecke",Node_Metallkunde,Node_Lampadiusbau);     
            var Entfernung_mittel = new Entfernungsberechnung("mittlere Strecke", Node_TUBAF,Node_TUCH);
            var Entfernung_lang = new Entfernungsberechnung("lange Strecke", Node_TUBAF,Node_Oxford);

            Entfernungsberechnung[] Strecken = new Entfernungsberechnung[3]{Entfernung_kurz, Entfernung_mittel, Entfernung_lang};
            
            foreach (Entfernungsberechnung Strecke in Strecken){
            Console.WriteLine($"{Strecke.name}: ");
            for (uint model = 1; model<=3; model++){
                Strecke.distance(model);    
            }
            }
           
        }

    }
