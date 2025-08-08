using System;

namespace AlumniConnect.Front.Utils
{
    public static class EventImageHelper
    {
        private const string DefaultEventImagePath = "images/default-event.png";
        private const string ApiBaseUrl = "http://localhost:5175";

        public static string GetEventImageUrl(string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return DefaultEventImagePath;

            // Si l'URL est déjà complète (commence par http/https)
            if (imageUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return imageUrl;

            // Si l'URL est un chemin relatif commençant par /images
            if (imageUrl.StartsWith("/images", StringComparison.OrdinalIgnoreCase))
                return $"{ApiBaseUrl}{imageUrl}";

            // Pour les chemins absolus du système
            if (imageUrl.StartsWith("/home") || imageUrl.Contains(":\\"))
                return DefaultEventImagePath;

            // Si le chemin ne commence pas par /, on l'ajoute
            if (!imageUrl.StartsWith("/"))
                imageUrl = $"/{imageUrl}";

            return $"{ApiBaseUrl}{imageUrl}";
        }

        public static string FormatBase64Image(string? base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
                return string.Empty;

            // Vérifie si la chaîne contient déjà le préfixe data:image
            if (base64String.StartsWith("data:image"))
                return base64String;

            // Si non, ajoute le préfixe pour l'affichage de l'image
            return $"data:image/jpeg;base64,{base64String}";
        }

        public static bool IsValidImageUrl(string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return false;

            // Vérifie si l'URL est au format http/https
            if (imageUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return Uri.TryCreate(imageUrl, UriKind.Absolute, out _);

            // Vérifie si c'est un chemin d'image valide
            return imageUrl.StartsWith("/images") ||
                   imageUrl.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                   imageUrl.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                   imageUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                   imageUrl.EndsWith(".gif", StringComparison.OrdinalIgnoreCase);
        }
    }
}
