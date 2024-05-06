using HPRBackend.Modules.Paramettres.GestionDesServices.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.GestionDesServices.DAL
{
    public class DAL_ServicesType
    {
        public readonly DataBaseContext serviceContext;
        public DAL_ServicesType(DataBaseContext serviceContext)
        {
            this.serviceContext = serviceContext;
        }
        /// <summary>
        /// AJOUTER UNE TYPE DE SERVICE type
        /// </summary>
        /// <param name="servicesType"></param>
        /// <returns></returns>
        public async Task<Message> Add(ServicesType servicesType)
        {
            try
            {


                this.serviceContext.ServicesType.Add(servicesType);
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
        /// <param name="servicesType"></param>
        /// <returns></returns>
        public async Task<Message> Update(ServicesType servicesType)
        {
            try
            {


                this.serviceContext.ServicesType.Update(servicesType);
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


                var servicesType = await this.serviceContext.ServicesType.FirstOrDefaultAsync(s => s.Id == Id);
                if (servicesType != null)
                {


                    this.serviceContext.ServicesType.Remove(servicesType);
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
        public async Task<ServicesType?> GetById(long Id)
        {



            var servicesType = await this.serviceContext.ServicesType.FirstOrDefaultAsync(s => s.Id == Id);
            if (servicesType != null)
            {



                return servicesType;
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
        public async Task<List<ServicesType>> GetAll()
        {
            await Migrations.create_table_ServicesType();
            return await this.serviceContext.ServicesType.ToListAsync();
        }
     
    }
}
