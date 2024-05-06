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
    public class ServicesTypeController : ControllerBase
    {
        public readonly DataBaseContext contextdb;
        public DAL_ServicesType DAL_ServicesType;
        public ServicesTypeController(DataBaseContext contextdb)
        {
            this.contextdb = contextdb;
            DAL_ServicesType = new DAL_ServicesType(contextdb);
        }
        /// <summary>
        /// retourne la liste des types de service 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<JsonResult> GetAll()
        {
            var te = await this.DAL_ServicesType.GetAll();

            return new JsonResult(te);
        }
        /// <summary>
        ///  ajouter ou modifier un type de service 
        /// </summary>
        /// <param name="servicesType"></param>
        /// <returns></returns>

        [HttpPost("Add_Or_Update")]
        public async Task<JsonResult> Add_Or_Update(ServicesType servicesType)
        {

            if (ModelState.IsValid)
            {
                if (servicesType.Id == 0)
                {
                    TimeZoneInfo gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    servicesType.Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1);

                    servicesType.Date_Ajout = DateTime.UtcNow;
                    var me = await this.DAL_ServicesType.Add(servicesType);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_ServicesType.Update(servicesType);
                    return new JsonResult(me);
                }
            }
            else
            {
                return new JsonResult(servicesType);
            }




        }
        /// <summary>
        /// supprimer un service type selon son Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]

        public async Task<JsonResult> Delete(long Id)
        {
            var me = await this.DAL_ServicesType.DeleteById(Id);
            return new JsonResult(me);
        }
    
        /// <summary>
        /// supprimés les types services selon les Ids 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpDelete("Deletes")]

        public async Task<JsonResult> Deletes(List<long> Ids)
        {
            try
            {


                foreach (var Id in Ids)
                {


                    await this.DAL_ServicesType.DeleteById(Id);
                }
                return new JsonResult(new Message(true, "services types  supprimés avec succés"));
            }
            catch (Exception e)
            {

                return new JsonResult(new Message(false, " erreur : " + e.Message.ToString()));

            }


        }
    }
}
