using HPRBackend.Modules.Paramettres.Gestion_des_Prestation.DAL;
using HPRBackend.Modules.Paramettres.Gestion_des_Prestation.Models;
using HPRBackend.Modules.shard;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPRBackend.Controllers.Paramettres.Gestion_des_Prestation
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class MotifAdmissionController : ControllerBase
    {
        private readonly DataBaseContext ActeTraimentContext;
        public DAL_MotifAdmission DAL_MotifAdmission;

        public MotifAdmissionController(DataBaseContext acteTraimentContext)
        {
            this.ActeTraimentContext = acteTraimentContext;
            DAL_MotifAdmission = new DAL_MotifAdmission(acteTraimentContext);
        }


        [HttpGet("Getall")]
        public async Task<JsonResult> Getall()
        {
            var me = await DAL_MotifAdmission.GetAll();
            return new JsonResult(me);
        }


        [HttpPost("Add_Or_Update")]
        public async Task<IActionResult> Add_Or_Update(MotifAdmission TypeChambre)
        {
            if (ModelState.IsValid)
            {

                if (TypeChambre.Id == 0)
                {



                    var me = await this.DAL_MotifAdmission.Add(TypeChambre);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_MotifAdmission.Update(TypeChambre);
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

            return new JsonResult(await this.DAL_MotifAdmission.Delete(id));
        }
    }
}
