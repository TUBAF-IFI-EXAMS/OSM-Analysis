![Logo: Osm-Analysis](https://github.com/DarioDagne/OSM-Analysis/blob/main/images/Bild-Readme-Logo.png)
# Projekt: OSM-Analysis

Dario Tome Jonaca  
Dario Leon Dagné

## Kurzbeschreibung

Dieses Projekt ermöglicht das **Herunterladen, Filtern** und **Auswerten** von **Geodaten aus der OpenStreetMap-Datenbank**. 
Hierfür wurde ein Gesamtkonzept entwickelt, welches unter Verwendung der bestehenden Implementierungen _OsmSharp_, _Nominatim_ und _OverPassAPI_ den Datensatz einer Stadt im _.xml_- oder _.osm.pbf_-Format herunterlädt und anschließend die darin enthaltenen Wege (_ways_) extrahiert. Eine **Filterung** ermöglicht das Laden des Datensatzes einer bestimmten Straße nach deren Name oder das Laden eines _way_-Typen (z.B. Radwege, Schnellstraßen usw.). Zum gewählten Datensatz werden Informationen über die Stadt ausgegeben (Bundesland, PLZ, Einwohnerzahl), ein selbstprogrammierter Algorithmus berechnet die **Länge einer Straße** / eines Straßennetzes. Der Aufbau der _OSM_-Daten macht zudem eine Verwendung dieser Implementierung für weitere _ways_ möglich. Dies kann z.B. zur Auswertung von Stromleitungen oder Flüssen genutzt werden.

## Berechnung der Länge eines Straßennetzes

Implementierung der [hier](https://github.com/DarioDagne/OSM-Analysis/wiki/Berechnung-der-Stra%C3%9Fenl%C3%A4nge-aus-geographischen-Koordinaten) aufgeführten Theorie zur Berechnung der Entfernung zweier Knoten.
