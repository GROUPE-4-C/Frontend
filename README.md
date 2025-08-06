Architecture de dossiers recommandée pour Blazor WebAssembly:

```plaintext
AlumniConnect.Front/
├── wwwroot/                  # Fichiers statiques (images, css, favicon, etc.)
├── Models/                   # Modèles métiers (Promotion, User, Temoignage, etc.)
├── Mocks/                    # Données fictives pour avancer sans backend
├── Services/                 # Services pour consommer l’API (HttpClient, Auth, etc.)
├── Pages/                    # Pages Razor (une par fonctionnalité)
│   ├── Auth/                 # Pages d’authentification (Login, Register)
│   ├── Alumni/               # Recherche, fiche d’un ancien, etc.
│   ├── Promotions/           # Liste, sélection de promotions
│   ├── Temoignages/          # Mur, création, édition, suppression de témoignages
│   └── ...                   # Autres modules (messagerie, admin, etc.)
├── Components/               # Composants réutilisables (Card, Modal, etc.)
├── Shared/                   # Layouts, NavMenu, Header, Footer, etc.
├── App.razor                 # Point d’entrée de l’app
├── Program.cs                # Bootstrap de l’app
├── AlumniConnect.Front.csproj
└── ...
```

---

## Exemple de répartition du travail

- **Pages/Auth/** : 1 étudiant sur Login/Register, gestion du token, UI de connexion
- **Pages/Alumni/** : 1 étudiant sur la recherche, la fiche d’un ancien, le filtrage par promo/métier
- **Pages/Promotions/** : 1 étudiant sur la liste des promotions, la sélection à l’inscription
- **Pages/Temoignages/** : 1 étudiant sur le mur de témoignages, création/édition/suppression
- **Components/** : 1 étudiant sur les composants réutilisables (boutons, cartes, modals, etc.)
- **Shared/** : 1 étudiant sur la structure globale (Header, Footer, navigation)

---

## Exemple de fichiers pour avancer sans backend

**Models/Promotion.cs**
```csharp
public class Promotion
{
    public int Id { get; set; }
    public string Nom { get; set; }
}
```

**Mocks/PromotionsMock.cs**
```csharp
public static class PromotionsMock
{
    public static List<Promotion> Promotions = new()
    {
        new Promotion { Id = 1, Nom = "Promo 2025" },
        new Promotion { Id = 2, Nom = "Promo 2026" }
    };
}
```

**Pages/Promotions/Promotions.razor**
```csharp
@page "/promotions"
@using AlumniConnect.Front.Models
@using AlumniConnect.Front.Mocks

<h3>Liste des promotions</h3>
<ul>
@foreach (var promo in PromotionsMock.Promotions)
{
    <li>@promo.Nom</li>
}
</ul>
```

---

## README pour guider l’équipe

```markdown
# AlumniConnect.Front

Frontend Blazor WebAssembly pour le portail AlumniConnect.

## Structure du projet

- `Models/` : Modèles métiers (Promotion, User, Temoignage, etc.)
- `Mocks/` : Données fictives pour avancer sans backend
- `Services/` : Services pour consommer l’API (à brancher plus tard)
- `Pages/` : Pages Razor (une par fonctionnalité)
- `Components/` : Composants réutilisables
- `Shared/` : Layouts globaux (Header, Footer, etc.)

## Répartition du travail

- Authentification : Pages/Auth/
- Recherche d’anciens : Pages/Alumni/
- Promotions : Pages/Promotions/
- Mur de témoignages : Pages/Temoignages/
- Composants réutilisables : Components/
- Layouts : Shared/

## Démarrage

    dotnet run

