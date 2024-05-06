using HPRBackend.Modules.Paramettres.GestionDesServices.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.GestionDesServices.DAL
{
    public class DAL_Chambre
    {
        public readonly DataBaseContext serviceContext;
        public DAL_ServiceEtage DAL_ServiceEtage;
        public DAL_Chambre(DataBaseContext serviceContext1)
        {
            this.serviceContext = serviceContext1;
            this.DAL_ServiceEtage=new DAL_ServiceEtage(serviceContext1);
        }

        public async Task<List<Chambre>?> GetAll()
        {
            try
            {
                await Migrations.create_table_Chambre();
                var cses = await this.serviceContext.Chambre.ToListAsync();


                    //from c in this.serviceContext.Chambre
                    //       join se in this.serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
                    //       join s in this.serviceContext.Service on se.ServiceId equals s.Id

                //       select new
                //       {
                //           Chambre = c,

                //           ServiceEtage = se,
                //           Service = s




                //       };
                return cses;
            }
            catch
            {
                return null;
            }
        }


        public async Task<Message> Add(Chambre Chambre)
        {
            try
            {
                if (existe(Chambre.EtageServiceId, Chambre.NumeroChambre, Chambre.NumeroLit))
                {
                    return new Message(false, " cette chambre exite deja");
                }
                else
                {
                    Chambre.Status = false;
                    this.serviceContext.Chambre.Add(Chambre);
                    this.serviceContext.SaveChanges();
                    var etageId = Chambre.EtageServiceId;

                    var nombreChambre = this.serviceContext.Chambre.Count(c => c.EtageServiceId == etageId);
                    var nombreLit = this.serviceContext.Chambre.Where(c => c.EtageServiceId == etageId).ToList().Count + 1; // Si NumeroLit représente le nombre de lits dans la chambre

                    var etage = this.serviceContext.ServiceEtage.Find(etageId);
                    if (etage != null)
                    {
                        etage.NombreChambre = nombreChambre;
                        etage.Nombrelit = nombreLit;

                        this.serviceContext.SaveChanges();
                    }
                    return new Message(true, "chambre ajouté  avec succées  ");

                }

            }
            catch (Exception ex)
            {
                return new Message(false, "erreur :" + ex.Message);
            }
        }



        public async Task<Message> UpdateServiceEtage(long ServiceId)
        {

            var service = await DAL_ServiceEtage.GetById2(ServiceId);
            if (service != null)
            {
                service.NombreChambre = Count(ServiceId);

                return await DAL_ServiceEtage.Update(service);

            }
            else
            {
                return new Message(false, "  erreur ");

            }




        }


        public int Count(long EtageServiceId)
        {
            return serviceContext.Chambre.Where(s => s.EtageServiceId == EtageServiceId).ToList().Count();

        }
        public async Task<object?> GetAllPrix()
        {
            try
            {
                await  Migrations.create_table_Chambre();
                var cses = from c in serviceContext.Chambre

                           join se in serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
                           join s in serviceContext.Service on se.ServiceId equals s.Id
                           join ch in serviceContext.PrixChambre on c.Id equals ch.ChambreId

                           select new
                           {
                               Chambre = c,

                               ServiceEtage = se,
                               Service = s,
                               PrixChambre = ch,




                           };
                return cses;
            }
            catch
            {
                return null;
            }
        }
        public async Task<object?> GetAllByIdservice(long Idservice)
        {
            try
            {
                var cses = from c in serviceContext.Chambre

                           join se in serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
                           join s in serviceContext.Service on se.ServiceId equals s.Id

                           select new
                           {
                               Chambre = c,

                               ServiceEtage = se,
                               Service = s





                           };
                return cses;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Chambre?> GetById(long Id)
        {
            try
            {
                var cses = serviceContext.Chambre.FirstOrDefault(C => C.Id == Id);
                return cses;
            }
            catch
            {
                return null;
            }
        }

        public async Task<object?> GetAllByIdserviceLivre(long Idservice)
        {
            try
            {
                var cses = from c in serviceContext.Chambre

                           join se in serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
                           join s in serviceContext.Service on se.ServiceId equals s.Id
                           where s.Id == Idservice && c.Etat == true && c.Status == false
                           select new
                           {
                               Chambre = c,

                               ServiceEtage = se,
                               Service = s,




                           };
                return cses;
            }
            catch
            {
                return null;
            }
        }
        public async Task<object?> GetAllByIdserviceEtage(long IdserviceEtage)
        {
            try
            {
                var cses = from c in serviceContext.Chambre

                           join se in serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
                           join s in serviceContext.Service on se.ServiceId equals s.Id
                           join ty in serviceContext.TypeChambre on c.TypeChambreId equals ty.Id
                           where se.Id == IdserviceEtage
                           select new
                           {
                               Chambre = c,

                               ServiceEtage = se,
                               Service = s,




                           };
                return cses;
            }
            catch
            {
                return null;
            }
        }

        public async Task<object?> GetAllChambreLibreByIdserviceEtage(long IdserviceEtage)
        {
            try
            {
                var cses = from c in serviceContext.Chambre

                           join se in serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
                           join s in serviceContext.Service on se.ServiceId equals s.Id
                           join ty in serviceContext.TypeChambre on c.TypeChambreId equals ty.Id
                           where se.Id == IdserviceEtage && c.Etat == true && c.Status == false
                           select new
                           {
                               Chambre = c,

                               ServiceEtage = se,
                               Service = s,




                           };
                return cses;
            }
            catch
            {
                return null;
            }
        }

        //public async Task<object?> GetAllChambreOccupeByIdserviceEtage(long IdserviceEtage)
        //{
        //    try
        //    {
        //        var cses = from c in serviceContext.Chambre
        //                   join h in serviceContext.Hospitalisation on c.Id equals h.litId
        //                   join p in serviceContext.Patient on h.PatientId equals p.Id
        //                   join se in serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
        //                   where se.Id == IdserviceEtage && c.Etat == true && c.Status == true && h.Status.Equals("EN Cours ")
        //                   select new
        //                   {
        //                       Chambre = c,
        //                       Patient = p,





        //                   };
        //        return cses;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}


        public async Task<object?> GetAllByIdserviceEtageAndNumeroEtage(long IdserviceEtage, long NumeroEtage)
        {
            try
            {
                var cses = from c in serviceContext.Chambre

                           join se in serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
                           join s in serviceContext.Service on se.ServiceId equals s.Id
                           where se.Id == IdserviceEtage && se.NumeroEtage == NumeroEtage
                           select new
                           {
                               Chambre = c,

                               ServiceEtage = se,
                               Service = s,




                           };
                return cses;
            }
            catch
            {
                return null;
            }
        }
        public async Task<object?> GetAllByIdserviceEtageAndNumeroEtageAndNumeroChambre(long IdserviceEtage, long NumeroEtage, long NumeroChambre)
        {
            try
            {
                var cses = from c in serviceContext.Chambre

                           join se in serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
                           join s in serviceContext.Service on se.ServiceId equals s.Id
                           where se.Id == IdserviceEtage && se.NumeroEtage == NumeroEtage && c.NumeroChambre == NumeroChambre
                           select new
                           {
                               Chambre = c,

                               ServiceEtage = se,
                               Service = s,




                           };
                return cses;
            }
            catch
            {
                return null;
            }
        }
        public async Task<object?> GetAllByIdserviceEtageAndNumeroEtageAndNumeroChambreAndNumeroLit(long IdserviceEtage, long NumeroEtage, long NumeroChambre, long NumeroLit)
        {
            try
            {
                var cses = from c in serviceContext.Chambre

                           join se in serviceContext.ServiceEtage on c.EtageServiceId equals se.Id
                           join s in serviceContext.Service on se.ServiceId equals s.Id
                           where se.Id == IdserviceEtage && se.NumeroEtage == NumeroEtage && c.NumeroChambre == NumeroChambre && c.NumeroLit == NumeroLit
                           select new
                           {
                               Chambre = c,

                               ServiceEtage = se,
                               Service = s,




                           };
                return cses;
            }
            catch
            {
                return null;
            }
        }


        public async Task<Message> DecommissionByIdchambre(long Id)
        {

            try
            {
                var c = await serviceContext.Chambre.FirstOrDefaultAsync(c => c.Id == Id);
                c.Etat = false;
                serviceContext.Chambre.Update(c);
                await serviceContext.SaveChangesAsync();
                return new Message(true, "chambre modifie avec succées  ");
            }
            catch
            {
                return new Message(false, "une erreur est survenue  lors de la mise à jours ");
            }


        }


        public async Task<Message> DecommissionByEtageServiceId(long EtageServiceId)
        {

            try
            {
                var c = serviceContext.Chambre.Where(c => c.EtageServiceId == EtageServiceId).ToList();
                for (int i = 0; i < c.Count; i++)
                {
                    c[i].Etat = false;
                }


                serviceContext.Chambre.UpdateRange(c);
                await serviceContext.SaveChangesAsync();
                return new Message(true, "chambres modifie avec succées  ");
            }
            catch
            {
                return new Message(false, "une erreur est survenue  lors de la mise à jours ");
            }


        }

        public async Task<Message> reclassifyByIdchambre(long Id)
        {

            try
            {
                var c = await serviceContext.Chambre.FirstOrDefaultAsync(c => c.Id == Id);
                c.Etat = true;
                serviceContext.Chambre.Update(c);
                await serviceContext.SaveChangesAsync();
                return new Message(true, "chambre modifie avec succées  ");
            }
            catch
            {
                return new Message(false, "une erreur est survenue  lors de la mise à jours ");
            }


        }
        public async Task<Message> OccupéeByIdchambre(long Id)
        {

            try
            {
                var c = serviceContext.Chambre.Where(c => c.Id == Id).ToList()[0];
                c.Status = true;
                c.Etat = true;
                serviceContext.Chambre.Update(c);
                serviceContext.SaveChanges();
                return new Message(true, "chambre modifie avec succées  ");
            }
            catch
            {
                return new Message(false, "une erreur est survenue  lors de la mise à jours ");
            }


        }
        public async Task<Message> livréeByIdchambre(long Id)
        {

            try
            {
                var c = await serviceContext.Chambre.FirstOrDefaultAsync(c => c.Id == Id);
                c.Status = false;
                c.Etat = false;
                serviceContext.Chambre.Update(c);
                await serviceContext.SaveChangesAsync();


                return new Message(true, "chambre modifie avec succées  ");
            }
            catch
            {
                return new Message(false, "une erreur est survenue  lors de la mise à jours ");
            }


        }


        public async Task<Message> reclassifyByEtageServiceId(long EtageServiceId)
        {

            try
            {
                var c = serviceContext.Chambre.Where(c => c.EtageServiceId == EtageServiceId).ToList();
                for (int i = 0; i < c.Count; i++)
                {
                    c[i].Etat = true;
                }


                serviceContext.Chambre.UpdateRange(c);
                await serviceContext.SaveChangesAsync();
                return new Message(true, "chambres modifie avec succées  ");
            }
            catch
            {
                return new Message(false, "une erreur est survenue  lors de la mise à jours ");
            }


        }



        public Boolean existe(long EtageServiceId, long NumeroChambre, long NumeroLit)
        {
            var servicetege = serviceContext.Chambre.Where(s => s.EtageServiceId == EtageServiceId && s.NumeroChambre == NumeroChambre

             && s.NumeroLit == NumeroLit
            ).ToList();
            if (servicetege.Count > 0)
                return true;
            else
                return false;

        }



        public Object? GetAllChambre_Type(long IdEtage)
        {



            var result = from chambre in serviceContext.Chambre
                         join typeChambre in serviceContext.TypeChambre on chambre.TypeChambreId equals typeChambre.Id
                         where chambre.EtageServiceId == IdEtage
                         group typeChambre by typeChambre.Nom into groupedTypes
                         select new
                         {
                             TypeChambre = groupedTypes.Key,
                             NombreChambres = groupedTypes.Count()
                         };

            return result;



        }
    }
}
