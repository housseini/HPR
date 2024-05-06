using HPRBackend.Modules.Gestion_Des_Patients.DAL;
using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.shard;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPRBackend.Controllers.Gestion_Des_Patient
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        public readonly DataBaseContext AdmissionPatientContext;
        /// <summary>
        /// 
        /// </summary>
        public DAL_Patient DAL_Patient;

        public PatientController(DataBaseContext AdmissionPatientContext)
        {
            this.AdmissionPatientContext = AdmissionPatientContext;
            this.DAL_Patient = new DAL_Patient(AdmissionPatientContext);

        }
        /// <summary>
        /// ajouter un patient
        /// </summary>
        /// <param name="Patient"></param>
        /// <returns></returns>

        [HttpPost("Add_Or_Update")]
        public async Task<IActionResult> Add_Or_Update(Patient Patient)
        {
            if (ModelState.IsValid)
            {

                if (Patient.Id == 0)
                {
                    var gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    Patient.Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1);


                    var me = await this.DAL_Patient.Add(Patient);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_Patient.Update(Patient);
                    return new JsonResult(me);

                }

            }
            else
            {
                return new JsonResult(Patient);
            }
        }

        /// <summary>
        /// modifier un patient 
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetAll")]
        public async Task<JsonResult> GetAll()
        {
            var te = await this.DAL_Patient.GetAll();

            return new JsonResult(te);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public async Task<JsonResult> GetById(long Id)
        {
            var te = await this.DAL_Patient.GetById(Id);

            return new JsonResult(te);
        }
        /// <summary>
        /// recherche avancée 
        /// </summary>
        /// <param name="Nom"></param>
        /// <param name="Prenom"></param>
        /// <param name="Telephone"></param>
        /// <param name="Service"></param>
        /// <param name="MotifdAdmission"></param>
        /// <param name="ReferencePatient"></param>
        /// <returns></returns>
        [HttpGet("SearchBy")]
        public async Task<JsonResult> SearchBy(string Nom, string Prenom, string Telephone, string Service, string MotifdAdmission, string ReferencePatient)
        {
            var te = await this.DAL_Patient.SearchBy(Nom, Prenom, Telephone, Service, MotifdAdmission, ReferencePatient);

            return new JsonResult(te);
        }


        /// <summary>
        /// renvoie une liste des patient  selon Id du service 
        /// </summary>
        /// <param name="IdService"></param>
        /// <returns></returns>
        //[HttpGet("GetByIdService")]
        //public JsonResult GetByIdService(long IdService)
        //{
        //    var te = this.DAL_Patient.GetByIdService(IdService);

        //    return new JsonResult(te);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdMedecinTraitant"></param>
        /// <returns></returns>
        //[HttpGet("GetByIdMedecinTraitant")]
        //public JsonResult GetByIdMedecinTraitant(long IdMedecinTraitant)
        //{
        //    var te = this.DAL_Patient.GetByIdMedecinTraitant(IdMedecinTraitant);

        //    return new JsonResult(te);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MedecinCorrespondantId"></param>
        /// <returns></returns>
        //[HttpGet("GetByIdMedecinCorespondent")]
        //public JsonResult GetByIdMedecinCorespondent(long MedecinCorrespondantId)
        //{
        //    var te = this.DAL_Patient.GetByIdMedecinCorespondent(MedecinCorrespondantId);

        //    return new JsonResult(te);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<JsonResult> Delete(long Id)
        {
            var te = await this.DAL_Patient.Delete(Id);

            return new JsonResult(te);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[HttpGet("GetAllPatientND")]
        //public JsonResult GetAllPatientND()
        //{
        //    var te = this.DAL_Patient.GetAllPatientND();

        //    return new JsonResult(te);
        //}

        [HttpGet("CountsPatientByIdService")]
        public async Task<JsonResult> CountsPatientByIdService(long ServiceId)
        {
            var te = this.DAL_Patient.CountsPatientByIdService(ServiceId);

            return new JsonResult(te);
        }

        [HttpGet("CountsPatientByIdServiceDatePlagedate")]
        public async Task<JsonResult> CountsPatientByIdServiceDatePlagedate(long ServiceId, DateTime date, long plagedate)
        {
            var te = this.DAL_Patient.CountsPatientByIdService(ServiceId, date, plagedate);

            return new JsonResult(te);
        }
    }
}
