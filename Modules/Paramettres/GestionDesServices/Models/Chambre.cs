namespace HPRBackend.Modules.Paramettres.GestionDesServices.Models
{
    public class Chambre
    {
        public long Id { get; set; }
        public long EtageServiceId { get; set; }
        public long TypeChambreId { get; set; }

        public long NumeroChambre { get; set; }
        public long NumeroLit { get; set; }
        public bool Etat { get; set; } = true;
        public bool Status { get; set; } = false;
    }
}
