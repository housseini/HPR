using System.ComponentModel.DataAnnotations;

namespace HPRBackend.Modules.Paramettres.GestionDesServices.Models
{
    public class Service
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "le Nom  du service est Obligatoire ")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "l' Emplacement  du service est Obligatoire ")]
        public string Emplacement { get; set; }
        [Required(ErrorMessage = "le Nombre d'Etage  du service est Obligatoire ")]
        public long NombreEtage { get; set; }
        [Required(ErrorMessage = "le responsable est obligatoire du service est Obligatoire ")]
        public long MedecinId { get; set; }
        [Required(ErrorMessage = "le Type du service est Obligatoire ")]
        public string Type { get; set; }
        public DateTime? Date_Ajout { get; set; }

    }
}
