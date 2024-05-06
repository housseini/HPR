using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Gestion_Des_Patients.DAL
{
    public class DAL_PayementFacturePA
    {
        private readonly DataBaseContext DataBaseContext;

        public DAL_PayementFacturePA(DataBaseContext _DataBaseContext)
        {
            DataBaseContext = _DataBaseContext;
        }
        /// <summary>
        /// AOUTER UN PayementFacturePA
        /// </summary>
        /// <param name="PayementFacturePA"></param>
        /// <returns></returns>
        public async Task<Message> ADD(PayementFacturePA PayementFacturePA)
        {
            try
            {
                var Fact=  await this.DataBaseContext.FactureAdmission.Where(p=>p.Id==PayementFacturePA.IdFactureAdmission).FirstAsync();
                if(Fact != null) {
                    PayementFacturePA.MontantRestantPayer = Fact.MontantPatient - PayementFacturePA.MontantTotalePayer;     
                }


                await DataBaseContext.PayementFacturePA.AddAsync(PayementFacturePA);
                await DataBaseContext.SaveChangesAsync();


                return new Message(true, " PayementFacturePA Ajouter avec succés");

            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Matricule'"))
                {
                    return new Message(false, " le Matricule existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Email'"))
                {
                    return new Message(false, " l'adresse email existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Contacte'"))
                {
                    return new Message(false, " le numero de telephone existe");


                }
                return new Message(false, e.Message);
            }

        }
        /// <summary>
        /// liste des PayementFacturePAs
        /// </summary>
        /// <returns></returns>
        public async Task<List<PayementFacturePA>> GetAll()
        {
            //await Migrations.Migrations.create_table_Affectation();
            //await Migrations.Migrations.create_table_Service();
            await Migrations.create_table_PayementFacturePA();
            return await DataBaseContext.PayementFacturePA.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<PayementFacturePA>> GetAllByIdAgent(long Id)
        {
            //await Migrations.Migrations.create_table_Affectation();
            //await Migrations.Migrations.create_table_Service();
            return await DataBaseContext.PayementFacturePA.Where(p => p.IdAgent == Id ).ToListAsync();
        }
        /// <summary>
        /// retour le PayementFacturePA selon Id du PayementFacturePA 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<PayementFacturePA?> GetById(long Id)
        {
            var PayementFacturePA = await DataBaseContext.PayementFacturePA.FirstOrDefaultAsync(m => m.Id == Id);
            if (PayementFacturePA != null)
                return PayementFacturePA;
            return null;
        }




        /// <summary>
        /// modifier un PayementFacturePA
        /// </summary>
        /// <param name="PayementFacturePA"></param>
        /// <returns></returns>
        public async Task<Message> Update(PayementFacturePA PayementFacturePA)
        {
            try
            {
                DataBaseContext.PayementFacturePA.Update(PayementFacturePA);
                await DataBaseContext.SaveChangesAsync();
                return new Message(true, " PayementFacturePA modifier avec succes");
            }
            catch (DbUpdateException e)
            {

                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Matricule'"))
                {
                    return new Message(false, " le Matricule existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Email'"))
                {
                    return new Message(false, " l'adresse email existe");


                }
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint 'Uk_Contacte'"))
                {
                    return new Message(false, " le numero de telephone existe");


                }
                return new Message(false, " erreur " + e.Message);
            }
        }

        /// <summary>
        /// suppression de PayementFacturePA selon un Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public async Task<Message> DELETEbYId(long Id)
        {
            var PayementFacturePA = await DataBaseContext.PayementFacturePA.FirstOrDefaultAsync(m => m.Id == Id);
            if (PayementFacturePA != null)
            {
                DataBaseContext.PayementFacturePA.Remove(PayementFacturePA);
                await DataBaseContext.SaveChangesAsync();
                return new Message(true, "PayementFacturePA supprimer avec succés");
            }
            return new Message(false, "erreur de suppression de PayementFacturePA ");


        }
    }
}
