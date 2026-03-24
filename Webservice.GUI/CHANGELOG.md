In dieser Datei wird erläutert, wie Visual Studio das Projekt erstellt hat.

Folgende Tools wurden zur Erstellung dieses Projekts verwendet:
- Angular CLI (ng)

Folgende Schritte wurden zur Erstellung dieses Projekts verwendet:
- Erstellen Sie ein Angular-Projekt mit NG: `ng new Webservice.GUI --defaults --skip-install --skip-git --no-standalone `.
- Aktualisieren Sie angular.json mit dem Port.
- Projektdatei (`Webservice.GUI.esproj`) erstellen.
- Erstellen Sie `launch.json`, um das Debuggen zu aktivieren.
- Aktualisieren Sie package.json, um `jest-editor-support` hinzuzufügen.
- `start`-Skript in `package.json` aktualisieren, um den Host anzugeben.
- Fügen Sie `karma.conf.js` für Einheitstests hinzu.
- `angular.json` aktualisieren, um auf `karma.conf.js` zu zeigen.
- Projekt zur Projektmappe hinzufügen.
- Schreiben Sie diese Datei.
