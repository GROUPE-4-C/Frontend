using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class PromotionModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la promotion est obligatoire")]
        public string Nom { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
