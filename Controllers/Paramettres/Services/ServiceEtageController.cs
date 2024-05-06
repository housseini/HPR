using HPRBackend.Modules.Paramettres.GestionDesServices.DAL;
using HPRBackend.Modules.shard;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPRBackend.Controllers.Paramettres.Services
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceEtageController : ControllerBase
    {
        public readonly DataBaseContext contextdb;
        public DAL_ServiceEtage DAL_ServiceEtage;
        public ServiceEtageController(DataBaseContext contextdb)
        {
            this.contextdb = contextdb;
            DAL_ServiceEtage = new DAL_ServiceEtage(contextdb);
        }
        /// <summary>

        /// </summary>
        /// <param name="ServiceId"></param>
        /// <param name="Nombre_de_chambre"></param>
        /// <param name="Nombre_de_lit"></param>
        /// <returns></returns>


        [HttpGet("Add")]
        public async Task<JsonResult> Add(long ServiceId, long NumeroEtage)
        {
            await Migrations.create_table_ServiceEtage();
            var me = await DAL_ServiceEtage.Add(ServiceId, NumeroEtage);
            return new JsonResult(me);
        }

        /// <summary>
        /// service etage liste 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Getall")]
        public async Task<JsonResult> Getall()
        {
            await Migrations.create_table_ServiceEtage();
            var me = await DAL_ServiceEtage.Getall();
            return new JsonResult(me);
        }
        /// <summary>
		/// renvoie le service etage selon l Id de l etage passé 
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>

        [HttpGet("GetByServiceId")]
        public async Task<JsonResult> GetByServiceId(long ServiceId)
        {

            var me = await DAL_ServiceEtage.GetByServiceId(ServiceId);
            return new JsonResult(me);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<JsonResult> Delete(long Id)
        {

            var me = await DAL_ServiceEtage.Delete(Id);
            return new JsonResult(me);
        }


        [HttpGet("GetById")]
        public async Task<JsonResult> GetById(long Id)
        {

            var me = await DAL_ServiceEtage.GetById(Id);
            return new JsonResult(me);
        }

        [HttpGet("GetById1")]
        public async Task<JsonResult> GetById1(long Id)
        {

            var me = await DAL_ServiceEtage.GetById1(Id);
            return new JsonResult(me);
        }


        /// <summary>
        /// renvoie le service etage selon l Id du service passé 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet("GetService_ServiceEtageByIdservice")]
        public async Task<JsonResult> GetService_ServiceEtageByIdservice(long Id)
        {

            var me = await DAL_ServiceEtage.GetService_ServiceEtageByIdservice(Id);
            return new JsonResult(me);
        }


        [HttpGet("CountsServiceEtagetByIdService")]
        public async Task<JsonResult> CountsServiceEtagetByIdService(long ServiceId)
        {

            var me = DAL_ServiceEtage.CountsServiceEtagetByIdService(ServiceId);
            return new JsonResult(me);
        }
        //[HttpGet("CountsServicetByIdServiceEtage")]
        //public async Task<JsonResult> CountsServicetByIdServiceEtage(long ServiceId)
        //{

        //    var me = DAL_ServiceEtage.CountsServicetByIdServiceEtage(ServiceId);
        //    return new JsonResult(me);
        //}
    }
}
