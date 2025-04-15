# Éditeur Graphique Windows Forms

Un éditeur graphique simple permettant de dessiner des formes géométriques basiques, développé avec Windows Forms en C#.

## Fonctionnalités

- Dessin de formes géométriques :
  - Lignes
  - Rectangles
  - Ellipses
- Personnalisation :
  - Choix de la couleur
  - Modification de l'épaisseur du trait
- Gestion de fichiers :
  - Sauvegarde des dessins
  - Chargement des dessins existants

## Interface utilisateur

### Menu principal
- **Fichier** :
  - Ouvrir : Charger un dessin existant
  - Enregistrer : Sauvegarder le dessin actuel
- **Outils** :
  - Ligne : Outil de dessin de lignes
  - Rectangle : Outil de dessin de rectangles
  - Ellipse : Outil de dessin d'ellipses
  - Épaisseur du trait : Modifier l'épaisseur du trait

### Menu contextuel (clic droit)
- Couleur : Changer la couleur du trait
- Augmenter trait : Augmenter l'épaisseur du trait
- Diminuer trait : Diminuer l'épaisseur du trait

### Barre d'état
Affiche l'outil actuel, la couleur sélectionnée et l'épaisseur du trait

## Structure du projet

### Classes principales

#### Forme (classe abstraite)
Base pour toutes les formes géométriques avec :
- Points de début et fin
- Couleur
- Épaisseur du trait
- Méthodes abstraites pour le dessin et la sauvegarde

#### Classes dérivées
- **Ligne** : Implémentation du dessin de lignes droites
- **Rectangle** : Implémentation du dessin de rectangles
- **Ellipse** : Implémentation du dessin d'ellipses

## Utilisation

1. Sélectionnez un outil de dessin dans le menu "Outils"
2. Personnalisez la couleur et l'épaisseur du trait si désiré
3. Cliquez et maintenez le bouton gauche de la souris pour commencer le dessin
4. Déplacez la souris pour ajuster la forme
5. Relâchez le bouton pour finaliser le dessin

## Format de sauvegarde

Les dessins sont sauvegardés au format texte avec une ligne par forme :
```
TypeForme;X1;Y1;X2;Y2;R;G;B;Epaisseur
```
- TypeForme : Ligne, Rectangle ou Ellipse
- X1,Y1 : Coordonnées du point de début
- X2,Y2 : Coordonnées du point de fin
- R,G,B : Composantes de couleur RGB
- Epaisseur : Épaisseur du trait en pixels
