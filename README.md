# Frydia

Application WinForms de sensibilisation : elle simule un faux virus après déverrouillage de session Windows.  
Ce programme est uniquement un outil de sensibilisation interne pour rappeler l’importance de verrouiller son poste de travail.

## Installation
```bash
git clone https://github.com/IAidenI/Frydia.git
cd Frydia && .\build.bat
.\bin\Frydia.exe
```

## Fonctionnement

En cas de problème avec Frank, un bouton de 5px/5px toute en bas à droit permet de quitter proprement.  
En cas de problème avec Lydia, le code de sécurité qui marche tout le temps est : -8000.

Au lancement, Frank ouvre une fenêtre sur chaque écran détecté puis verrouille l'ordinateur.

Elle détecte ensuite lorsque l’utilisateur déverrouille sa session Windows et affiche un message d’alerte plein écran avec un compte à rebours et bloque les interactions avec le clavier.

A la fin du compte a rebours, les fichiers ne sont pas supprimer comme pourrait le laisser penser le message mais un simple popup final s'affiche et restaure le clavier.

**Note :** Le ctrl + alt + suppr est toujours actif (car impossible à verrouillé (sécurité Windows)) et si le gestionnaire des tâches est lancé cela réactive l'affichage du alt + tab. Ce comportement peut être éviter en lançant le programme en administrateur.

Ensuite Lydia prend le relai et demande a l'utilisateur de saisir la réponse a un calcul en bloquant certains fonctionnalités de l'utilisateurs.

## TODO

- [ ] Frank : refaire le TextStyle en plus propre
- [ ] ProcessWatcher : refacto car pas propre
- [ ] Global : commentaires