using EPM.Dto.Base;
using EPM.Dto.Loglar;
using EPM.Production.Dto.Extensions;
using EPM.Production.Dto.Production;
using System;
using System.Collections.Generic; 

namespace EPM.Production.Service.Services
{
    public interface IProductionService
    {
        public List<UretimListesi> UretimListesi(string MODEL, string SEZON, string URETIM_TIPI, int DURUM);

        public Tuple<UretimOnayliListe, List<UretimListesiAktarim>> UretimOnayliListe(Tuple<UretimOnayliListe, List<UretimListesiAktarim>> model);

        public List<EPM_PRODUCTION_BRANDS> GetBrandList(string USER_CODE, bool hepsi = true);

        public List<EPM_PRODUCTION_SUB_BRANDS> GetSubBrandList(string USER_CODE, bool hepsi = true);

        public List<EPM_PRODUCTION_SEASON> GetSeasonList();

        public List<EPM_PRODUCTION_MARKET> GetMarketList(bool hepsi = true);

        public List<EPM_PRODUCTION_ORDER_TYPES> GetOrderList(bool hepsi = true);

        public List<EPM_PRODUCTION_FABRIC_TYPES> GetFabricTypes(string USER_CODE, bool hepsi = true);

        public List<EPM_PRODUCTION_TYPES> GetProductionTypes(string USER_CODE, bool hepsi = true);

        public List<EPM_PRODUCT_COLLECTION_TYPES> GetCollectionTypes(bool hepsi = true);

        public List<EPM_PRODUCTION_BAND_GROUP> GetBandList(bool hepsi = true);

        public List<EPM_PRODUCT_GROUP> GetProductGroupList(bool hepsi = true);

        public List<EPM_PRODUCTION_RECIPE> GetRecipeList(bool hepsi = true);

        public List<EPM_PRODUCTION_RECIPE> GetRecipeListByType(bool hepsi = true, int TYPE = 1);

        public List<ENUMMODEL> GetApprovalStatueList();

        public List<MasterList> OnayliUretimListesi(string USER_CODE, UretimOnayliListe liste);

        public List<VerticalList> UretimListesiDikey(string USER_CODE, UretimOnayliListe liste);

        public List<DetailList> OnayliUretimListesiDetail(int ID);

        public object[] UretimOnayliDetailUpdate(string USER_CODE, int Key, string Values, ILogService logRepository);

        public object[] UretimOnayliDetailDelete(string USER_CODE, int Key, ILogService logRepository);

        public EPM_MASTER_PRODUCTION_D UretimOnayliDetailInsert(string Values);

        public object[] UretimOnayliUpdate(string USER_CODE, int Key, string Values, ILogService logRepository);

        public object[] UretimOnayliDelete(string USER_CODE, int Key, ILogService logRepositor);

        public EPM_MASTER_PRODUCTION_H UretimOnayliInsert(string Values);

        public List<EPM_PRODUCTION_CURRENCY_UNITS> GetCurrencyUnits();

        public List<LOG_EPM_MASTER_PRODUCTION_H> OnayliUretimListesiLog(int HEADER_ID, ILogService logRepository);

        public List<LOG_EPM_MASTER_PRODUCTION_D> OnayliUretimListesiLogDetail(int DETAIL_ID, ILogService logRepository);
    }
}
