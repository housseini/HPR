namespace HPRBackend.Modules.Paramettres.Gestion_des_Prestation.Models
{
    public class Prestation
    {
        public long Id { get; set; }
        public long IdTypePrestation { get; set; }
        public long TVA { get; set; }
        public float Prix { get; set; }
        public string Nom { get; set; }
    }
}
