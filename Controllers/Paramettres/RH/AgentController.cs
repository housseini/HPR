using HPRBackend.Modules.Paramettres.GestionDesAgents.DAL;
using HPRBackend.Modules.Paramettres.GestionDesAgents.Models;
using HPRBackend.Modules.shard;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPRBackend.Controllers.Paramettres.RH
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly DataBaseContext _contextdb;
        /// <summary>
        /// 
        /// </summary>
        public DAL_Agent DAL_Agent;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextdb"></param>
        public AgentController(DataBaseContext contextdb)
        {
            this._contextdb = contextdb;
            this.DAL_Agent = new DAL_Agent(_contextdb);
        }
        /// <summary>
        /// retourne la listes des Agents
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<JsonResult> GetAll()
        {
            var te = await this.DAL_Agent.GetAll();

            return new JsonResult(te);
        }
        /// <summary>
        /// retournne la liste GetAllMedecin
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllMedecin")]
        public async Task<JsonResult> GetAllMedecin()
        {
            var te = await this.DAL_Agent.GetAllMedecin();

            return new JsonResult(te);
        }
        /// <summary>
        /// elle permet d ajouter ou de modifieer un Agent
        /// </summary>
        /// <param name="Agent"></param>
        /// <returns></returns>
        [HttpPost("Add_Or_Update")]
        public async Task<JsonResult> Add_Or_Update(Agent Agent)
        {

            if (ModelState.IsValid)
            {
                if (Agent.Id == 0)
                {
                    TimeZoneInfo gmtPlus1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                    Agent.Date_Ajout = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus1);


                    var me = await this.DAL_Agent.ADD(Agent);
                    return new JsonResult(me);
                }
                else
                {
                    var me = await this.DAL_Agent.Update(Agent);
                    return new JsonResult(me);
                }
            }
            else
            {
                return new JsonResult(Agent);
            }




        }
        /// <summary>
        /// supprimer un Agent selon son ID 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]

        public async Task<JsonResult> Delete(long Id)
        {
            var me = await this.DAL_Agent.DELETEbYId(Id);
            return new JsonResult(me);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet("GetById")]

        public async Task<JsonResult> GetById(long Id)
        {
            var me = await this.DAL_Agent.GetById(Id);
            return new JsonResult(me);
        }
     
        /// <summary>
        ///  supprimer selon une liste des Ids 
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


                    await this.DAL_Agent.DELETEbYId(Id);
                }
                return new JsonResult(new Message(true, "Agents supprimés avec succés"));
            }
            catch (Exception e)
            {

                return new JsonResult(new Message(false, " erreur : " + e.Message.ToString()));

            }


        }

        /// <summary>
        /// renvoie les données staticiques
        /// </summary>
        /// <returns></returns>

        [HttpGet("Counts")]
        public async Task<JsonResult> Counts()
        {
            var te = this.DAL_Agent.Counts();

            return new JsonResult(te);
        }
    }
}
