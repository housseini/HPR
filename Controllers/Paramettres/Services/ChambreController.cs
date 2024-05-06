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
    public class ChambreController : ControllerBase
    {
        public readonly DataBaseContext contextdb;
        public DAL_Chambre DAL_Chambre;
        public ChambreController(DataBaseContext _contextdb)
        {
            this.contextdb = _contextdb;
            this.DAL_Chambre = new DAL_Chambre(this.contextdb);

        }

        /// <summary>
        /// renvoie liste des chambre ,les service etage , et le service
        /// </summary>
        /// <returns></returns>
        [HttpGet("Getall")]
        public async Task<JsonResult> Getall()
        {

            var me = DAL_Chambre.GetAll();
            return new JsonResult(me);
        }


        [HttpPost("Add_or_Update")]
        public async Task<JsonResult> Add_or_Update(Chambre Chambre)
        {

            if (ModelState.IsValid)
            {
                var me = await DAL_Chambre.Add(Chambre);
                return new JsonResult(me);

            }
            else
                return new JsonResult(Chambre);

        }

        [HttpGet("GetAllPrix")]
        public async Task<JsonResult> GetAllPrix()
        {

            var me = DAL_Chambre.GetAllPrix();
            return new JsonResult(me);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Idservice"></param>
        /// <returns></returns>
        [HttpGet("GetAllByIdservice")]
        public async Task<JsonResult> GetAllByIdservice(long Idservice)
        {

            var me = DAL_Chambre.GetAllByIdservice(Idservice);
            return new JsonResult(me);
        }

        [HttpGet("GetById")]
        public async Task<JsonResult> GetById(long Id)
        {

            var me = DAL_Chambre.GetById(Id);
            return new JsonResult(me);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdserviceEtage"></param>
        /// <returns></returns>
        [HttpGet("GetAllByIdserviceEtage")]
        public async Task<JsonResult> GetAllByIdserviceEtage(long IdserviceEtage)
        {

            var me = DAL_Chambre.GetAllByIdserviceEtage(IdserviceEtage);
            return new JsonResult(me);
        }
        [HttpGet("GetAllByIdserviceLivre")]
        public async Task<JsonResult> GetAllByIdserviceLivre(long Idservice)
        {

            var me = DAL_Chambre.GetAllByIdserviceLivre(Idservice);
            return new JsonResult(me);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdserviceEtage"></param>
        /// <param name="NumeroEtage"></param>
        /// <returns></returns>
        [HttpGet("GetAllByIdserviceEtageAndNumeroEtage")]
        public async Task<JsonResult> GetAllByIdserviceEtageAndNumeroEtage(long IdserviceEtage, long NumeroEtage)
        {

            var me = DAL_Chambre.GetAllByIdserviceEtageAndNumeroEtage(IdserviceEtage, NumeroEtage);
            return new JsonResult(me);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdserviceEtage"></param>
        /// <param name="NumeroEtage"></param>
        /// <param name="NumeroChambre"></param>
        /// <returns></returns>
        [HttpGet("GetAllByIdserviceEtageAndNumeroEtageAndNumeroChambre")]
        public async Task<JsonResult> GetAllByIdserviceEtageAndNumeroEtageAndNumeroChambre(long IdserviceEtage, long NumeroEtage, long NumeroChambre)
        {

            var me = DAL_Chambre.GetAllByIdserviceEtageAndNumeroEtageAndNumeroChambre(IdserviceEtage, NumeroEtage, NumeroChambre);
            return new JsonResult(me);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdserviceEtage"></param>
        /// <param name="NumeroEtage"></param>
        /// <param name="NumeroChambre"></param>
        /// <param name="NumeroLit"></param>
        /// <returns></returns>
        [HttpGet("GetAllByIdserviceEtageAndNumeroEtageAndNumeroChambreAndNumeroLit")]
        public async Task<JsonResult> GetAllByIdserviceEtageAndNumeroEtageAndNumeroChambreAndNumeroLit(long IdserviceEtage, long NumeroEtage, long NumeroChambre, long NumeroLit)
        {

            var me = DAL_Chambre.GetAllByIdserviceEtageAndNumeroEtageAndNumeroChambreAndNumeroLit(IdserviceEtage, NumeroEtage, NumeroChambre, NumeroLit);
            return new JsonResult(me);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Idchambre"></param>
        /// <returns></returns>
        [HttpGet("DecommissionByIdchambre")]
        public async Task<JsonResult> DecommissionByIdchambre(long Idchambre)
        {

            var me = await DAL_Chambre.DecommissionByIdchambre(Idchambre);
            return new JsonResult(me);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EtageServiceId"></param>
        /// <returns></returns>
        [HttpGet("DecommissionByEtageServiceId")]
        public async Task<JsonResult> DecommissionByEtageServiceId(long EtageServiceId)
        {

            var me = await DAL_Chambre.DecommissionByEtageServiceId(EtageServiceId);
            return new JsonResult(me);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Idchambre"></param>
        /// <returns></returns>
        [HttpGet("reclassifyByIdchambre")]
        public async Task<JsonResult> reclassifyByIdchambre(long Idchambre)
        {

            var me = await DAL_Chambre.reclassifyByIdchambre(Idchambre);
            return new JsonResult(me);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EtageServiceId"></param>
        /// <returns></returns>
        [HttpGet("reclassifyByEtageServiceId")]
        public async Task<JsonResult> reclassifyByEtageServiceId(long EtageServiceId)
        {

            var me = await DAL_Chambre.reclassifyByEtageServiceId(EtageServiceId);
            return new JsonResult(me);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="EtageServiceId"></param>
        /// <returns></returns>



        [HttpGet("GetAllChambreLibreByIdserviceEtage")]
        public async Task<JsonResult> GetAllChambreLibreByIdserviceEtage(long EtageServiceId)
        {

            var me = await DAL_Chambre.GetAllChambreLibreByIdserviceEtage(EtageServiceId);
            return new JsonResult(me);
        }
        //[HttpGet("GetAllChambreOccupeByIdserviceEtage")]
        //public async Task<JsonResult> GetAllChambreOccupeByIdserviceEtage(long EtageServiceId)
        //{

        //    var me = await DAL_Chambre.GetAllChambreOccupeByIdserviceEtage(EtageServiceId);
        //    return new JsonResult(me);
        //}

        [HttpGet("GetAllChambre_Type")]
        public async Task<JsonResult> GetAllChambre_Type(long Id)
        {

            var me = DAL_Chambre.GetAllChambre_Type(Id);
            return new JsonResult(me);
        }
    }
}
