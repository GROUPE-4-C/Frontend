using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class User
    {
        [Required(ErrorMessage = "L'e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format d'e-mail invalide.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères.")]
        public required string Password { get; set; }
    }
}
