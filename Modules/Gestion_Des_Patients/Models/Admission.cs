namespace HPRBackend.Modules.Gestion_Des_Patients.Models
{
    public class Admission
    {
        public string? TypeHopitale { get; set; }
        public long Id { get; set; }
        public long IdPatient { get; set; }
        public long IdService { get; set; }
        public long IdMotif { get; set; }
        public long IdMedecin { get; set; }
        public long IdAgent { get; set; }
        public string Date_Ajout { get; set; }
    }
}
