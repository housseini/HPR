using HPRBackend.Modules.shard;
using HPRBackend.Modules.Paramettres.GestionDesServices.Models;

namespace HPRBackend.Modules.Paramettres.GestionDesServices.DAL
{
    public class DAL_PrixChambre
    {
        public readonly DataBaseContext serviceContext;
        public DAL_PrixChambre(DataBaseContext serviceContext)
        {
            this.serviceContext = serviceContext;
        }
        /// <summary>
        /// renvoie tous les Prix par chambre 
        /// </summary>
        /// <returns></returns>
        public async Task<object?> GetAll()
        {
            try
            {
                await Migrations.create_table_Chambre();

                var cses = from p in serviceContext.PrixChambre
                           join c in serviceContext.Chambre on p.ChambreId equals c.Id
                           select new
                           {
                               Chambre = c,
                               PrixChambre = p




                           };
                return cses;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// ajouter le prix du chambre selon son Id 
        /// </summary>
        /// <param name="ChambreId"></param>
        /// <param name="Prix"></param>
        /// <returns></returns>
        public async Task<Message> AddByChambreId(long ChambreId, long Prix)
        {

            try
            {
                PrixChambre PrixChambre = new PrixChambre();
                PrixChambre.ChambreId = ChambreId;
                PrixChambre.PrixJours = Prix;
                serviceContext.PrixChambre.Add(PrixChambre);
                serviceContext.SaveChanges();
                return new Message(true, "chambre modifie avec succées  ");
            }
            catch
            {
                return new Message(false, "une erreur est survenue  lors de la mise à jours ");
            }


        }

        /// <summary>
        /// ajouter le prix des toute les chambre d un Etage 
        /// </summary>
        /// <param name="EtageServiceId"></param>
        /// <param name="Prix"></param>
        /// <returns></returns>
        public async Task<Message> AddByEtageServiceId(long EtageServiceId, long Prix)
        {

            try
            {
                var serviceEtage = serviceContext.Chambre.Where(se =>

                    se.EtageServiceId == EtageServiceId

                ).ToList();

                foreach (var chambres in serviceEtage)
                {
                    PrixChambre PrixChambre = new PrixChambre();
                    PrixChambre.ChambreId = chambres.Id;
                    PrixChambre.PrixJours = Prix;
                    serviceContext.PrixChambre.Add(PrixChambre);
                    serviceContext.SaveChanges();
                }



                return new Message(true, "chambre modifie avec succées  ");
            }
            catch
            {
                return new Message(false, "une erreur est survenue  lors de la mise à jours ");
            }


        }
        /// <summary>
        /// ajoute le prix des toute les chambre selon Id du service 
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <param name="Prix"></param>
        /// <returns></returns>
        public async Task<Message> AddByServiceId(long ServiceId, long Prix)
        {

            try
            {
                var Services = serviceContext.Service.Where(s => s.Id == ServiceId).ToList();


                foreach (var service in Services)
                {

                    var serviceEtage = serviceContext.Chambre.Where(se =>

                   se.EtageServiceId == service.Id

                    ).ToList();
                    foreach (var chambres in serviceEtage)
                    {
                        PrixChambre PrixChambre = new PrixChambre();
                        PrixChambre.ChambreId = chambres.Id;
                        PrixChambre.PrixJours = Prix;
                        serviceContext.PrixChambre.Add(PrixChambre);
                        serviceContext.SaveChanges();
                    }
                }







                return new Message(true, "prix ajouter  avec succées  ");
            }
            catch
            {
                return new Message(false, "une erreur est survenue  lors de la mise à jours ");
            }


        }
    }
}
