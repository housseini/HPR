namespace HPRBackend.Modules.Paramettres.GestionDesServices.Models
{
    public class ServiceEtage
    {
        public long Id { get; set; }
        public long NumeroEtage { get; set; }
        public long NombreChambre { get; set; }
        public long Nombrelit { get; set; }
        public long ServiceId { get; set; }
        public DateTime? Date_Ajout { get; set; }
    }
}
