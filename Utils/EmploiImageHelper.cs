using System;

namespace AlumniConnect.Front.Utils
{
    public static class EmploiImageHelper
    {
        private static readonly string DefaultImageUrl = "/images/emplois/default-job.png";

        public static string GetEmploiImageUrl(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return DefaultImageUrl;
            }

            // Si l'URL est déjà complète (commence par http ou https)
            if (imageUrl.StartsWith("http://") || imageUrl.StartsWith("https://"))
            {
                return imageUrl;
            }

            // Si c'est un chemin relatif, on ajoute le préfixe du dossier images
            if (!imageUrl.StartsWith("/images/"))
            {
                imageUrl = $"/images/emplois/{imageUrl}";
            }

            return imageUrl;
        }
    }
}
