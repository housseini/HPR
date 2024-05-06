using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.Paramettres.Gestion_des_Prestation.Models;
using HPRBackend.Modules.shard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HPRBackend.Modules.Gestion_Des_Patients.DAL
{
    public class DAL_FactureAdmission
    {
        private readonly DataBaseContext AdmissionPatientContext;

        public DAL_FactureAdmission(DataBaseContext AdmissionPatientContext)
        {
            this.AdmissionPatientContext = AdmissionPatientContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public async Task<List<FactureAdmission>> GetAll()
        {
            await Migrations.create_table_FactureAdmission();
            await Migrations.create_table_Admission_Prestation();
            return await AdmissionPatientContext.FactureAdmission.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>


        public async Task<Object?> GetAllAdmission()
        {
            var queri = from FactureAdmission in this.AdmissionPatientContext.FactureAdmission
                        join Admission in this.AdmissionPatientContext.Admission on FactureAdmission.IdAdmission equals
                        Admission.Id
                        join Admission_Prestation in this.AdmissionPatientContext.Admission_Prestation on Admission.Id equals Admission_Prestation.AdmissionId
                        join Prestation in this.AdmissionPatientContext.Prestation on Admission_Prestation.PrestationId equals Prestation.Id
                        select new
                        {
                            FactureAdmission= FactureAdmission,
                            Admission_Prestation= Admission_Prestation,
                            Prestation= Prestation,


                        };

            return queri;
        }
        /// <summary>
        /// ajouter Admission 
        /// </summary>
        /// <param name="Admission"></param>
        /// <returns></returns>
        public async Task<Message> Add(FactureAdmission Patient)
        {
            try
            {


                this.AdmissionPatientContext.FactureAdmission.Add(Patient);
                await this.AdmissionPatientContext.SaveChangesAsync();



                return new Message(true, "le  FactureAdmission ajouté avec succés  : ");
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
        public async Task<Message> Update(FactureAdmission Patient)
        {
            try
            {

                this.AdmissionPatientContext.FactureAdmission.Update(Patient);
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
        public async Task<FactureAdmission?> GetById(long Id)
        {



            var Patient = await this.AdmissionPatientContext.FactureAdmission.FirstOrDefaultAsync(s => s.Id == Id);
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
                    this.AdmissionPatientContext.FactureAdmission.Remove(m);
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
                if(Patient != null)
                {
                    FactureAdmission FactureAdmission = new FactureAdmission();
                    var Prisevalide = await this.AdmissionPatientContext.PriseEnCharge.Where(s => s.PatientID == Patient.IdPatient).OrderByDescending(s => s.Id).FirstOrDefaultAsync();
                    if (Prisevalide != null)
                    {
                        DateTime expirationDate;
                        DateTime.TryParse(Prisevalide.DateExpiration, out expirationDate);
                        if (expirationDate >= DateTime.Now)
                        {
                            FactureAdmission.PEC = Prisevalide.Pourcentage;
                            FactureAdmission.PEC = Prisevalide.Pourcentage;
                            FactureAdmission.IdPrisEncharge = Prisevalide.Id;
                            FactureAdmission.IdAdmission = Id;
                            FactureAdmission.MontantTotale = 0;
                            FactureAdmission.Date_Ajout = Date_Ajout;
                            foreach (var item in PrestationIdAndNombreS)
                            {
                               var  prestation = AdmissionPatientContext.Prestation.First(s => s.Id == item.IdPrestion);
                                
                                if (prestation != null) {

                                  FactureAdmission.MontantTotale = FactureAdmission.MontantTotale + ((prestation.Prix + (prestation.Prix * prestation.TVA / 100)) * item.Nombre);
                                }
                                
                            }
                            FactureAdmission.MontantPatient = FactureAdmission.MontantTotale - ((FactureAdmission.MontantTotale * FactureAdmission.PEC) / 100);
                            FactureAdmission.MontantPriseEncharge = ((FactureAdmission.MontantTotale * FactureAdmission.PEC) / 100);
                            FactureAdmission.ReferenceFacture = "sdgsdgssg";

                            this.AdmissionPatientContext.FactureAdmission.Add(FactureAdmission);
                            await this.AdmissionPatientContext.SaveChangesAsync();

                        }
                        else
                        {
                            FactureAdmission.PEC = 0;
                            FactureAdmission.IdPrisEncharge = 0;
                            FactureAdmission.MontantTotale = 0;
                            FactureAdmission.IdAdmission = Id;

                            FactureAdmission.Date_Ajout = Date_Ajout;
                            foreach (var item in PrestationIdAndNombreS)
                            {
                                var prestation = AdmissionPatientContext.Prestation.First(s => s.Id == item.IdPrestion);

                                if (prestation != null)
                                {

                                    FactureAdmission.MontantTotale = FactureAdmission.MontantTotale + ((prestation.Prix + (prestation.Prix * prestation.TVA / 100)) * item.Nombre);
                                }

                            }
                            FactureAdmission.MontantPatient = FactureAdmission.MontantTotale;
                            FactureAdmission.MontantPriseEncharge = 0; 
                            FactureAdmission.ReferenceFacture = "sdgsdgssg";

                            this.AdmissionPatientContext.FactureAdmission.Add(FactureAdmission);
                            await this.AdmissionPatientContext.SaveChangesAsync();
                            List<Admission_Prestation> admission_Prestations = new List<Admission_Prestation>();
                            foreach (var item in PrestationIdAndNombreS)
                            {
                                Admission_Prestation  admission_Prestation= new Admission_Prestation();
                                admission_Prestation.AdmissionId = Id;
                                admission_Prestation.PrestationId = item.IdPrestion;
                                admission_Prestation.Nombre = item.Nombre;
                                admission_Prestations.Add(admission_Prestation);


                            }
                            this.AdmissionPatientContext.Admission_Prestation.AddRange(admission_Prestations);
                            await this.AdmissionPatientContext.SaveChangesAsync();




                        }



                    }
                    else
                    {
                        FactureAdmission.PEC = 0;
                        FactureAdmission.IdPrisEncharge = 0;
                        FactureAdmission.MontantTotale = 0;
                        FactureAdmission.IdAdmission = Id;

                        FactureAdmission.Date_Ajout = Date_Ajout;
                        foreach (var item in PrestationIdAndNombreS)
                        {
                            var prestation = AdmissionPatientContext.Prestation.First(s => s.Id == item.IdPrestion);

                            if (prestation != null)
                            {

                                FactureAdmission.MontantTotale = FactureAdmission.MontantTotale + ((prestation.Prix + (prestation.Prix * prestation.TVA / 100)) * item.Nombre);
                            }

                        }
                        FactureAdmission.MontantPatient = FactureAdmission.MontantTotale;
                        FactureAdmission.MontantPriseEncharge = 0;
                        FactureAdmission.ReferenceFacture = "sdgsdgssg";

                        this.AdmissionPatientContext.FactureAdmission.Add(FactureAdmission);
                        await this.AdmissionPatientContext.SaveChangesAsync();

                        List<Admission_Prestation> admission_Prestations = new List<Admission_Prestation>();
                        foreach (var item in PrestationIdAndNombreS)
                        {
                            Admission_Prestation admission_Prestation = new Admission_Prestation();
                            admission_Prestation.AdmissionId = Id;
                            admission_Prestation.PrestationId = item.IdPrestion;
                            admission_Prestation.Nombre = item.Nombre;

                            admission_Prestations.Add(admission_Prestation);


                        }
                        this.AdmissionPatientContext.Admission_Prestation.AddRange(admission_Prestations);
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
