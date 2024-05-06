using HPRBackend.Modules.Paramettres.Gestion_des_Prestation.DAL;
using HPRBackend.Modules.Paramettres.Gestion_des_Prestation.Models;
using HPRBackend.Modules.shard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPRBackend.Controllers.Paramettres.Gestion_des_Prestation
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestationController : ControllerBase
    {
        private readonly DataBaseContext ActeTraimentContext;
        public DAL_Prestation DAL_Prestation;

        public PrestationController(DataBaseContext acteTraimentContext)
        {
            this.ActeTraimentContext = acteTraimentContext;
            DAL_Prestation = new DAL_Prestation(acteTraimentContext);
        }


        [HttpGet("Getall")]
        public async Task<JsonResult> Getall()
        {
            var me = await DAL_Prestation.GetAll();
            return new JsonResult(me);
        }
        [HttpGet("GetAllByIdType")]
        public async Task<JsonResult> GetAllByIdType(long Id)
        {
            var me = await DAL_Prestation.GetAllByIdType(Id);
            return new JsonResult(me);
        }

        [HttpPost("Add_Or_Update")]
        public async Task<IActionResult> Add_Or_Update(Prestation TypeChambre)
        {
            if (ModelState.IsValid)
            {

                if (TypeChambre.Id == 0)
                {



                    var me = await this.DAL_Prestation.Add(TypeChambre);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_Prestation.Update(TypeChambre);
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

            return new JsonResult(await this.DAL_Prestation.Delete(id));
        }
    }
}
