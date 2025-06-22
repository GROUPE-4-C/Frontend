using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "L'e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format d'e-mail invalide.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Le code OTP est obligatoire.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Le code OTP doit contenir exactement 6 chiffres.")]
        public required string Otp { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères.")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "Veuillez confirmer le mot de passe.")]
        [Compare("NewPassword", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public required string ConfirmPassword { get; set; }
    }
}
