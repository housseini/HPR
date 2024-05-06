namespace HPRBackend.Modules.Gestion_Des_Patients.Models
{
    public class PreAdmission
    {
        public long Id { get; set; }
        public long IdPatient { get; set; }
        public long IdAgent { get; set; }
        public string Date_Ajout { get; set; }
    }
}
