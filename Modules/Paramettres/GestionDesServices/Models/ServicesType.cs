using System.ComponentModel.DataAnnotations;

namespace HPRBackend.Modules.Paramettres.GestionDesServices.Models
{
    public class ServicesType
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "le Type du service est Obligatoire ")]

        public string Type { get; set; }
        [Required(ErrorMessage = "le Nom  du service est Obligatoire ")]
        public DateTime? Date_Ajout { get; set; }
    }
}
