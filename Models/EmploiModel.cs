using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class EmploiModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères")]
        public string Titre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La description est obligatoire")]
        [StringLength(2000, ErrorMessage = "La description ne peut pas dépasser 2000 caractères")]
        public string Description { get; set; } = string.Empty;

        public string? ImageBase64 { get; set; }

        [Required(ErrorMessage = "L'entreprise est obligatoire")]
        [StringLength(100, ErrorMessage = "Le nom de l'entreprise ne peut pas dépasser 100 caractères")]
        public string Entreprise { get; set; } = string.Empty;

        [Required(ErrorMessage = "La localisation est obligatoire")]
        [StringLength(100, ErrorMessage = "La localisation ne peut pas dépasser 100 caractères")]
        public string Localisation { get; set; } = string.Empty;

        public DateTime DateDebut { get; set; } = DateTime.Today;
        public DateTime DateFin { get; set; } = DateTime.Today.AddMonths(6);

        [Url(ErrorMessage = "L'URL de l'image n'est pas valide")]
        public string? ImageUrl { get; set; }

        public string UserId { get; set; } = string.Empty;
        public bool EstActif { get; set; } = true;

        public string? CreatorName { get; set; }
        public string? CreatorEmail { get; set; }

        // Ajout pour le tri par date de création
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
