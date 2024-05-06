namespace HPRBackend.Modules.Gestion_Des_Patients.Models
{
    public class Admission_Prestation
    {
        public long Id { get; set; }    
        public long AdmissionId { get; set;}
        public long PrestationId { get; set;}
        public long Nombre { get; set; }
    }
}
