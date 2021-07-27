## Stufenplan

Die in der [Einführung](https://github.com/DarioDagne/OSM-Analysis/wiki/Einf%C3%BChrung) gelisteten Zielstellungen sind im Rahmen der verfügbaren Zeit nicht vollständig umsetzbar. Daher soll der nachfolgende Plan eine Übersicht über die Prioritätenreihenfolge der Aufgaben geben:

**Stufe 1 (Minimalziel):**
- [x] Extrahieren von Straßen aus gegebenen _OSM_-Daten  
    - [x] Herunterladen von _OSM_-Daten (unter Verwendung von Nominatim bzw OsmSharp)
    - [x] Filtern der Daten (nur Straßen einlesen)
- [x] Berechnung der Länge eines Straßennetzes  
    - [x] Test möglicher Berechnungsmodelle zur Bestimmung des Abstands zweier Koordinaten
    - [x] Berechnung der Länge eines Polygonzugs (Testknoten)
    - [x] Nutzung extrahierter _OSM_-Daten zur Berechnung der Länge einer Straße
    - [x] Schleife über alle Straßen eines gewählten Bereichs
- [x] Daten aus Stadt extrahieren (PLZ, Einwohner, Bundesland)

**Stufe 2 (optional):**
- [ ] Erweiterte Filterung der Daten
    - [ ] Unterteilung der Straßen nach Befahrbarkeit (Auto/Fußweg...) mittels **highway=**-_key_
    - [x] Abgrenzung von Städten mittels relation
        - [ ] Ermittlung der Einwohnerzahl für Vergleich zwischen mehreren Städten
- [ ] Auslesen der OSM-Daten mittels OsmApiClient (kein lokaler Download notwendig)
- [ ] Nutzereingabe zum Vergleich mehrerer Datensätze

**Stufe 3 (Zusatzfeatures, werden nur bei übrig gebliebener Zeit umgesetzt):**
- [ ] GUI (Windows-Forms-Anwendung) zum Plotten des Straßennetzes und zur Darstellung der Daten

**Stufe 4 (weiterführende Ideen, welche momentan nicht umgesetzt werden):**
- [ ] Berechnung der Fläche einer Stadt (klare Abgrenzung notwendig)
- [x] Berechnung anderer _ways_ (Stromnetze, Flüsse etc. )

## Aufgabenteilung (Hauptzuständigkeiten)

| Dario D.                                                                  | Dario J.                       |
|---------------------------------------------------------------------------|--------------------------------|
| Mathematische Umsetzung (Berechnung der Straßennetzlänge aus Knotenliste) | Datenfilterung                  |
| Wiki                                                                      | Datengewinnung                 |