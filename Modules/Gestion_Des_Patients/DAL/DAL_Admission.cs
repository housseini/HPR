using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HPRBackend.Modules.Gestion_Des_Patients.DAL
{
    public class DAL_Admission
    {
        private readonly DataBaseContext AdmissionPatientContext;



        public DAL_Admission(DataBaseContext AdmissionPatientContext)
        {
            this.AdmissionPatientContext = AdmissionPatientContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<Object?> GetAll()
        {
            await Migrations.create_table_Admission();

            var querie = from Admission in this.AdmissionPatientContext.Admission
                         join Patient in this.AdmissionPatientContext.Patient on Admission.IdPatient equals Patient.Id
                         join Medecin in this.AdmissionPatientContext.Agent on Admission.IdMedecin equals Medecin.Id
                         join Service in this.AdmissionPatientContext.Service on Admission.IdService equals Service.Id
                         join Agent in this.AdmissionPatientContext.Agent on Admission.IdAgent equals Agent.Id
                         join FactureAdmission  in this.AdmissionPatientContext.FactureAdmission on Admission.Id equals FactureAdmission.IdAdmission into FactureGroup
                         from FactureAdmission in FactureGroup.DefaultIfEmpty()
                         select new
                         {
                             Medecin = Medecin,
                             Service = Service,
                             Agent = Agent,
                             Patient = Patient,
                             Admission = Admission,
                             FactureAdmission= FactureAdmission ?? null,


                         };

            return querie;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Object?> GetByIdPatient(long Id )
        {

            var querie = from Patient  in this.AdmissionPatientContext.Patient.Where(f => f.Id == Id)
                         join Admission in this.AdmissionPatientContext.Admission on Patient.Id equals Admission.IdPatient
                         join Medecin in this.AdmissionPatientContext.Agent on Admission.IdMedecin equals Medecin.Id
                         join Service in this.AdmissionPatientContext.Service on Admission.IdService equals Service.Id
                         join Agent in this.AdmissionPatientContext.Agent on Admission.IdAgent equals Agent.Id
                         join FactureAdmission in this.AdmissionPatientContext.FactureAdmission on Admission.Id equals FactureAdmission.IdAdmission into FactureGroup
                         from FactureAdmission in FactureGroup.DefaultIfEmpty()
                         select new
                         {
                             Medecin = Medecin,
                             Service = Service,
                             Agent = Agent,
                             Patient = Patient,
                             Admission = Admission,
                             FactureAdmission = FactureAdmission ?? null,


                         };

            return querie;
        }





        /// <summary>
        /// ajouter Admission 
        /// </summary>
        /// <param name="Admission"></param>
        /// <returns></returns>
        public async Task<Message> Add(Admission Patient)
        {
            try
            {


                this.AdmissionPatientContext.Admission.Add(Patient);
                await this.AdmissionPatientContext.SaveChangesAsync();

                var Admission = this.AdmissionPatientContext.Admission.Where(p => p.Date_Ajout == Patient.Date_Ajout && p.IdPatient == Patient.IdPatient).First();


                return new Message(true, "le  Admission ajouté avec succés ;Admission : " + Admission.Id);
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
        public async Task<Message> Update(Admission Patient)
        {
            try
            {

                this.AdmissionPatientContext.Admission.Update(Patient);
                await this.AdmissionPatientContext.SaveChangesAsync();
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
        public async Task<Admission?> GetById(long Id)
        {



            var Patient = await this.AdmissionPatientContext.Admission.FirstOrDefaultAsync(s => s.Id == Id);
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
                    this.AdmissionPatientContext.Admission.Remove(m);
                    this.AdmissionPatientContext.SaveChanges();

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
