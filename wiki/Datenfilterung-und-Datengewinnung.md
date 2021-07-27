
Das folgende UML-Diagramm soll dazu dienen, einen Überblick über den Ablauf unseres Prozesses zur Filterung und Gewinnung der zu analysierende Daten zu bieten:  

![Diagramm](http://www.plantuml.com/plantuml/png/dLPTJzim57tFhx2qbqIqIDiZGWXeLuI46AATze0GCl5jQ-7uPhOpY3R---nyk0xfI8OV2BRlFUVwtECcfzfGPOfCHAcWMfCvFak1b4N4ZjyHQSO1MU622uWB9SWHqKPnkI4JihPhacRWrnBx7zMbJCcAwYTI0Rn6rQCc-AQpEJLq2LIbspSiKm82Cf1cLUP0tXc-WY3rGLUGjw0BOVIxoHU0AJAmeJbA3HsKHtmXeyOHEST2Cbl5ERw426jKprInXaJzCttHZGiXVXQWolEmal4w24ofk9Hh73x16nsYeDrrVso9Pqo1rZ5brNF7cFWIVY5d94Yz03FZfdHT_dvxHU9aeFZG8xFA8rT8cKllsUF1-Src0Ss6X5duoBVvrpI3aRfsx8M5Ic6CBwp5bnNpiaxnA9z8lH8d7qLv3MwI4xchK5XkZjfGuTexoCIb8ViXya9Gmr6EWGWpdsavvvPVfZ2KoR1u511r_NCcn0zQwZWPIBP1T_U1l65hHpROqzxuqyR9-DDQqXaMqgXoF6cGUpEwI_wczw-BjRe11QnlYF1Qrb5ELX8tMBPDfinXqc9l-MPhrmRSEQYaRQ3E8ULhdbgws29sNX4JyWHbEndBvViTzLyfCwkWQnsqdcj4YBmXsOFTRhr6ZezJbDgSdEm2G3556IzqFoY0cSDcXRUKnLHk1BIXmqSOzh_dSE9T8mEzK9XTpdNi9fVp8o9GRkxkayF_Pt08T_SxXg_Qy2nGwwOqMxf1HMkopqtAfFhxfPuamwNtWPVv5XHFgPWXAiQbtMeezZ4SxWCzs3Nt6imLo8tPnX9CS_K_8lq0JLjyt7xQZ7QgTwPxThzpo7JglwpsxuQlF3yXnt-cKtUh-wlxFUzYg-tEJy8fI5PauYy0)


## **Datengewinnung** 
Die OSM-Database verfügt über eine größere Menge  an Daten und unser Ziel  ist nicht alle Daten zu analysieren sondern  nur eine teil davon, denn ist hier klar, dass wir irgendwie die Daten filtern müssen, bevor wir dieser herunterladen. Für die Erste Filterung werden wir die Nominatim nutzen, also gibt uns die Möglichkeit  die Name der Stadt oder Postleitzahl  als Suchbegriff  einzugeben und als Ergebnis  liefert uns  Daten wie Latitude/Longitude, Alternative Name, Box(Begrenzung des gesuchtes Elements )  etc. Wobei hier Die Box uns interessiert, denn wir brauchen dieser im Overpass  API für die Download.

## **Datenfilterung** 
Nach der Download der Daten  werden wir auch nicht alles auf einmal ausgeben, sondern nur bestimmte Infomationen, die für uns an der stelle interessieren. Für diese Filterung werden wir OsmSharp benutzen.

## **Beispiel** 
 Für die Suche und Herunterladen der Daten findet man im Branch Tests.Hier möchte ich die geladenen Daten(Freiberg) nuten, um die Erste 5 knoten zu extraiehen:

` await using var fileStream = File.OpenRead(path);  `

`XmlOsmStreamSource source = new XmlOsmStreamSource(fileStream);`

`var anfangsknoten =(from os in source`

                    where os.Type == OsmSharp.OsmGeoType.Node  

                    select os).Take(5);

`var anfang = anfangsknoten.ToComplete();`                            
         
 `Node[] knoten = new Node[anfang.Count()];`

`for(int i = 0; i<anfang.Count();i++)`

`{
       knoten[i] = (Node)anfang.ElementAt(i);
 }`
   
   
