namespace AlumniConnect.Front.Models
{
    public class AlumniModel
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? PhotoUrl { get; set; }
        public string Promotion { get; set; } = string.Empty;
    }
}
