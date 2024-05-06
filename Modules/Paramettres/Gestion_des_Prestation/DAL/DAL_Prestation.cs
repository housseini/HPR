using HPRBackend.Modules.Paramettres.Gestion_des_Prestation.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.Gestion_des_Prestation.DAL
{
    public class DAL_Prestation
    {
        private readonly DataBaseContext ActeTraimentContext;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActeTraimentContext"></param>
        public DAL_Prestation(DataBaseContext ActeTraimentContext)
        {
            this.ActeTraimentContext = ActeTraimentContext;

        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<Prestation>?> GetAll()
        {
            await Migrations.create_table_Prestation();
            return await this.ActeTraimentContext.Prestation.ToListAsync();
        }


        public async Task<List<Prestation>?> GetAllByIdType(long Id)
        {
            await Migrations.create_table_Prestation();
            return await this.ActeTraimentContext.Prestation.Where(p => p.IdTypePrestation == Id).ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Prestation"></param>
        /// <returns></returns>
        public async Task<Message> Add(Prestation Prestation)
        {
            try
            {
                ActeTraimentContext.Prestation.Add(Prestation);
                await ActeTraimentContext.SaveChangesAsync();


                return new Message(true, "Prestation ajouter avec Succée ");

            }
            catch (Exception e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NomPrestation'"))
                {
                    return new Message(false, " le Nom de la Categorie Existe");


                }


                return new Message(false, e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Prestation"></param>
        /// <returns></returns>
        public async Task<Message> Update(Prestation Prestation)
        {
            try
            {
                ActeTraimentContext.Prestation.Update(Prestation);
                await ActeTraimentContext.SaveChangesAsync();


                return new Message(true, "Prestation ajouter avec Succée ");

            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NomPrestation'"))
                {
                    return new Message(false, " le Nom de la Categorie Existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_CodePrestation'"))
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

                var act = ActeTraimentContext.Prestation.FirstOrDefault(a => a.Id == id);
                if (act != null)
                {
                    ActeTraimentContext.Prestation.Remove(act);
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
