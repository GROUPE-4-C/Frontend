using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "L'e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format d'e-mail invalide.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Le nom complet est obligatoire.")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Sélectionnez une promotion.")]
        public required string Promotion { get; set; }

        public string Profession { get; set; } = "";
        public string PhotoUrl { get; set; } = "";
        public string Bio { get; set; } = "";

    }
}
