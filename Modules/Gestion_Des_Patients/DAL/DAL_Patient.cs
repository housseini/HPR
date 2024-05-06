using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Gestion_Des_Patients.DAL
{
    public class DAL_Patient
    {
        private readonly DataBaseContext AdmissionPatientContext;



        public DAL_Patient(DataBaseContext AdmissionPatientContext)
        {
            this.AdmissionPatientContext = AdmissionPatientContext;
        }
        /// <summary>
        /// renvoie la liste de Patient 
        /// </summary>
        /// <returns></returns>

        public async Task<List<Patient>> GetAll()
        {
           
            await Migrations.create_table_Patient();

            return await AdmissionPatientContext.Patient.ToListAsync();
        }
        /// <summary>
        /// ajouter Patient 
        /// </summary>
        /// <param name="Patient"></param>
        /// <returns></returns>
        public async Task<Message> Add(Patient Patient)
        {
            try
            {


                this.AdmissionPatientContext.Patient.Add(Patient);
                await this.AdmissionPatientContext.SaveChangesAsync();

                var patient = AdmissionPatientContext.Patient.Where(m => m.NumeroCarte == Patient.NumeroCarte).First();

                return new Message(true, "le  Patient ajouté avec succés ," + patient.Id);
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
        /// update patient 
        /// </summary>
        /// <param name="Patient"></param>
        /// <returns></returns>
        public async Task<Message> Update(Patient Patient)
        {
            try
            {

                this.AdmissionPatientContext.Update(Patient);
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
        /// renvoie un  patient selon son Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Patient?> GetById(long Id)
        {



            var Patient = await this.AdmissionPatientContext.Patient.FirstOrDefaultAsync(s => s.Id == Id);
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
        /// supprime un patient selon son Id 
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
                    this.AdmissionPatientContext.Patient.Remove(m);
                    this.AdmissionPatientContext.SaveChanges();

                }

                return new Message(true, "  Patient(e) supprimé(e) avec succes ");


            }
            catch (DbUpdateException e)
            {



                return new Message(false, "erreur de suppression " + e.Message);
            }


        }

        /// <summary>
        /// recherche Avancer 
        /// </summary>
        /// <param name="Nom"></param>
        /// <param name="Prenom"></param>
        /// <param name="Telephone"></param>
        /// <param name="Service"></param>
        /// <param name="MotifdAdmission"></param>
        /// <param name="ReferencePatient"></param>
        /// <returns></returns>

        public async Task<List<Patient>?> SearchBy(string Nom, string Prenom, string Telephone, string Service, string MotifdAdmission, string ReferencePatient)


        {

            await Migrations.create_table_Patient();
            var liste = this.AdmissionPatientContext.Patient.Where(
                        p => p.Nom == Nom || p.Telephone == Telephone

                         || p.ReferencePatient == ReferencePatient
                         || p.Prenom == Prenom
                         || p.Telephone == Telephone

                         || p.ReferencePatient == ReferencePatient
                        ).ToList();

            return liste;

        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="IdService"></param>
        /// <returns></returns>
        //public List<Patient>? GetByIdService(long IdService)
        //{
        //    var Patient = this.AdmissionPatientContext.Patient.Where(s => s.Id == IdService).ToList();
        //    if (Patient != null)
        //    {



        //        return Patient;
        //    }
        //    else
        //    {
        //        return null;

        //    }


        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdMedecinTraitant"></param>
        /// <returns></returns>

        //public List<Patient>? GetByIdMedecinTraitant(long IdMedecinTraitant)
        //{
        //    var Patient = this.AdmissionPatientContext.Patient.Where(s => s.Id == IdMedecinTraitant).ToList();
        //    if (Patient != null)
        //    {



        //        return Patient;
        //    }
        //    else
        //    {
        //        return null;

        //    }


        //}
        //public List<Patient>? GetByIdMedecinCorespondent(long MedecinCorrespondantId)
        //{
        //    var Patient = this.AdmissionPatientContext.Patient.Where(s => s.Id == MedecinCorrespondantId).ToList();
        //    if (Patient != null)
        //    {



        //        return Patient;
        //    }
        //    else
        //    {
        //        return null;

        //    }


        //}


        //public async Task<Object?> GetAllPatientND()
        //{

        //    try
        //    {
        //        var DossierAndPatient = AdmissionPatientContext.Patient
        //                                .Where(p => !AdmissionPatientContext.DossierPatient.Any(dp => dp.PatientId == p.Id))
        //                                .Select(p => new
        //                                {
        //                                    Patient = p
        //                                }).ToList();
        //        return DossierAndPatient;
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}


        public Dictionary<string, long> CountsPatientByIdService(long ServiceId)
        {
            var keyValuePairs = new Dictionary<string, long>();



            return keyValuePairs;
        }


        public Dictionary<string, long> CountsPatientByIdService(long ServiceId, DateTime date, long plagedate)
        {
            var keyValuePairs = new Dictionary<string, long>();



            return keyValuePairs;
        }


     
    }
}
