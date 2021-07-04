using EPM.Core.FormModels.Uretim;
using EPM.Core.Loglar;
using EPM.Core.Managers;
using EPM.Core.Models;
using EPM.Core.Nesneler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EPM.Core.Services
{
    public interface IUretimService
    { 
        public List<UretimListesi> UretimListesi(HttpContext context, string MODEL, string SEZON, string URETIM_TIPI,int DURUM);
         
      

        public object[] UretimListesiAktarExcelYukle(HttpRequest request);

        public Tuple<UretimOnayliListe, List<UretimListesiAktarim>> UretimOnayliListe(Tuple<UretimOnayliListe, List<UretimListesiAktarim>> model);

        public IEnumerable<EPM_PRODUCTION_BRANDS> GetBrandList(HttpContext context, bool hepsi = true);

        public IEnumerable<EPM_PRODUCTION_SUB_BRANDS> GetSubBrandList(HttpContext context, bool hepsi = true);

        public IEnumerable<EPM_PRODUCTION_SEASON> GetSeasonList();

        public IEnumerable<EPM_PRODUCTION_MARKET> GetMarketList(bool hepsi = true);

        public IEnumerable<EPM_PRODUCTION_ORDER_TYPES> GetOrderList(bool hepsi = true);

        public IEnumerable<EPM_PRODUCTION_FABRIC_TYPES> GetFabricTypes(HttpContext context, bool hepsi = true);

        public IEnumerable<EPM_PRODUCTION_TYPES> GetProductionTypes(HttpContext context, bool hepsi = true);

        public IEnumerable<EPM_PRODUCT_COLLECTION_TYPES> GetCollectionTypes(bool hepsi = true);

        public IEnumerable<EPM_PRODUCTION_BAND_GROUP> GetBandList(bool hepsi = true);

        public IEnumerable<ENUMMODEL> GetApprovalStatueList();

        public IEnumerable<EPM_PRODUCT_GROUP> GetProductGroupList(bool hepsi = true);

        public IEnumerable<EPM_PRODUCTION_RECIPE> GetRecipeList(bool hepsi = true);

        public IEnumerable<EPM_PRODUCTION_CURRENCY_UNITS> GetCurrencyUnits();

        public IEnumerable<MasterList> OnayliUretimListesi(HttpContext context, UretimOnayliListe liste);

        public IEnumerable<VerticalList> UretimListesiDikey(HttpContext context, UretimOnayliListe liste);

        public IEnumerable<DetailList> OnayliUretimListesiDetail(int ID);

        public object[] UretimOnayliDetailUpdate(HttpContext context, int Key, string Values, ILogService logRepository);

        public object[] UretimOnayliDetailDelete(int Key); 

        public EPM_MASTER_PRODUCTION_D UretimOnayliDetailInsert(string Values);

        public object[] UretimOnayliUpdate(HttpContext context, int Key, string Values, ILogService logRepository);

        public object[] UretimOnayliDelete(int Key);

        public EPM_MASTER_PRODUCTION_H UretimOnayliInsert(string Values); 

        public IEnumerable<LOG_EPM_MASTER_PRODUCTION_H> OnayliUretimListesiLog(int HEADER_ID,ILogService logRepository);

        public IEnumerable<LOG_EPM_MASTER_PRODUCTION_D> OnayliUretimListesiLogDetail(int DETAIL_ID, ILogService logRepository);
    }
}
