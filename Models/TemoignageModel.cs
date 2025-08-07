using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class TemoignageModel
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Contenu { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Promotion { get; set; } = string.Empty;

        // Propriétés de compatibilité pour le support existant
        public string UserName { get => FullName; set => FullName = value; }
        public string UserTitle { get => Profession; set => Profession = value; }
        public string? MotsCles { get; set; }

        // Statistiques
        public int NombreReactions { get; set; } = 0;
        public int NombreCommentaires { get; set; } = 0;
        public int NombrePartages { get; set; } = 0;
    }
}
