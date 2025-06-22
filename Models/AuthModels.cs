using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom complet est obligatoire")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmation du mot de passe est obligatoire")]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "La promotion est obligatoire")]
        public int PromotionId { get; set; }

        [Required(ErrorMessage = "La profession est obligatoire")]
        public string Profession { get; set; } = string.Empty;

        public string? Bio { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhotoBase64 { get; set; }
    }


    public class LoginDto
    {
        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        public string Password { get; set; } = string.Empty;
    }

    public class ConfirmEmailDto
    {
        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le code OTP est obligatoire")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Le code OTP doit contenir 6 caractères")]
        public string Token { get; set; } = string.Empty;
    }

    public class ResendOtpDto
    {
        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; } = string.Empty;
    }

    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le code OTP est obligatoire")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Le code OTP doit contenir 6 caractères")]
        public string Otp { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nouveau mot de passe est obligatoire")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        public string NewPassword { get; set; } = string.Empty;
    }

    public class UserInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Promotion { get; set; }
        public string Profession { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserInfo User { get; set; } = new();
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public string[]? Errors { get; set; }
    }
}
