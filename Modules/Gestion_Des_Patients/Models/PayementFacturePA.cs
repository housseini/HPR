namespace HPRBackend.Modules.Gestion_Des_Patients.Models
{
    public class PayementFacturePA
    {
        public long Id { get; set; }
        public long IdFactureAdmission { get; set; }
        public long IdAgent  { get; set; }
        public string? ReferencePayement { get; set; }
        public string? ModePayement { get; set; }
        public float MontantTotalePayer { get; set; }
        public float MontantRestantPayer { get; set; }
        public string Date_Ajout { get; set; }
    }
}
