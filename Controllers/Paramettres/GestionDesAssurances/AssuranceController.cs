using HPRBackend.Modules.Paramettres.GestionDesAssurances.DAL;
using HPRBackend.Modules.Paramettres.GestionDesAssurances.Models;
using HPRBackend.Modules.shard;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPRBackend.Controllers.Paramettres.GestionDesAssurances
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class AssuranceController : ControllerBase
    {
        private readonly DataBaseContext ActeTraimentContext;
        public DAL_Assurance DAL_Assurance;

        public AssuranceController(DataBaseContext acteTraimentContext)
        {
            this.ActeTraimentContext = acteTraimentContext;
            DAL_Assurance = new DAL_Assurance(acteTraimentContext);
        }


        [HttpGet("Getall")]
        public async Task<JsonResult> Getall()
        {
            var me = await DAL_Assurance.GetAll();
            return new JsonResult(me);
        }

        [HttpPost("Add_Or_Update")]
        public async Task<IActionResult> Add_Or_Update(Assurance TypeChambre)
        {
            if (ModelState.IsValid)
            {

                if (TypeChambre.Id == 0)
                {

                    TimeZoneInfo gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    TypeChambre.Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1);


                    var me = await this.DAL_Assurance.Add(TypeChambre);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_Assurance.Update(TypeChambre);
                    return new JsonResult(me);

                }

            }
            else
            {
                return new JsonResult(TypeChambre);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(long id)
        {

            return new JsonResult(await this.DAL_Assurance.Delete(id));
        }


    }
}
