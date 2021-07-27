# OSM-Analysis

## OSM - Was ist das ?

OSM, eine Abkürzung für _**O**pen**S**treet**M**ap_, ist ein im Jahre 2004 gegründetes Projekt, welches das Ziel hat eine lizenzfrei verfügbare Weltkarte zu erschaffen [1]. Die zugrundeliegende Geodatenbank wird von Projektmitgliedern stetig ergänzt und aktualisiert und steht jedem Nutzer sowohl in Form von gerenderten Karten als auch in Form der Rohdaten frei zur Verfügung.

### Elemente einer OSM-Datei

Elemente wie Straßen, Häuser, Oberleitungen, Flüsse usw. werden in OSM aus den einfachen Grundelementen _node_ (Punkt), _way_ (Linie) , _area_ (Fläche) sowie _Relationen_ zwischen diesen Elementen aufgebaut [2].  
Diese Elemente besitzen Attribute bestehend aus einem _key_ (Schlüssel) und einem _value_ (Wert).

## OSM - Was haben wir damit vor ? 

Ziel unseres Projektes ist die Analyse von Straßendaten aus der OSM-Datenbank. Ausgangspunkt hierfür ist eine Filterung des Datensatzes innerhalb von C# mit dem Ziel, lediglich die Knoten (_nodes_) und Verbindungslinien (_ways_) zu extrahieren. Hierbei wird eine Abgrenzung der Daten nach Städten angestrebt. Darauf aufbauend sollen die Daten dazu genutzt werden, um die Länge des kompletten Straßennetzes innerhalb einer Stadt (als Beispiel dient die Stadt Freiberg/Sa) zu ermitteln.  
Sofern die verfügbaren Daten dies hergeben, soll eine Unterscheidung der _ways_ in Bezug auf die Befahrkeit bzw. Begehbarkeit stattfinden.  
Darüber hinaus planen wir eine grafische Darstellung des analysierten Straßennetzes unter Verwendung verfügbarere Renderer.

## Wie soll dies umgesetzt werden?

Zum Arbeiten mit OSM-Daten ist ein Verständnis für deren Grundlagen notwendig. Hier besteht die erste Aufgabe darin, den Aufbau von OSM-Dateien zu verstehen und zu dokumentieren. Danach werden die einzelnen Funktionalitäten gemäß des [Stufenplans](https://github.com/DarioDagne/OSM-Analysis/wiki/Zeitplanung-&-Aufgabenteilung) umgesetzt.

Thematisch müssen dabei folgende Probleme gelöst werden:
* Herunterladen eines OSM-Datensatzes
* Filtern der Daten nach gewünschten Informationen
* Berechnung der Länge einer Straße anhand einer Liste geografischer Koordinaten

Die einzelnen Punkte sind in diesem Wiki ausführlich erklärt und mit der nötigen Theorie hinterlegt worden. 
Nähere Informationen zur [Datenfilterung und -gewinnung](https://github.com/DarioDagne/OSM-Analysis/wiki/Datenfilterung-und-Datengewinnung) und zur [Berechnung der Straßenlänge](https://github.com/DarioDagne/OSM-Analysis/wiki/Berechnung-der-Stra%C3%9Fenl%C3%A4nge-aus-geographischen-Koordinaten) können in den jeweiligen Unterseiten gefunden werden.

## Und wozu das Ganze ?

Aus reinem Interesse an dem Ergebnis.   
Könnte man in einer Kleinstadt wie Freiberg einen Straßen-Marathon laufen ohne eine Straße doppelt laufen zu müssen?   
Wie viele Kilometer Straße wurden in Freiberg pro Einwohner erbaut?   
Wie groß ist der Anteil der für Radfahrer geeigneten Wege in unserer Studentenstadt?  
Wir wissen es nicht aber mit unserem kleinen Beitrag zur OSM-Analyse können wir den Antworten auf diese Fragen näher kommen.  
Ganz nebenbei hilft uns dieses Projekt dabei, die Grundideen der Softwareentwicklung im Team praktisch zu testen und gleichzeitig unseren Wissenshorizont bezüglich dem Aufbau und der Verarbeitung von Geodaten zu erweitern.



***
### Quellen:   
[1] - FAQ: Was ist OpenStreetMap?, Abgerufen 06. Juli 2021 von https://www.openstreetmap.de/faq.html#was_ist_osm  
[2] - DE:Elemente, Abgerufen 13. Juli 2021 von https://wiki.openstreetmap.org/wiki/DE:Elemente  
[3] - Geographische Koordinaten, Abgerufen 21. Juli 2021 von https://de.wikipedia.org/wiki/Geographische_Koordinaten  
[4] - Martin Kompf - Entfernungsberechnung, Abgerufen 21. Juli 2021 von https://www.kompf.de/gps/distcalc.html  
[5] - Node, Abgerufen 21. Juli 2021 von https://wiki.openstreetmap.org/wiki/Node  
[6] - Geographische Breite - Abgerufen 21. Juli 2021 von https://de.wikipedia.org/wiki/Geographische_Breite  
[7] - Geographische Länge - Abgerufen 21. Juli 2021 von https://de.wikipedia.org/wiki/Geographische_L%C3%A4nge