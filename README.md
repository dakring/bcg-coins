# Projekt Bildbasierte Computer Grafik :camera:: 'Coins'
### Ausgearbeitet von: David Alexander Kring
### Betreuender Professor: Andreas Karge

Im Modul _Bildbasierte Computergrafik_ an der TH Köln bearbeiten Studenten praktische Aufgaben. Dieses Repository enthält eine Lösung für das Projekt _Coins_ .
Ziel dieses Projekts ist das Zählen von Euro-Münzen auf einem Bild mittels verschiedener Algorithmen der Computergrafik. Dabei soll die Orientation der Münzen, sowie die Münzseite irrelevant sein. Betrachtet wird lediglich das deutsche Set der Euro-Münzen.

Links für einfache Navigation:
- [Dokumentation](https://github.com/dakring/bcg-coins/blob/main/Dokumentation/Bildbasierte_Computergrafik_Coins.pdf)
- [Datensatz](https://github.com/dakring/bcg-coins/tree/main/Datensatz)
- [Excel Tabelle mit Versuchsergebnissen](https://github.com/dakring/bcg-coins/blob/main/Evaluationsergebnisse/Ergebnisse.xlsx)
- [Exectuable (Debug)](https://github.com/dakring/bcg-coins/blob/main/bcg-coins/bin/Debug/bcg-coins.exe) (benötigt eventuell das Downloaden weiterer Ressourcen)
- [Präsentationsfolien](https://github.com/dakring/bcg-coins/blob/main/Abschlusspr%C3%A4sentation/Abschlusspr%C3%A4sentation_Coins.pdf)

Um den Code auf einer neuen Maschine ausführen zu können müssen die folgenden Pfade im Code von _MainWindow.cs_ angepasst werden:
> Zeile 156 - 171 - Die Pfade zu den Ground Truth-Dateien für jede Münze (Vorder- und Rückseite) müssen angegeben werden. 
> Zeile 290 - eine Kreisreferenz wird benötigt für die Konturensuche.

Diese Dateien befinden sich alle im Ordner für die Datensets, die Kreisreferenz unter dem alten Datensatz.
