using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Gestion_Des_Patients.DAL
{
    public class DAL_PreAdmission
    {
        private readonly DataBaseContext DataBaseContext;


        public DAL_PreAdmission(DataBaseContext DataBaseContext)
        {
            this.DataBaseContext = DataBaseContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<PreAdmission>> GetAll()
        {
            await Migrations.create_table_PreAdmission();

            return await DataBaseContext.PreAdmission.ToListAsync();
        }
        /// <summary>
        /// ajouter Admission 
        /// </summary>
        /// <param name="Admission"></param>
        /// <returns></returns>
        public async Task<Message> Add(PreAdmission Patient)
        {
            try
            {


                this.DataBaseContext.PreAdmission.Add(Patient);
                await this.DataBaseContext.SaveChangesAsync();

                var Admission = this.DataBaseContext.PreAdmission.Where(p => p.Date_Ajout == Patient.Date_Ajout && p.IdPatient == Patient.IdPatient).First();

                return new Message(true, "le  PreAdmission ajouté avec succés , Identifiant :"+Admission.Id);
            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NumeroCarte_Patient'"))
                {
                    return new Message(false, " un Patient a ete ajouter avec  meme numero de la  carte merci de verifiez s'il s agit du meme patient ");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Telephone_Patient'"))
                {
                    return new Message(false, " un Patient a ete ajouter avec  meme numero de telephone mercie verifiez s'il s agit du meme patient");


                }

                return new Message(false, e.Message);
            }


        }
        /// <summary>
        /// update Admission 
        /// </summary>
        /// <param name="Admission"></param>
        /// <returns></returns>
        public async Task<Message> Update(PreAdmission Patient)
        {
            try
            {

                this.DataBaseContext.PreAdmission.Update(Patient);
                await this.DataBaseContext.SaveChangesAsync();
                return new Message(true, "Patient modifié avec succés ");
            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_NumeroCarte_Patient'"))
                {
                    return new Message(false, " un Patient a ete ajouter avec  meme numero de la  carte merci de verifiez s'il s agit du meme patient ");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Telephone_Patient'"))
                {
                    return new Message(false, " un Patient a ete ajouter avec  meme numero de telephone mercie verifiez s'il s agit du meme patient");


                }

                return new Message(false, e.Message);
            }


        }
        /// <summary>
        /// renvoie un  Admission selon son Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<PreAdmission?> GetById(long Id)
        {



            var Patient = await this.DataBaseContext.PreAdmission.FirstOrDefaultAsync(s => s.Id == Id);
            if (Patient != null)
            {



                return Patient;
            }
            else
            {
                return null;

            }





        }
        /// <summary>
        /// supprime un Admission selon son Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Message> Delete(long Id)
        {
            try
            {
                var m = await GetById(Id);
                if (m != null)
                {
                    this.DataBaseContext.PreAdmission.Remove(m);
                    this.DataBaseContext.SaveChanges();

                }

                return new Message(true, "  Patient(e) supprimé(e) avec succes ");


            }
            catch (DbUpdateException e)
            {



                return new Message(false, "erreur de suppression " + e.Message);
            }


        }
    }
}
