# 🎓 GestionEtudiantEnsit — Gestion des étudiants, paiements et réinscriptions

Application **Windows Forms en VB.NET** pour gérer les étudiants d'un établissement : inscriptions, paiements des frais de scolarité, réinscriptions annuelles et rapports.

![VB.NET](https://img.shields.io/badge/VB.NET-.NET%2010-512BD4) ![WinForms](https://img.shields.io/badge/UI-Windows%20Forms-0078D4) ![Access](https://img.shields.io/badge/DB-MS%20Access-A4373A)

## Fonctionnalités

- **Connexion** avec rôles (les écrans visibles dépendent du rôle) — `FormLogin`
- **Étudiants** : création, modification, photo — `FormEtudiant`
- **Paiements** : suivi des versements et des frais attendus — `FormPaiement`
- **Réinscriptions** par année — `FormReinscription`
- **Rapports** : états filtrables des paiements et des étudiants — `FormRapports`
- **Gestion des utilisateurs** — `FormUsers`
- Menu principal adapté au rôle — `FormMenuPrincipal`

## Stack technique

- VB.NET sur **.NET 10** (Windows Forms)
- **MS Access** (`.accdb`) via `System.Data.OleDb` (fournisseur ACE OLEDB 12)
- Requêtes paramétrées pour limiter les injections SQL

## Base de données

Le fichier `GestionEtudiant.accdb` **n'est pas publié** (il contient des données réelles). Pour lancer l'application, créer une base Access du même nom, placée à côté de l'exécutable, avec les tables :

| Table | Rôle |
|---|---|
| `Utilisateurs` | comptes et rôles |
| `Etudiants` | fiches étudiants |
| `Paiements` | versements |
| `FraisAttendus` | frais attendus par étudiant |
| `Reinscriptions` | réinscriptions annuelles |

Un dossier `PhotosEtudiants/` est utilisé à côté de l'exécutable pour les photos.

## Installation

1. **Prérequis** : Windows, SDK .NET 10, [Microsoft Access Database Engine (ACE OLEDB 12)](https://www.microsoft.com/download/details.aspx?id=54920).
2. Placer `GestionEtudiant.accdb` dans le dossier de sortie (`bin/Debug/net10.0-windows`).
3. Lancer :
   ```bash
   dotnet run
   ```

## Limites connues / améliorations

- Les mots de passe sont comparés en clair dans la base : à remplacer par un hachage (PBKDF2/bcrypt).
- Chaîne de connexion codée en dur dans `dbManager.vb` : à externaliser.

## Auteur

**Charles Traoré** — projet ENSIT.
