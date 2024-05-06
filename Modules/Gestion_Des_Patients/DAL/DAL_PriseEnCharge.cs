using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Gestion_Des_Patients.DAL
{
    public class DAL_PriseEnCharge
    {
        public readonly DataBaseContext contextdossierpatient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextdossierpatient"></param>
        public DAL_PriseEnCharge(DataBaseContext contextdossierpatient)
        {
            this.contextdossierpatient = contextdossierpatient;

        }
        public async Task<List<PriseEnCharge>> GetAll()
        {
            await Migrations.create_table_PriseEncharge();

            return await this.contextdossierpatient.PriseEnCharge.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PriseEnCharge"></param>
        /// <returns></returns>
        public async Task<Message> Add(PriseEnCharge PriseEnCharge)
        {
            try
            {
                await Migrations.create_table_PriseEncharge();

                this.contextdossierpatient.PriseEnCharge.Add(PriseEnCharge);
                await this.contextdossierpatient.SaveChangesAsync();
                return new Message(true, "PriseEnCharge ajouté  avec succées  ");
            }
            catch (Exception ex)
            {
                return new Message(false, "erreur :" + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PriseEnCharge"></param>
        /// <returns></returns>
        public async Task<Message> Update(PriseEnCharge PriseEnCharge)
        {
            try
            {
                this.contextdossierpatient.PriseEnCharge.Update(PriseEnCharge);
                await this.contextdossierpatient.SaveChangesAsync();
                return new Message(true, "PriseEnCharge modifié avec succées  ");
            }
            catch (Exception ex)
            {
                return new Message(false, " erreur :" + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<PriseEnCharge>> GetByIdPatient(long Id)
        {

            return await this.contextdossierpatient.PriseEnCharge.Where(p => p.PatientID == Id).ToListAsync();
        }





        public async Task<PriseEnCharge?> GetByIdPatientLast(long PatientId)
        {

            var dernierElement = contextdossierpatient.PriseEnCharge
           .Where(e => e.PatientID == PatientId)
           // Supposons que vous avez une propriété "DateAjout" pour déterminer l'ordre d'ajout
           .Last();

            return dernierElement;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public async Task<PriseEnCharge?> GetById(long Id)
        {

            return await this.contextdossierpatient.PriseEnCharge.FirstOrDefaultAsync(p => p.Id == Id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public async Task<Message> Delete(long Id)
        {
            try
            {
                var act = await GetById(Id);
                if (act != null)
                {
                    this.contextdossierpatient.PriseEnCharge.Remove(act);
                    await this.contextdossierpatient.SaveChangesAsync();
                }
                return new Message(true, "PriseEnCharge supprimé avec succées  ");

            }

            catch (Exception ex)
            {
                return new Message(false, " erreur :" + ex.Message);
            }
        }

    }
}
