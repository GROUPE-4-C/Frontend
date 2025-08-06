using System;
using System.ComponentModel.DataAnnotations;

namespace AlumniConnect.Front.Models
{
    public class ExperienceModel
    {
        public Guid Id { get; set; }
        public string Poste { get; set; } = string.Empty;
        public string Entreprise { get; set; } = string.Empty;
        public string Localisation { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public bool EnCours { get; set; }
        public string? TypeContrat { get; set; }
        public string? Secteur { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModification { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhotoUrl { get; set; }
        public string? Duree { get; set; }
    }
}
