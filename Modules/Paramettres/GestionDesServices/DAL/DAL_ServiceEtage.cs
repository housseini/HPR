using HPRBackend.Modules.Paramettres.GestionDesServices.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.GestionDesServices.DAL
{
    public class DAL_ServiceEtage
    {   /// <summary>
        /// 
        /// </summary>
        public readonly DataBaseContext serviceContext;
        public readonly DAL_Service dal_service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContext"></param>
        public DAL_ServiceEtage(DataBaseContext serviceContext)
        {
            this.serviceContext = serviceContext;
            this.dal_service = new DAL_Service(serviceContext);
        }
        /// <summary>
        /// recherche Service etage selon l Id de service donnée 
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <returns></returns>

        public List<ServiceEtage>? RechercheServiceEtageById(long ServiceId)
        {
            var ServiceEtage = serviceContext.ServiceEtage.Where(se => se.ServiceId == ServiceId).ToList();
            if (ServiceEtage != null)
            {
                return ServiceEtage;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <param name="NumeroEtage"></param>
        /// <returns></returns>
        public async Task<Message> Add(long ServiceId, long NumeroEtage)
        {
            try
            {
                await Migrations.create_table_ServiceEtage();
                if (existe(ServiceId, NumeroEtage) == true)
                {
                    return new Message(false, " Cet etage exite deja");

                }
                else
                {

                    ServiceEtage etage = new ServiceEtage();
                    etage.ServiceId = ServiceId;
                    etage.NumeroEtage = NumeroEtage;
                    etage.NombreChambre = 0;
                    etage.Nombrelit = 0;
                    TimeZoneInfo gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    etage.Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1);
                    serviceContext.ServiceEtage.Add(etage);
                    serviceContext.SaveChanges();

                    await UpdateService(ServiceId);


                    return new Message(true, " les etages sont ajoutés avec succés");


                }



            }
            catch (Exception ex)
            {
                return new Message(false, "  erreur " + ex.Message);

            }




        }



        public async Task<Message> Delete(long ServiceId)
        {

            var servietage = await GetById2(ServiceId);
            if (servietage != null)
            {
                serviceContext.ServiceEtage.Remove(servietage);
                serviceContext.SaveChanges();
            }
            return new Message(true, "  serviceEtage supprimée AVEC SUCCées  ");

        }
        public long Count(long ServiceId)
        {
            return serviceContext.ServiceEtage.Where(s => s.ServiceId == ServiceId).ToList().Count();
        }


        public async Task<Message> UpdateService(long ServiceId)
        {

            var service = await dal_service.GetById(ServiceId);
            if (service != null)
            {
                service.NombreEtage = Count(ServiceId);
                return await dal_service.Update(service);

            }
            else
            {
                return new Message(false, "  erreur ");

            }




        }



        public async Task<Message> Update(ServiceEtage serviceEtage)
        {
            serviceContext.ServiceEtage.Update(serviceEtage);
            serviceContext.SaveChanges();


            return new Message(true, "  serviceEtage MODIFIER AVEC SUCCées  ");






        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <returns></returns>
        public Boolean existe(long ServiceId, long NumeroEtage)
        {
            var servicetege = serviceContext.ServiceEtage.Where(s => s.ServiceId == ServiceId && s.NumeroEtage == NumeroEtage).ToList();
            if (servicetege.Count > 0)
                return true;
            else
                return false;

        }

        /// <summary>
        /// service etage liste 
        /// </summary>
        /// <returns></returns>
        public async Task<object?> Getall()
        {
            try
            {
                await Migrations.create_table_ServiceEtage();

                var affectations = from m in serviceContext.ServiceEtage

                                   join s in serviceContext.Service on m.ServiceId equals s.Id

                                   select new
                                   {
                                       ServiceEtage = m,
                                       Service = s




                                   };
                return affectations;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// renvoie le service etage selon l Id de l etage passé 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<object?> GetById(long Id)
        {
            try
            {
                var affectations = from m in serviceContext.ServiceEtage
                                   where m.Id == Id
                                   join s in serviceContext.Service on m.ServiceId equals s.Id

                                   select new
                                   {
                                       ServiceEtage = m,
                                       Service = s




                                   };
                return affectations;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ServiceEtage>?> GetByServiceId(long ServiceId)
        {
            try
            {
                var affectations = await serviceContext.ServiceEtage.Where(p => p.ServiceId == ServiceId).ToListAsync();




                return affectations;
            }
            catch
            {
                return null;
            }
        }

        public async Task<object?> GetById1(long Id)
        {
            try
            {
                var affectations = serviceContext.ServiceEtage.FirstOrDefault(s => s.Id == Id);

                return affectations;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ServiceEtage?> GetById2(long Id)
        {
            try
            {
                var affectations = serviceContext.ServiceEtage.Where(s => s.Id == Id).ToList()[0];

                return affectations;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// renvoie le service etage selon l Id du service passé 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public async Task<object?> GetService_ServiceEtageByIdservice(long Id)
        {
            try
            {
                var affectations = from m in serviceContext.ServiceEtage

                                   join s in serviceContext.Service on m.ServiceId equals s.Id
                                   where s.Id == Id
                                   select new
                                   {
                                       ServiceEtage = m,
                                       Service = s




                                   };
                return affectations;
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, long> CountsServiceEtagetByIdService(long ServiceId)
        {
            var keyValuePairs = new Dictionary<string, long>();

            keyValuePairs.Add("Total Etage", serviceContext.ServiceEtage.Where(a => a.ServiceId == ServiceId).Count());
            keyValuePairs.Add("Total chambre", serviceContext.ServiceEtage.Where(a => a.ServiceId == ServiceId).Sum(a => a.NombreChambre));
            return keyValuePairs;
        }
        //public Dictionary<string, long> CountsServicetByIdServiceEtage(long ServiceId)
        //{
        //    var keyValuePairs = new Dictionary<string, long>();

        //    keyValuePairs.Add("Nombre Total de lit ", serviceContext.Chambre.Where(a => a.EtageServiceId == ServiceId).Count());
        //    keyValuePairs.Add("Nombre Total de occupés  ", serviceContext.Chambre.Where(a => a.EtageServiceId == ServiceId && a.Etat == true && a.Status == true).Count());
        //    keyValuePairs.Add("Nombre Total de livre  ", serviceContext.Chambre.Where(a => a.EtageServiceId == ServiceId && a.Etat == true && a.Status == false).Count());
        //    keyValuePairs.Add("Nombre Total de lit nettoyer   ", serviceContext.Chambre.Where(a => a.EtageServiceId == ServiceId && a.Etat == false && a.Status == false).Count());

        //    return keyValuePairs;
        //}
    }
}
