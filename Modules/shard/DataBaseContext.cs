using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.Paramettres.Gestion_des_Prestation.Models;
using HPRBackend.Modules.Paramettres.GestionDesAgents.Models;
using HPRBackend.Modules.Paramettres.GestionDesAssurances.Models;
using HPRBackend.Modules.Paramettres.GestionDesServices.Models;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.shard
{
    public class DataBaseContext : DbContext
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
        {
        }

        public DbSet<Agent> Agent { get; set; } = default!;
        public DbSet<ServicesType> ServicesType { get; set; } = default!;
        public DbSet<Service> Service { get; set; } = default!;
        public DbSet<ServiceEtage> ServiceEtage { get; set; } = default!;
        public DbSet<PrixChambre> PrixChambre { get; set; } = default!;
        public DbSet<TypeChambre> TypeChambre { get; set; } = default!;
        public DbSet<Chambre> Chambre { get; set; } = default!;
        public DbSet<Assurance> Assurance { get; set; } = default!;
        public DbSet<MotifAdmission> MotifAdmission { get; set; } = default!;
        public DbSet<TypesPrestation> TypesPrestation { get; set; } = default!;
        public DbSet<Prestation> Prestation { get; set; } = default!;
        public DbSet<Patient> Patient { get; set; } = default!;
        public DbSet<PriseEnCharge> PriseEnCharge { get; set; } = default!;
        public DbSet<Admission> Admission { get; set; } = default!;
        public DbSet<PreAdmission> PreAdmission { get; set; } = default!;
        public DbSet<FactureAdmission> FactureAdmission { get; set; } = default!;   
        public DbSet<FacturePreAdmission> FacturePreAdmission { get; set; } = default!;
        public DbSet<Admission_Prestation> Admission_Prestation { get; set; } = default!;
        public DbSet<PreAdmission_Prestation> PreAdmission_Prestation { get; set; } = default!;
        public DbSet<PayementFacturePA> PayementFacturePA { get; set; } = default!;




    }
}
