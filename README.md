# Frydia

**Outil de sensibilisation à la sécurité informatique :** Une application Windows Forms qui simule un scénario de ransomware pour rappeler l'importance de sécuriser son poste de travail.

## Installation et exécution

### Prérequis

- **Windows 10 ou 11** (64 bits)

### Installation rapide
```powershell
# Installation rapide
curl.exe -L -s -f -o quicklaunch.bat "https://github.com/IAidenI/Frydia/releases/download/v1.1/quick_install.bat"; .\quicklaunch.bat
```

### Installation manuelle

```powershell
# Cloner le dépôt
git clone https://github.com/IAidenI/Frydia.git
cd .\Frydia\install

# Compiler les binaires
.\install.bat

# Les exécutables se trouvent dans .\bin
# Frydia.exe est prêt à l'emploi
```

### Installation complète (Persistance globale)

```powershell
# Après avoir effectuer l'installation manuelle
.\bin\setup.exe

# Cela permet de :
# - Rendre l'application persistante
# - L'enregistrer en tant que programme du système
# - Se lancer avec le raccourci : Ctrl + Alt + Shift + F12
```

### Lancement

**Mode simple** :
```powershell
.\Frydia.exe
```

**Après installation complète** :
```
Raccourci clavier depuis n'importe quelle fenêtre : Ctrl + Alt + Shift + F12
```

## Vue d'ensemble

**Frydia** est une application de sensibilisation à la sécurité informatique. Elle utilise une simulation réaliste (mais **non-destructrice**) d'une attaque par ransomware pour enseigner aux utilisateurs les bonnes pratiques de sécurité.

L'application simule une infection par malware qui se déclenche lors du déverrouillage d'une session Windows. Deux "personnages" guident l'expérience :

- **Frank** : met la pression avec une alerte et un compte à rebours
- **Lydia** : poursuit l'expérience avec des défis mathématiques permettant de faire intégrer la leçon

**Notes**

- Aucune donnée n'est endommagée
- S'affiche sur chaque écran connecté
- Code d'urgence disponible

## Mesures d'urgence

En cas de problème, deux mécanismes de secours sont disponibles :

- **Frank** : Bouton d'urgence discret (5px × 5px) positionné en bas à droite de l'écran, conçu pour arrêter immédiatement la simulation en cas de nécessité
- **Lydia** : Code d'urgence `-8000` : Entrez ce code en réponse à la formule mathématique pour fermer l'application instantanément, quelle que soit l'équation générée

## Flux de l'application

### Phase 1 : Démarrage et verrouillage

1. L'application se lance de manière invisible
2. Une instance de page d'alerte (**Frank**) apparaît sur **chaque écran connecté**
3. Le poste de travail Windows est **verrouillé immédiatement**
4. L'utilisateur doit déverrouiller sa session normalement

### Phase 2 : Frank - Compte à rebours

Dès le déverrouillage :

- Le **clavier est désactivé** (souris reste active)
- Un **compte à rebours** de 10 secondes s'affiche
- Bouton d'urgence disponible en bas a droit de l'écran pour les cas critiques
- Après la fin du compte à rebours --> transition vers **Lydia**

### Phase 3 : Lydia - Défi mathématique

1. **Génération d'une formule mathématique aléatoire**
   - Plusieurs motifs de complexité variable
   - L'utilisateur doit entrer le résultat correct

2. **Surveillance active des processus**
   - Détecte et bloque les tentatives de contournement :

| Processus | Action |
|-----------|--------|
| `Taskmgr` | Bloqué, affiche un message d'erreur |
| `CalculatorApp` | Superposition d'une fenêtre de bloquage |
| `excel` | La formule change à la validation si détecté |
| `SnippingTool`, `ScreenClippingHost`, `SnipAndSketch` | Cache l'application pour empêcher une capture d'écran |

3. **Protections contre les contournements**
   - **Ctrl+C** → Texte remplacé par un autre contenu
   - **Ctrl+V** → Contenu changé dans le champ de saisie
   - **Excel détecté** → Formule renouvelée à chaque tentative (jusqu'à 2 fois)

4. **Résolution**
   - Saisir le bon résultat → Application se ferme proprement
   - Saisir le code d'urgence `-8000` → Fermeture immédiate
