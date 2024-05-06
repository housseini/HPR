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
    public class TypeChambreController : ControllerBase
    {
        public readonly DataBaseContext contextdb;
        public DAL_TypeChambre DAL_TypeChambre;
        public TypeChambreController(DataBaseContext contextdb)
        {
            this.contextdb = contextdb;
            DAL_TypeChambre = new DAL_TypeChambre(contextdb);

        }


        [HttpGet("Getall")]
        public async Task<JsonResult> Getall()
        {
            var me = await DAL_TypeChambre.GetAll();
            return new JsonResult(me);
        }
        [HttpGet("GetById")]
        public async Task<JsonResult> GetById(long Id)
        {
            var me = DAL_TypeChambre.GetById(Id);
            return new JsonResult(me);
        }

        [HttpDelete("Delete")]
        public async Task<JsonResult> Delete(long Id)
        {
            var me = DAL_TypeChambre.Delete(Id);
            return new JsonResult(me);
        }



        [HttpPost("Add_Or_Update")]
        public async Task<IActionResult> Add_Or_Update(TypeChambre TypeChambre)
        {
            if (ModelState.IsValid)
            {

                if (TypeChambre.Id == 0)
                {



                    var me = await this.DAL_TypeChambre.Add(TypeChambre);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_TypeChambre.Update(TypeChambre);
                    return new JsonResult(me);

                }

            }
            else
            {
                return new JsonResult(TypeChambre);
            }
        }

    }
}
