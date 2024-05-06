namespace HPRBackend.Modules.Gestion_Des_Patients.Models
{
    public class PriseEnCharge
    {
        public long Id { get; set; }
        //public int IdFacture { get; set; }
        public long PatientID { get; set; }
        public long CompagnieID { get; set; }
        public long Pourcentage { get; set; }
        public string DateExpiration { get; set; }
        public string NumeroPriseEnCharge { get; set; }
        public string LienParente { get; set; }
        public string? Path { get; set; }

    }
}
