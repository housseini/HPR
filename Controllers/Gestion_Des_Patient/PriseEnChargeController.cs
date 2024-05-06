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
    public class PriseEnChargeController : ControllerBase
    {
        public readonly DataBaseContext contextdossierpatient;
        public DAL_PriseEnCharge DAL_PriseEncharge;

        public PriseEnChargeController(DataBaseContext contextdossierpatient)
        {
            this.contextdossierpatient = contextdossierpatient;

            this.DAL_PriseEncharge = new DAL_PriseEnCharge(contextdossierpatient);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetAll")]
        public async Task<JsonResult> GetAll()
        {
            var te = await this.DAL_PriseEncharge.GetAll();

            return new JsonResult(te);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PriseEncharge"></param>
        /// <returns></returns>


        [HttpPost("Add_Or_Update")]
        public async Task<IActionResult> Add_Or_Update(PriseEnCharge PriseEncharge)
        {
            if (ModelState.IsValid)
            {

                if (PriseEncharge.Id == 0)
                {
                    var me = await this.DAL_PriseEncharge.Add(PriseEncharge);
                    return new JsonResult(me);
                }

                else
                {
                    var me = await this.DAL_PriseEncharge.Update(PriseEncharge);
                    return new JsonResult(me);
                }


            }
            else
            {
                return new JsonResult(PriseEncharge);
            }
        }

        [HttpPost("UpdateFile")]
        public async Task<Message> UpdateFileAsync([FromForm] long IdPatient, [FromForm] IFormFile file)
        {
            try
            {



                if (file != null && file.Length > 0)
                {

                    var priseEncharge = await this.DAL_PriseEncharge.GetByIdPatientLast(IdPatient);
                    var fileName = Path.GetFileName("P000" + IdPatient + "_" + priseEncharge.NumeroPriseEnCharge + ".pdf");
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PriseEncharges", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                return (new Message(true, "ajouter  avec succes "));
            }
            catch (Exception e)
            {
                return (new Message(false, "erreur " + e.Message));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public async Task<JsonResult> GetById(long Id)
        {
            var te = this.DAL_PriseEncharge.GetById(Id);

            return new JsonResult(te);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetByIdPatient")]
        public async Task<JsonResult> GetByIdPatient(long Id)
        {
            var te = await this.DAL_PriseEncharge.GetByIdPatient(Id);

            return new JsonResult(te);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>


        [HttpDelete("Delete")]
        public async Task<JsonResult> Delete(long Id)
        {
            var te = await this.DAL_PriseEncharge.Delete(Id);

            return new JsonResult(te);
        }

        [HttpGet("GetFile")]
        public ActionResult GetFile(string Filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PriseEncharges", Filename);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = "example.pdf";

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
        }
    }
}
