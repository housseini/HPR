namespace HPRBackend.Modules.Gestion_Des_Patients.Models
{
    public class PreAdmission_Prestation
    {
        public long Id { get; set; }
        public long PreAdmissionId { get; set; }
        public long PrestationId { get; set; }
        public long Nombre { get; set; }
    }
}
