using HPRBackend.Modules.Paramettres.GestionDesServices.DAL;
using HPRBackend.Modules.Paramettres.GestionDesServices.Models;
using HPRBackend.Modules.shard;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPRBackend.Controllers.Paramettres.Services
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        public readonly DataBaseContext contextdb;
        public DAL_Service DAL_Service;
        public ServiceController(DataBaseContext contextdb)
        {
            this.contextdb = contextdb;
            DAL_Service = new DAL_Service(contextdb);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetAll")]
        public async Task<JsonResult> GetAll()
        {
            await Migrations.create_table_Service();
            var te = await this.DAL_Service.GetAll();

            return new JsonResult(te);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="Service"></param>
        /// <returns></returns>

        [HttpPost("Add_Or_Update")]
        public async Task<JsonResult> Add_Or_Update(Service Service)
        {

            if (ModelState.IsValid)
            {
                if (Service.Id == 0)
                {
                    TimeZoneInfo gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    Service.Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1);


                    var me = await this.DAL_Service.Add(Service);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_Service.Update(Service);
                    return new JsonResult(me);
                }
            }
            else
            {
                return new JsonResult(Service);
            }




        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]

        public async Task<JsonResult> Delete(long Id)
        {
            var me = await this.DAL_Service.DeleteById(Id);
            return new JsonResult(me);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="file"></param>
        /// <returns></returns>



        [HttpPost("UpdateFile")]
        public async Task<JsonResult> UpdateFile(long Id, IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/planEmplacementServices", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var servie =    await DAL_Service.GetById(Id);
                if (servie != null)
                {
                    servie.Emplacement = file.FileName;

                }
                var re = await DAL_Service.UpdateFile(servie);



                return new JsonResult(re);


            }
            return new JsonResult(null);



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public async Task<JsonResult> GetById(long Id)
        {
            var te = await this.DAL_Service.GetById(Id);

            return new JsonResult(te);
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Filename"></param>
       /// <returns></returns>
        [HttpGet("GetFile")]
        public ActionResult GetFile(string Filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/planEmplacementServices", Filename);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = "example.pdf";

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
        }



    }
}
