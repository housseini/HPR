using HPRBackend.Modules.Paramettres.Gestion_des_Prestation.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.Gestion_des_Prestation.DAL
{
    public class DAL_TypesPrestation
    {
        private readonly DataBaseContext ActeTraimentContext;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActeTraimentContext"></param>
        public DAL_TypesPrestation(DataBaseContext ActeTraimentContext)
        {
            this.ActeTraimentContext = ActeTraimentContext;

        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<TypesPrestation>?> GetAll()
        {
            await Migrations.create_table_TypesPrestation();
            return await this.ActeTraimentContext.TypesPrestation.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypesPrestation"></param>
        /// <returns></returns>
        public async Task<Message> Add(TypesPrestation TypesPrestation)
        {
            try
            {
                ActeTraimentContext.TypesPrestation.Add(TypesPrestation);
                await ActeTraimentContext.SaveChangesAsync();


                return new Message(true, "TypesPrestation ajouter avec Succée ");

            }
            catch (Exception e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NomTypesPrestation'"))
                {
                    return new Message(false, " le Nom de la Categorie Existe");


                }


                return new Message(false, e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypesPrestation"></param>
        /// <returns></returns>
        public async Task<Message> Update(TypesPrestation TypesPrestation)
        {
            try
            {
                ActeTraimentContext.TypesPrestation.Update(TypesPrestation);
                await ActeTraimentContext.SaveChangesAsync();


                return new Message(true, "TypesPrestation ajouter avec Succée ");

            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NomTypesPrestation'"))
                {
                    return new Message(false, " le Nom de la Categorie Existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_CodeTypesPrestation'"))
                {
                    return new Message(false, " le code est deja enregitré pour un autre act ");


                }

                return new Message(false, e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Message> Delete(long id)
        {
            try
            {

                var act = ActeTraimentContext.TypesPrestation.FirstOrDefault(a => a.Id == id);
                if (act != null)
                {
                    ActeTraimentContext.TypesPrestation.Remove(act);
                }

                await ActeTraimentContext.SaveChangesAsync();
                return new Message(true, "Acte Medical Categorie Supprimé avec succé");


            }
            catch (Exception e)
            {

                return new Message(false, e.Message);

            }
        }
    }
}
