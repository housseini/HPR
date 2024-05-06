using System.ComponentModel.DataAnnotations;

namespace HPRBackend.Modules.Gestion_Des_Patients.Models
{
    public class Patient
    {
        public long Id { get; set; }
        public long AGE { get; set; }
        [Required(ErrorMessage = "le Nom  du Patient est Obligatoire ")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "le Prenom  du Patient est Obligatoire ")]

        public string Prenom { get; set; }
        [Required(ErrorMessage = "la Date de Naissance  du Patient est Obligatoire ")]

        public string DateNaissance { get; set; }
        [Required(ErrorMessage = "le Lieu de Naissance  du Patient est Obligatoire ")]

        public string LieuNaissance { get; set; }
        [Required(ErrorMessage = "le Type de Carte d'identter   du Patient est Obligatoire ")]

        public string TypeCarte { get; set; }
        [Required(ErrorMessage = "le Numero de la Carte  du Patient est Obligatoire ")]

        public string NumeroCarte { get; set; }
        [Required(ErrorMessage = "la Nationnalité  du Patient est Obligatoire ")]

        public string? Nationnalite { get; set; }
        [Required(ErrorMessage = "la Ville  du Patient est Obligatoire ")]

        public string? Ville { get; set; }
        [Required(ErrorMessage = "l'Adresse  du Patient est Obligatoire ")]

        public string? Adresse { get; set; }
        [Required(ErrorMessage = "le Telephone  du Patient est Obligatoire ")]

        public string Telephone { get; set; }
        [Required(ErrorMessage = "le Service d admission  du Patient est Obligatoire ")]
        public string? ReferencePatient { get; set; }
        [Required(ErrorMessage = "le Sexe est Obligatoire ")]
        public string Sexe { get; set; }
        [Required(ErrorMessage = "l'Ethnie est Obligatoire ")]
        public string Ethnie { get; set; }


        [Required(ErrorMessage = "la region Origine est Obligatoire ")]
        public string RegiondOrigine { get; set; }

     


        public DateTime? Date_Ajout { get; set; }
    }
}
