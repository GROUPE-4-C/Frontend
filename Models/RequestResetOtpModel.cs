using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Models
{
    public class RequestResetOtpModel
    {
        [Required(ErrorMessage = "L'e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format d'e-mail invalide.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Le code OTP est obligatoire.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Le code OTP doit contenir exactement 6 chiffres.")]
        public required string Token { get; set; }
    }
}
