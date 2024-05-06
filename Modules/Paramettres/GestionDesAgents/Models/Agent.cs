using System.ComponentModel.DataAnnotations;

namespace HPRBackend.Modules.Paramettres.GestionDesAgents.Models
{
    public class Agent
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "le nom est Obligatoire ")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "le prenom est Obligatoire ")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "le Contacte est Obligatoire ")]
        public string Contacte { get; set; }
        [Required(ErrorMessage = "l'addresse  Email est Obligatoire ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "la Date de naissance  est Obligatoire ")]
        public string DateNaissance { get; set; }
        [Required(ErrorMessage = "Lieu de naissance  est Obligatoire ")]
        public string? LieuNaissance { get; set; }
        [Required(ErrorMessage = "le Matricule  est Obligatoire ")]
        public string Matricule { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "l'Adresse est Obligatoire ")]
        public string Adresse { get; set; }
        [Required(ErrorMessage = "le Sexe est Obligatoire ")]
        public string Sexe { get; set; }
        [Required(ErrorMessage = "le Ville est Obligatoire ")]
        public string Ville { get; set; }

        public string? CodePostal { get; set; }
        [Required(ErrorMessage = "le Situation_matri est Obligatoire ")]
        public string Situation_matri { get; set; }

        public DateTime? Date_Ajout { get; set; }
    }
}
