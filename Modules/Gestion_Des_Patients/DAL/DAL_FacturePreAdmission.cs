using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;

namespace HPRBackend.Modules.Gestion_Des_Patients.DAL
{
    public class DAL_FacturePreAdmission
    {
        private readonly DataBaseContext AdmissionPatientContext;

        public DAL_FacturePreAdmission(DataBaseContext AdmissionPatientContext)
        {
            this.AdmissionPatientContext = AdmissionPatientContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<FacturePreAdmission>> GetAll()
        {
            await Migrations.create_table_FacturePreAdmission();
            await Migrations.create_table_PreAdmission_Prestation();
            return await AdmissionPatientContext.FacturePreAdmission.ToListAsync();
        }
        /// <summary>
        /// ajouter Admission 
        /// </summary>
        /// <param name="Admission"></param>
        /// <returns></returns>
        public async Task<Message> Add(FacturePreAdmission Patient)
        {
            try
            {


                this.AdmissionPatientContext.FacturePreAdmission.Add(Patient);
                await this.AdmissionPatientContext.SaveChangesAsync();



                return new Message(true, "le  FacturePreAdmission ajouté avec succés  : ");
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
        public async Task<Message> Update(FacturePreAdmission Patient)
        {
            try
            {

                this.AdmissionPatientContext.FacturePreAdmission.Update(Patient);
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
        public async Task<FacturePreAdmission?> GetById(long Id)
        {



            var Patient = await this.AdmissionPatientContext.FacturePreAdmission.FirstOrDefaultAsync(s => s.Id == Id);
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
                    this.AdmissionPatientContext.FacturePreAdmission.Remove(m);
                    this.AdmissionPatientContext.SaveChanges();

                }

                return new Message(true, "  Patient(e) supprimé(e) avec succes ");


            }
            catch (DbUpdateException e)
            {



                return new Message(false, "erreur de suppression " + e.Message);
            }



        }

        public async Task<Message> Add(long Id, List<PrestationIdAndNombre> PrestationIdAndNombreS)
        {
            try
            {

                var gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                string Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1).ToString();
                var Patient = await this.AdmissionPatientContext.Admission.FirstOrDefaultAsync(s => s.Id == Id);
                if (Patient != null)
                {
                    FacturePreAdmission FacturePreAdmission = new FacturePreAdmission();
                    var Prisevalide = await this.AdmissionPatientContext.PriseEnCharge.Where(s => s.PatientID == Patient.IdPatient).OrderByDescending(s => s.Id).FirstOrDefaultAsync();
                    if (Prisevalide != null)
                    {
                        DateTime expirationDate;
                        DateTime.TryParse(Prisevalide.DateExpiration, out expirationDate);
                        if (expirationDate >= DateTime.Now)
                        {
                            FacturePreAdmission.PEC = Prisevalide.Pourcentage;
                            FacturePreAdmission.PEC = Prisevalide.Pourcentage;
                            FacturePreAdmission.IdPrisEncharge = Prisevalide.Id;
                            FacturePreAdmission.IdPreAdmission = Id;
                            FacturePreAdmission.MontantTotale = 0;
                            FacturePreAdmission.Date_Ajout = Date_Ajout;
                            foreach (var item in PrestationIdAndNombreS)
                            {
                                var prestation = AdmissionPatientContext.Prestation.First(s => s.Id == item.IdPrestion);

                                if (prestation != null)
                                {

                                    FacturePreAdmission.MontantTotale = FacturePreAdmission.MontantTotale + ((prestation.Prix + (prestation.Prix * prestation.TVA / 100)) * item.Nombre);
                                }

                            }
                            FacturePreAdmission.MontantPatient = FacturePreAdmission.MontantTotale - ((FacturePreAdmission.MontantTotale * FacturePreAdmission.PEC) / 100);
                            FacturePreAdmission.MontantPriseEncharge = ((FacturePreAdmission.MontantTotale * FacturePreAdmission.PEC) / 100);
                            FacturePreAdmission.ReferenceFacture = "sdgsdgssg";

                            this.AdmissionPatientContext.FacturePreAdmission.Add(FacturePreAdmission);
                            await this.AdmissionPatientContext.SaveChangesAsync();

                        }
                        else
                        {
                            FacturePreAdmission.PEC = 0;
                            FacturePreAdmission.IdPrisEncharge = 0;
                            FacturePreAdmission.MontantTotale = 0;
                            FacturePreAdmission.IdPreAdmission = Id;

                            FacturePreAdmission.Date_Ajout = Date_Ajout;
                            foreach (var item in PrestationIdAndNombreS)
                            {
                                var prestation = AdmissionPatientContext.Prestation.First(s => s.Id == item.IdPrestion);

                                if (prestation != null)
                                {

                                    FacturePreAdmission.MontantTotale = FacturePreAdmission.MontantTotale + ((prestation.Prix + (prestation.Prix * prestation.TVA / 100)) * item.Nombre);
                                }

                            }
                            FacturePreAdmission.MontantPatient = FacturePreAdmission.MontantTotale;
                            FacturePreAdmission.MontantPriseEncharge = 0;
                            FacturePreAdmission.ReferenceFacture = "sdgsdgssg";

                            this.AdmissionPatientContext.FacturePreAdmission.Add(FacturePreAdmission);
                            await this.AdmissionPatientContext.SaveChangesAsync();
                            List<PreAdmission_Prestation> admission_Prestations = new List<PreAdmission_Prestation>();
                            foreach (var item in PrestationIdAndNombreS)
                            {
                                PreAdmission_Prestation admission_Prestation = new PreAdmission_Prestation();
                                admission_Prestation.PreAdmissionId = Id;
                                admission_Prestation.PrestationId = item.IdPrestion;
                                admission_Prestation.Nombre = item.Nombre;

                                admission_Prestations.Add(admission_Prestation);


                            }
                            this.AdmissionPatientContext.PreAdmission_Prestation.AddRange(admission_Prestations);
                            await this.AdmissionPatientContext.SaveChangesAsync();




                        }



                    }
                    else
                    {
                        FacturePreAdmission.PEC = 0;
                        FacturePreAdmission.IdPrisEncharge = 0;
                        FacturePreAdmission.MontantTotale = 0;
                        FacturePreAdmission.IdPreAdmission = Id;

                        FacturePreAdmission.Date_Ajout = Date_Ajout;
                        foreach (var item in PrestationIdAndNombreS)
                        {
                            var prestation = AdmissionPatientContext.Prestation.First(s => s.Id == item.IdPrestion);

                            if (prestation != null)
                            {

                                FacturePreAdmission.MontantTotale = FacturePreAdmission.MontantTotale + ((prestation.Prix + (prestation.Prix * prestation.TVA / 100)) * item.Nombre);
                            }

                        }
                        FacturePreAdmission.MontantPatient = FacturePreAdmission.MontantTotale;
                        FacturePreAdmission.MontantPriseEncharge = 0;
                        FacturePreAdmission.ReferenceFacture = "sdgsdgssg";

                        this.AdmissionPatientContext.FacturePreAdmission.Add(FacturePreAdmission);
                        await this.AdmissionPatientContext.SaveChangesAsync();

                        List<PreAdmission_Prestation> admission_Prestations = new List<PreAdmission_Prestation>();
                        foreach (var item in PrestationIdAndNombreS)
                        {
                            PreAdmission_Prestation admission_Prestation = new PreAdmission_Prestation();
                            admission_Prestation.PreAdmissionId = Id;
                            admission_Prestation.PrestationId = item.IdPrestion;
                            admission_Prestation.Nombre = item.Nombre;

                            admission_Prestations.Add(admission_Prestation);


                        }
                        this.AdmissionPatientContext.PreAdmission_Prestation.AddRange(admission_Prestations);
                        await this.AdmissionPatientContext.SaveChangesAsync();


                    }






                }
                else
                {
                    return new Message(true, "Admission  verifier l Admission   ");
                }







                return new Message(true, "le  Facture Admission ajouté avec succés  : ");
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


    }
}
