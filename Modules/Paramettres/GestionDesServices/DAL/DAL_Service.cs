using HPRBackend.Modules.Paramettres.GestionDesServices.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.GestionDesServices.DAL
{
    public class DAL_Service
    {
        public readonly DataBaseContext serviceContext;
        public DAL_Service(DataBaseContext serviceContext)
        {
            this.serviceContext = serviceContext;
        }
        /// <summary>
        /// AJOUTER UNE TYPE DE SERVICE type
        /// </summary>
        /// <param name="Service"></param>
        /// <returns></returns>
        public async Task<Message> Add(Service Service)
        {
            try
            {


                this.serviceContext.Service.Add(Service);
                await this.serviceContext.SaveChangesAsync();
                return new Message(true, "le type de service ajouté avec succés");
            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Code'"))
                {
                    return new Message(false, " le Code  existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Type'"))
                {
                    return new Message(false, " le Type existe");


                }

                return new Message(true, e.Message);
            }


        }
        /// <summary>
        /// Mettre ajours un service type
        /// </summary>
        /// <param name="Service"></param>
        /// <returns></returns>
        public async Task<Message> Update(Service Service)
        {
            try
            {


                this.serviceContext.Service.Update(Service);
                await this.serviceContext.SaveChangesAsync();
                return new Message(true, "le type de service modifié avec succés");
            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Code'"))
                {
                    return new Message(false, " le Code  existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Type'"))
                {
                    return new Message(false, " le Type existe");


                }

                return new Message(true, e.Message);
            }

        }
        /// <summary>
        /// Supprimer un type de service selon un Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Message> DeleteById(long Id)
        {
            try
            {


                var Service = await this.serviceContext.Service.FirstOrDefaultAsync(s => s.Id == Id);
                if (Service != null)
                {


                    this.serviceContext.Service.Remove(Service);
                    await this.serviceContext.SaveChangesAsync();
                    return new Message(true, "le type de service supprimé avec succés");
                }
                else
                {
                    return new Message(false, " aucun  type de service  trouvé");

                }


            }
            catch (Exception e)
            {


                return new Message(false, e.Message);
            }

        }

        /// <summary>
        /// retourner un type de service selon son Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Service?> GetById(long Id)
        {



            var Service = await this.serviceContext.Service.FirstOrDefaultAsync(s => s.Id == Id);
            if (Service != null)
            {



                return Service;
            }
            else
            {
                return null;

            }





        }

        /// <summary>
        /// retourner la liste des types de servic
        /// </summary>
        /// <returns></returns>
        public async Task<List<Service>> GetAll()
        {
            await Migrations.create_table_Service();
            return await this.serviceContext.Service.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Service"></param>
        /// <returns></returns>
        public async Task<Message> UpdateFile(Service Service)
        {
            try
            {



                this.serviceContext.Service.Update(Service);
                await this.serviceContext.SaveChangesAsync();
                return new Message(true, "    service modifié avec succés");

            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Service_Nom'"))
                {
                    return new Message(false, " un service du meme nom existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Service_MedecinId'"))
                {
                    return new Message(false, " Ce medecin est deja responsable d'un autre service ");


                }

                return new Message(false, e.Message);
            }

        }
    }
}
