using HPRBackend.Modules.Paramettres.Gestion_des_Prestation.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Paramettres.Gestion_des_Prestation.DAL
{
    public class DAL_MotifAdmission
    {
        private readonly DataBaseContext ActeTraimentContext;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ActeTraimentContext"></param>
        public DAL_MotifAdmission(DataBaseContext ActeTraimentContext)
        {
            this.ActeTraimentContext = ActeTraimentContext;

        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<MotifAdmission>?> GetAll()
        {
            await Migrations.create_table_MotifAdmission();
            return await this.ActeTraimentContext.MotifAdmission.ToListAsync();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="MotifAdmission"></param>
        /// <returns></returns>
        public async Task<Message> Add(MotifAdmission MotifAdmission)
        {
            try
            {
                ActeTraimentContext.MotifAdmission.Add(MotifAdmission);
                await ActeTraimentContext.SaveChangesAsync();


                return new Message(true, "MotifAdmission ajouter avec Succée ");

            }
            catch (Exception e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NomMotifAdmission'"))
                {
                    return new Message(false, " le Nom de la Categorie Existe");


                }


                return new Message(false, e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MotifAdmission"></param>
        /// <returns></returns>
        public async Task<Message> Update(MotifAdmission MotifAdmission)
        {
            try
            {
                ActeTraimentContext.MotifAdmission.Update(MotifAdmission);
                await ActeTraimentContext.SaveChangesAsync();


                return new Message(true, "MotifAdmission ajouter avec Succée ");

            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NomMotifAdmission'"))
                {
                    return new Message(false, " le Nom de la Categorie Existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_CodeMotifAdmission'"))
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

                var act = ActeTraimentContext.MotifAdmission.FirstOrDefault(a => a.Id == id);
                if (act != null)
                {
                    ActeTraimentContext.MotifAdmission.Remove(act);
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
