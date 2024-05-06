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
    public class PreAdmissionController : ControllerBase
    {
        public readonly DataBaseContext DataBaseContext;
        public DAL_PreAdmission DAL_PreAdmission;
        public PreAdmissionController(DataBaseContext DataBaseContext)
        {
            this.DataBaseContext = DataBaseContext;

            this.DAL_PreAdmission = new DAL_PreAdmission(DataBaseContext);

        }

        [HttpPost("Add_Or_Update")]
        public async Task<IActionResult> Add_Or_Update(PreAdmission Admission)
        {
            if (ModelState.IsValid)
            {

                if (Admission.Id == 0)
                {

                    var gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    Admission.Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1).ToString();

                    var me = await this.DAL_PreAdmission.Add(Admission);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_PreAdmission.Update(Admission);
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
            var me = await DAL_PreAdmission.GetAll();
            return new JsonResult(me);
        }

        [HttpGet("GetById")]
        public async Task<JsonResult> GetById(long Id)
        {
            var me = await DAL_PreAdmission.GetById(Id);
            return new JsonResult(me);
        }


        [HttpDelete("Delete")]
        public async Task<JsonResult> Delete(long Id)
        {
            var te = await this.DAL_PreAdmission.Delete(Id);

            return new JsonResult(te);
        }
    }
}
