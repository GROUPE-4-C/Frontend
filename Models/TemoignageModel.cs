using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class TemoignageModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le contenu du témoignage est obligatoire")]
        [StringLength(5000, MinimumLength = 50, ErrorMessage = "Le témoignage doit contenir entre 50 et 5000 caractères")]
        public string Contenu { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Champ additionnel pour les mots-clés
        [StringLength(200, ErrorMessage = "Les mots-clés ne peuvent pas dépasser 200 caractères")]
        public string? MotsCles { get; set; }

        // Navigation properties simulées
        public string UserName { get; set; } = string.Empty;
        public string UserTitle { get; set; } = string.Empty;
    }
}
