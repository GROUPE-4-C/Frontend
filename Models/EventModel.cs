using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class EventModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(150, ErrorMessage = "Le titre ne peut pas dépasser 150 caractères")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "La description est obligatoire")]
        [StringLength(2000, ErrorMessage = "La description ne peut pas dépasser 2000 caractères")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date est obligatoire")]
        public DateTime Date { get; set; } = DateTime.Now.AddDays(7);

        [Required(ErrorMessage = "Le lieu est obligatoire")]
        [StringLength(200, ErrorMessage = "Le lieu ne peut pas dépasser 200 caractères")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'organisateur est obligatoire")]
        [StringLength(100, ErrorMessage = "Le nom de l'organisateur ne peut pas dépasser 100 caractères")]
        public string Organizer { get; set; } = string.Empty;

        [Url(ErrorMessage = "L'URL de l'image n'est pas valide")]
        public string? ImageUrl { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
