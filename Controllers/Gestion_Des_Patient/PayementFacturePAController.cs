
using HPRBackend.Modules.Gestion_Des_Patients.DAL;
using HPRBackend.Modules.Gestion_Des_Patients.Models;
using HPRBackend.Modules.shard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPRBackend.Controllers.Gestion_Des_Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayementFacturePAController : ControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        public readonly DataBaseContext _contextdb;
        /// <summary>
        /// 
        /// </summary>
        public DAL_PayementFacturePA DAL_PayementFacturePA;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextdb"></param>
        public PayementFacturePAController(DataBaseContext contextdb)
        {
            this._contextdb = contextdb;
            this.DAL_PayementFacturePA = new DAL_PayementFacturePA(_contextdb);
        }
        /// <summary>
        /// retourne la listes des PayementFacturePAs
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<JsonResult> GetAll()
        {
            var te = await this.DAL_PayementFacturePA.GetAll();

            return new JsonResult(te);
        }
        /// <summary>
        /// retournne la liste GetAllMedecin
        /// </summary>
        /// <returns></returns>
    
        /// <summary>
        /// elle permet d ajouter ou de modifieer un PayementFacturePA
        /// </summary>
        /// <param name="PayementFacturePA"></param>
        /// <returns></returns>
        [HttpPost("Add_Or_Update")]
        public async Task<JsonResult> Add_Or_Update(PayementFacturePA PayementFacturePA)
        {

            if (ModelState.IsValid)
            {
                if (PayementFacturePA.Id == 0)
                {
                    TimeZoneInfo gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    PayementFacturePA.Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1).ToString();


                    var me = await this.DAL_PayementFacturePA.ADD(PayementFacturePA);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_PayementFacturePA.Update(PayementFacturePA);
                    return new JsonResult(me);
                }
            }
            else
            {
                return new JsonResult(PayementFacturePA);
            }




        }
        /// <summary>
        /// supprimer un PayementFacturePA selon son ID 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
      
        /// <summary>
        ///
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet("GetById")]

        public async Task<JsonResult> GetById(long Id)
        {
            var me = await this.DAL_PayementFacturePA.GetById(Id);
            return new JsonResult(me);
        }

    }
}
