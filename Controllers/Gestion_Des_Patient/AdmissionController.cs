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
    public class AdmissionController : ControllerBase
    {
        public readonly DataBaseContext DataBaseContext;
        public DAL_Admission DAL_Admission;
        public AdmissionController(DataBaseContext DataBaseContext)
        {
            this.DataBaseContext = DataBaseContext;

            this.DAL_Admission = new DAL_Admission(DataBaseContext);

        }

        [HttpPost("Add_Or_Update")]
        public async Task<IActionResult> Add_Or_Update(Admission Admission)
        {
            if (ModelState.IsValid)
            {

                if (Admission.Id == 0)  
                {

                    var gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    Admission.Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1).ToString();

                    var me = await this.DAL_Admission.Add(Admission);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_Admission.Update(Admission);
                    return new JsonResult(me);

                }

            }
            else
            {
                return new JsonResult(Admission);
            }
        }


   

        [HttpGet("Getall")]
        public async Task<JsonResult> Getall()
        {
            var me = await DAL_Admission.GetAll();
            return new JsonResult(me);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        

       [HttpGet("GetByIdPatient")]
        public async Task<JsonResult> GetByIdPatient(long Id )
        {
            var me = await DAL_Admission.GetByIdPatient(Id);
            return new JsonResult(me);
        }

        [HttpGet("GetById")]
        public async Task<JsonResult> GetById(long Id)
        {
            var me = await DAL_Admission.GetById(Id);
            return new JsonResult(me);
        }


        [HttpDelete("Delete")]
        public async Task<JsonResult> Delete(long Id)
        {
            var te = await this.DAL_Admission.Delete(Id);

            return new JsonResult(te);
        }
    }
}
