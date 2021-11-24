using EPM.Dto.Base;
using EPM.Dto.Loglar;
using EPM.Production.Dto.Extensions;
using EPM.Production.Dto.Production;
using EPM.Production.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Production.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IProductionService _productionService;
        private readonly ILogService _logService;
        public ProductionController(IProductionService productionService, ILogService logService)
        {
            _productionService = productionService;
            _logService = logService;
        }

        [HttpGet, HttpPost]
        public ActionResult<List<UretimListesi>> UretimListesi(object[] obj) => _productionService.UretimListesi(obj[0].ToString(), obj[1].ToString(), obj[2].ToString(), obj[3].IntParse());

        [HttpGet, HttpPost]
        public ActionResult<Tuple<UretimOnayliListe, List<UretimListesiAktarim>>> UretimOnayliListe(object[] obj) => _productionService.UretimOnayliListe((Tuple<UretimOnayliListe, List<UretimListesiAktarim>>)obj[0]);

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_BRANDS>> GetBrandList(object[] obj) => _productionService.GetBrandList(obj[0].ToString(), JsonConvert.DeserializeObject<List<EPM_USER_BRANDS>>(JsonConvert.SerializeObject(obj[2])), obj[1].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_SUB_BRANDS>> GetSubBrandList(object[] obj) => _productionService.GetSubBrandList(obj[0].ToString(), obj[1].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_SEASON>> GetSeasonList() => _productionService.GetSeasonList();

        public ActionResult<List<EPM_PRODUCTION_MARKET>> GetMarketList(object[] obj) => _productionService.GetMarketList(obj[0].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_ORDER_TYPES>> GetOrderList(object[] obj) => _productionService.GetOrderList(obj[0].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_FABRIC_TYPES>> GetFabricTypes(object[] obj) => _productionService.GetFabricTypes(obj[0].ToString(), JsonConvert.DeserializeObject<List<EPM_USER_FABRIC_TYPES>>(JsonConvert.SerializeObject(obj[2])), obj[1].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_TYPES>> GetProductionTypes(object[] obj) => _productionService.GetProductionTypes(obj[0].ToString(), JsonConvert.DeserializeObject<List<EPM_USER_PRODUCTION_TYPES>>(JsonConvert.SerializeObject(obj[2])), obj[1].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCT_COLLECTION_TYPES>> GetCollectionTypes(object[] obj) => _productionService.GetCollectionTypes(obj[0].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_BAND_GROUP>> GetBandList(object[] obj) => _productionService.GetBandList(obj[0].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCT_GROUP>> GetProductGroupList(object[] obj) => _productionService.GetProductGroupList(obj[0].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_RECIPE>> GetRecipeList(object[] obj) => _productionService.GetRecipeList(obj[0].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_RECIPE>> GetRecipeListByType(object[] obj) => _productionService.GetRecipeListByType(obj[0].BooleanParse(), obj[1].IntParse());

        [HttpGet, HttpPost]
        public ActionResult<List<ENUMMODEL>> GetApprovalStatueList(object[] obj) => _productionService.GetApprovalStatueList(obj[0].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<ENUMMODEL>> GetStatusList(object[] obj) => _productionService.GetStatusList(obj[0].BooleanParse());
        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_FLAGS>> GetFlagList(object[] obj) => _productionService.GetFlagList(obj[0].BooleanParse());

        [HttpGet, HttpPost]
        public ActionResult<List<MasterList>> OnayliUretimListesi(object[] obj) => _productionService.OnayliUretimListesi(obj[0].ToString(), JsonConvert.DeserializeObject<UretimOnayliListe>(obj[1].ToStringParse()));

        [HttpGet, HttpPost]
        public ActionResult<List<VerticalList>> UretimListesiDikey(object[] obj) => _productionService.UretimListesiDikey(obj[0].ToString(), JsonConvert.DeserializeObject<UretimOnayliListe>(obj[1].ToStringParse()));

        [HttpGet, HttpPost]
        public ActionResult<List<DetailList>> OnayliUretimListesiDetail(object[] obj) => _productionService.OnayliUretimListesiDetail(obj[0].IntParse());

        [HttpGet, HttpPost]
        public ActionResult<object[]> UretimOnayliDetailUpdate(object[] obj) => _productionService.UretimOnayliDetailUpdate(obj[0].ToString(), obj[1].IntParse(), obj[2].ToString(), _logService);

        [HttpGet, HttpPost]
        public ActionResult<object[]> UretimOnayliDetailDelete(object[] obj) => _productionService.UretimOnayliDetailDelete(obj[0].ToString(),obj[1].IntParse(),_logService);

        [HttpGet, HttpPost]
        public ActionResult<EPM_MASTER_PRODUCTION_D> UretimOnayliDetailInsert(object[] obj) => _productionService.UretimOnayliDetailInsert(obj[0].ToString());

        [HttpGet, HttpPost]
        public ActionResult<object[]> UretimOnayliUpdate(object[] obj) => _productionService.UretimOnayliUpdate(obj[0].ToString(), obj[1].IntParse(), obj[2].ToString(), _logService);

        [HttpGet, HttpPost]
        public ActionResult<object[]> UretimOnayliDelete(object[] obj) => _productionService.UretimOnayliDelete(obj[0].ToString(), obj[1].IntParse(), _logService);

        [HttpGet, HttpPost]
        public ActionResult<EPM_MASTER_PRODUCTION_H> UretimOnayliInsert(object[] obj) => _productionService.UretimOnayliInsert(obj[0].ToString());

        [HttpGet, HttpPost]
        public ActionResult<List<EPM_PRODUCTION_CURRENCY_UNITS>> GetCurrencyUnits() => _productionService.GetCurrencyUnits();

        [HttpGet, HttpPost]
        public ActionResult<List<LOG_EPM_MASTER_PRODUCTION_H>> OnayliUretimListesiLog(object[] obj) => _productionService.OnayliUretimListesiLog(obj[0].IntParse(), _logService);

        [HttpGet, HttpPost]
        public ActionResult<List<LOG_EPM_MASTER_PRODUCTION_D>> OnayliUretimListesiLogDetail(object[] obj) => _productionService.OnayliUretimListesiLogDetail(obj[0].IntParse(), _logService);
    }
}
