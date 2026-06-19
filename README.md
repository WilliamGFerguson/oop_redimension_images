# Application Console – Redimensionnement d’images PBM et PGM

## Description du projet

Ce projet est une application console en C# permettant de redimensionner des images aux formats PBM et PGM.  
Bien que le code soit relativement simple et corresponde à mes débuts en programmation, il démontre mes compétences en programmation orientée objet et ma capacité à structurer un projet avec un flow logique de traitement des données.

L’objectif principal du projet est de parcourir des images dans un dossier, de valider leur format, puis de produire une version redimensionnée tout en conservant la logique métier séparée dans des classes et interfaces dédiées.

## Fonctionnalités principales

- Parcours d’un dossier pour détecter les fichiers images valides (.pbm et .pgm)
- Redimensionnement des images avec interpolation
- Sauvegarde des images redimensionnées sous un nouveau nom
- Gestion des formats via une interface commune (`IResizeImage`)
- Structure orientée objet avec classes dédiées pour chaque type d’image (`PBMImage`, `PGMImage`)
- Flow clair : validation → redimensionnement → sauvegarde

## Technologies utilisées

- C# (Console Application)
- Programmation orientée objet
- Interfaces et classes abstraites pour structurer la logique
- Gestion de fichiers et flux de lecture/écriture d’images
