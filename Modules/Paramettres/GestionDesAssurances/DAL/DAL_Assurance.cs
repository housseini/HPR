



using HPRBackend.Modules.Paramettres.GestionDesAssurances.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.GestionDesAssurances.DAL
{
    public class DAL_Assurance
    {
        private readonly DataBaseContext ActeTraimentContext;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActeTraimentContext"></param>
        public DAL_Assurance(DataBaseContext ActeTraimentContext)
        {
            this.ActeTraimentContext = ActeTraimentContext;

        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<Assurance>?> GetAll()
        {
            await Migrations.create_table_Assurance();
            return await this.ActeTraimentContext.Assurance.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Assurance"></param>
        /// <returns></returns>
        public async Task<Message> Add(Assurance Assurance)
        {
            try
            {
                ActeTraimentContext.Assurance.Add(Assurance);
                await ActeTraimentContext.SaveChangesAsync();


                return new Message(true, "Assurance ajouter avec Succée ");

            }
            catch (Exception e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NomAssurance'"))
                {
                    return new Message(false, " le Nom de la Categorie Existe");


                }


                return new Message(false, e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Assurance"></param>
        /// <returns></returns>
        public async Task<Message> Update(Assurance Assurance)
        {
            try
            {
                ActeTraimentContext.Assurance.Update(Assurance);
                await ActeTraimentContext.SaveChangesAsync();


                return new Message(true, "Assurance ajouter avec Succée ");

            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NomAssurance'"))
                {
                    return new Message(false, " le Nom de la Categorie Existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_CodeAssurance'"))
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

                var act = ActeTraimentContext.Assurance.FirstOrDefault(a => a.Id == id);
                if (act != null)
                {
                    ActeTraimentContext.Assurance.Remove(act);
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
