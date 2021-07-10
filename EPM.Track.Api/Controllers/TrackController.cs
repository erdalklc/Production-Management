using EPM.Dto.Base;
using EPM.Fason.Dto.Fason;
using EPM.Track.Dto.Extensions;
using EPM.Track.Dto.Track;
using EPM.Track.Service.Services; 
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic; 

namespace EPM.Track.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet, HttpPost]
        public ActionResult<List<SatinAlmaDetay>> GetSatinAlmaDetay(object[] obj) => _trackService.SatinAlmaDetay(obj[0].IntParse());

        [HttpGet, HttpPost]
        public ActionResult<List<SurecBilgileri>> GetProcessInformations(object[] obj) => _trackService.GetProcessInformations(obj[0].IntParse(), obj[1].IntParse(), obj[2].IntParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EgemenOrmeSiparis>> GetEgemenOrmeList(object[] obj) => _trackService.GetEgemenOrmeList(obj[0].ToStringParse(), obj[1].IntParse());

        [HttpGet, HttpPost]
        public ActionResult<List<KumasDepo>> GetKumasDepoList(object[] obj) => _trackService.GetKumasDepoList(obj[0].IntParse(), obj[1].IntParse());

        [HttpGet, HttpPost]
        public ActionResult<List<KesimFoyleri>> GetKesimFoyuList(object[] obj) => _trackService.GetKesimFoyuList(obj[0].IntParse(), obj[1].IntParse(), obj[2].ToStringParse());

        [HttpGet, HttpPost]
        public ActionResult<List<BantBitisleri>> GetBantList(object[] obj) => _trackService.GetBantList(obj[0].IntParse(), obj[1].IntParse(), obj[2].ToStringParse());

        [HttpGet, HttpPost]
        public ActionResult<List<BantBitisleri>> GetKaliteList(object[] obj) => _trackService.GetKaliteList(obj[0].IntParse(), obj[1].IntParse(), obj[2].ToStringParse());

        [HttpGet, HttpPost]
        public ActionResult<object> GetProductionDetail(object[] obj) => _trackService.GetProductionDetail(obj[0].IntParse(), obj[1].IntParse(), obj[2].IntParse(), obj[3].ToStringParse(), obj[4].ToStringParse());

        [HttpGet, HttpPost]
        public ActionResult<List<KaliteGerceklesen>> GetKaliteGerceklesenler(KaliteGerceklesenFilter filter) => _trackService.GetKaliteGerceklesenler(filter);

        [HttpGet, HttpPost]
        public ActionResult<List<UretimTakipListesi>> GetUretimTakipListesi(object[] obj) => _trackService.GetUretimTakipListesi(obj[0].ToStringParse(), JsonConvert.DeserializeObject<TrackList_Filter>(obj[1].ToStringParse()));

        [HttpGet, HttpPost]
        public ActionResult<IEnumerable<EPM_MASTER_PRODUCTION_ORDERS>> GetProductionOrderList(object[] obj) => _trackService.ProductionOrderList(obj[0].IntParse());

        [HttpGet, HttpPost]
        public ActionResult<object[]> ProductionOrdersInsert(object[] obj) => _trackService.ProductionOrdersInsert(obj[0].ToStringParse());

        [HttpGet, HttpPost]
        public ActionResult<object[]> ProductionOrdersUpdate(object[] obj) => _trackService.ProductionOrdersUpdate(obj[0].IntParse(), obj[1].ToStringParse());

        [HttpGet, HttpPost]
        public ActionResult<object[]> ProductionOrdersDelete(object[] obj) => _trackService.ProductionOrdersDelete(obj[0].IntParse());

        [HttpGet, HttpPost]
        public ActionResult<List<TabPanel>> GetTabList() => _trackService.GetTabList();

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_CONTRACT_WEB_USERS>> GetContractList() => _trackService.GetContractList();

        [HttpGet, HttpPost]
        public ActionResult<List<ContractProcessList>> GetContractProcessList(object[] obj) => _trackService.GetContractProcessList(obj[0].IntParse());


        [HttpGet, HttpPost]
        public ActionResult<PRODUCTION_HEADER> GetProductionList(object[] obj) => _trackService.GetProductionList(obj[0].IntParse());
    }
}
