namespace HPRBackend.Modules.Gestion_Des_Patients.Models
{
    public class FacturePreAdmission
    {
        public long Id { get; set; }
        public long IdPrisEncharge { get; set; }
        public long IdPreAdmission { get; set; }
        public long PEC { get; set; }
        public string? ReferenceFacture { get; set; }
        public float MontantTotale { get; set; }
        public float MontantPatient { get; set; }
        public float MontantPriseEncharge { get; set; }
        public string Date_Ajout { get; set; }
    }
}
