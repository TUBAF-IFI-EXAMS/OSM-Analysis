![Logo: Osm-Analysis](https://github.com/DarioDagne/OSM-Analysis/blob/main/images/Bild-Readme-Logo.png)
# Projekt: OSM-Analysis

Dario Tome Jonaca  
Dario Leon Dagné

## Kurzbeschreibung

Dieses Projekt ermöglicht das **Herunterladen, Filtern** und **Auswerten** von **Geodaten aus der OpenStreetMap-Datenbank**. 
Hierfür wurde ein Gesamtkonzept entwickelt, welches unter Verwendung der bestehenden Implementierungen _OsmSharp_, _Nominatim_ und _OverPassAPI_ den Datensatz einer Stadt im _.xml_- oder _.osm.pbf_-Format herunterlädt und anschließend die darin enthaltenen Wege (_ways_) extrahiert. Eine **Filterung** ermöglicht das Laden des Datensatzes einer bestimmten Straße nach deren Name oder das Laden eines _way_-Typen (z.B. Radwege, Schnellstraßen usw.). Zum gewählten Datensatz werden Informationen über die Stadt ausgegeben (Bundesland, PLZ, Einwohnerzahl), ein selbstprogrammierter Algorithmus berechnet die **Länge einer Straße** / eines Straßennetzes. Der Aufbau der _OSM_-Daten macht zudem eine Verwendung dieser Implementierung für weitere _ways_ möglich. Dies kann z.B. zur Auswertung von Stromleitungen oder Flüssen genutzt werden.

## Projektaufbau

Zur besseeren Übersichtlichkeit und Modularität wurden die einzelnen Funktionalitäten unserer Implementierung in verschiedenen Klassen umgesetzt. Hierfür wurde das gemeinsam erarbeitete UML-Diagramm umgesetzt:  
![UML-Diagramm](https://camo.githubusercontent.com/a4ab0b58eb71dbb4b4c89cd2dd68673498c54618c26dcbbffed464794cc0ab7a/687474703a2f2f7777772e706c616e74756d6c2e636f6d2f706c616e74756d6c2f706e672f644c50544a7a696d3537744668783271627149714944695a475758654c754934364141547a653047436c356a512d377550684f705933522d2d2d6e796b30786649384f563242526c4655567774454363667a6647504f6643484163574d66437646616b3162344e345a6a794851534f314d5536323275574239535748714b506e6b49344a6968506861635257726e4278377a4d624a4363417759544930526e36725143632d415170454a4c71324c4962737053694b6d3832436631634c5550307458632d57593372474c55476a7730424f5649786f485530414a416d654a62413348734b48746d5865794f484553543243626c354552773432366a4b7072496e58614a7a437474485a476958565851576f6c456d616c347732346f666b39486837337831366e73596544727256736f3950716f31725a3562724e463763465749565935643934597a3033465a666448545f6476784855396165465a473878464138725438634b6c6c735546312d53726330537336583564756f425676727049336152667378384d354963364342777035626e4e706961786e41397a386c48386437714c76334d7749347863684b35586b5a6a6647755465786f43496238566958796139476d723645574757706473617676765056665a324b6f523175353131725f4e43636e307a51775a575049425031545f55316c3635684870524f717a7875717952392d4444517158614d7167586f4636634755704577495f77637a772d426a52653131516e6c59463151726235454c5838744d42504466696e587163396c2d4d5068726d5253455159615251334538554c6864626777733239734e58344a79574862456e6442765669547a4c796643776b57516e737164636a3459426d58734f4654526872365a657a4a6244675364456d324733353536497a71466f5930635344635852554b6e4c486b314249586d71534f7a685f6453453954386d457a4b39585470644e6939665670386f3947526b786b6179465f50743038545f537858675f5179326e4777774f714d786631484d6b6f7071744166466878665075616d774e7457505676355848466750575841695162744d65657a5a34537857437a73334e7436696d4c6f3874506e583943535f4b5f386c71304a4c6a79743778515a3751675477507854687a706f374a676c7770737875516c463379586e742d634b7455682d776c7846557a59672d74454a7938664935506175597930)

### Verwendete APIs

Folgende bestehende Implementierungen wurden für die Umsetzung unseres Projekts verwendet:

| Name          | Funktion                                                        |
|---------------|-----------------------------------------------------------------|
| [OsmSharp](https://github.com/OsmSharp/core)      | Auslesen von _node_- und _way_-Objekten aus lokalen _OSM_-Daten |
| [Overpass API](https://wiki.openstreetmap.org/wiki/Overpass_API)  | Herunterladen eines _OSM_-Datensatzes                           |
| [Nominatim API](https://nominatim.org/release-docs/develop/api/Overview/) | Suche von _OSM_-Daten                                                              |

Zur erfolgreichen Nutzung unserer Implementierung ist das Einbinden der folgenden Packages in der _.csproj_-Datei notwendig:

```
<PackageReference Include="Nominatim.API" Version="*"/>
<PackageReference Include="OsmSharp" Version="*"/>
```

## Dokumentation

Eine ausführlichere Dokumentation unseres Projektes befindet sich im [Wiki](https://github.com/DarioDagne/OSM-Analysis/wiki). Hier haben wir den Ablauf der [Datenfilterung](https://github.com/DarioDagne/OSM-Analysis/wiki/Datenfilterung-und-Datengewinnung) sowie die zugrundeliegenden [Berechnungskonzepte](https://github.com/DarioDagne/OSM-Analysis/wiki/Berechnung-der-Stra%C3%9Fenl%C3%A4nge-aus-geographischen-Koordinaten) während des Entstehungsprozesses unserer Software beschrieben und durch Grafiken, Codebeispiele und Diagramme ergänzt. 

## Quellenangaben

Neben den genannten APIs wurden zur theoretischen Ausarbeitung dieses Projekts die nachfolgenden Quellen verwendet. Entsprechende Verweise finden sich in der ausführlichen Dokumentation des Projekts im [Wiki](https://github.com/DarioDagne/OSM-Analysis/wiki).  

[1] - FAQ: Was ist OpenStreetMap?, Abgerufen 06. Juli 2021 von https://www.openstreetmap.de/faq.html#was_ist_osm  
[2] - DE:Elemente, Abgerufen 13. Juli 2021 von https://wiki.openstreetmap.org/wiki/DE:Elemente  
[3] - Geographische Koordinaten, Abgerufen 21. Juli 2021 von https://de.wikipedia.org/wiki/Geographische_Koordinaten  
[4] - Martin Kompf - Entfernungsberechnung, Abgerufen 21. Juli 2021 von https://www.kompf.de/gps/distcalc.html  
[5] - Node, Abgerufen 21. Juli 2021 von https://wiki.openstreetmap.org/wiki/Node  
[6] - Geographische Breite - Abgerufen 21. Juli 2021 von https://de.wikipedia.org/wiki/Geographische_Breite  
[7] - Geographische Länge - Abgerufen 21. Juli 2021 von https://de.wikipedia.org/wiki/Geographische_L%C3%A4nge
