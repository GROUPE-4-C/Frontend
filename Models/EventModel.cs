using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class EventModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(150, ErrorMessage = "Le titre ne peut pas dépasser 150 caractères")]
        [Display(Name = "Titre")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "La description est obligatoire")]
        [StringLength(2000, ErrorMessage = "La description ne peut pas dépasser 2000 caractères")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date est obligatoire")]
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.Now.AddDays(7);

        [Required(ErrorMessage = "Le lieu est obligatoire")]
        [StringLength(200, ErrorMessage = "Le lieu ne peut pas dépasser 200 caractères")]
        [Display(Name = "Lieu")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'organisateur est obligatoire")]
        [StringLength(100, ErrorMessage = "Le nom de l'organisateur ne peut pas dépasser 100 caractères")]
        [Display(Name = "Organisateur")]
        public string Organizer { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string UserId { get; set; } = string.Empty;

        // Ajout pour affichage du créateur
        public string CreatorName { get; set; } = string.Empty;
        public string CreatorEmail { get; set; } = string.Empty;

        // Pour upload base64 (non mappé en base, juste pour la création)
        public string? ImageBase64 { get; set; }
    }
}
