namespace AlumniConnect.Front.Utils
{
    public static class PhotoUrlHelper
    {
        public static string GetFullPhotoUrl(string? photoUrl)
        {
            if (string.IsNullOrWhiteSpace(photoUrl))
                return "images/default-avatar.png";

            if (photoUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return photoUrl;

            return $"http://localhost:5175{photoUrl}";
        }
    }
}
