using EPM.Dto.Base;
using EPM.Dto.Loglar;
using EPM.Production.Dto.Extensions;
using EPM.Production.Dto.Production;
using EPM.Tools.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.ServiceHelper
{
    public class ProductionServiceHelper : EPMHttpCaller
    {
     
        public static List<UretimListesi> UretimListesi(string USER_CODE, string MODEL, string SEZON, string URETIM_TIPI, int DURUM)
        {
            string apiUrl = "UretimListesi";
            var list = PostRequest<object[], List<UretimListesi>>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE, MODEL, SEZON, URETIM_TIPI, DURUM });
            return list;
        }         

        public static Tuple<UretimOnayliListe, List<UretimListesiAktarim>> UretimOnayliListe(Tuple<UretimOnayliListe, List<UretimListesiAktarim>> model)
        {
            string apiUrl = "UretimOnayliListe";
            var list = PostRequest<object[], Tuple<UretimOnayliListe, List<UretimListesiAktarim>>>(EPMServiceType.Production, apiUrl, new object[] { model });
            return list;
        }

        

        public static List<EPM_PRODUCTION_BRANDS> GetBrandList(string USER_CODE, bool hepsi = true)
        {
            string apiUrl = "GetBrandList";
            var list = PostRequest<object[], List<EPM_PRODUCTION_BRANDS>>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE,hepsi });
            return list;
        }

        public static List<EPM_PRODUCTION_SUB_BRANDS> GetSubBrandList(string USER_CODE, bool hepsi = true)
        {
            string apiUrl = "GetSubBrandList";
            var list = PostRequest<object[], List<EPM_PRODUCTION_SUB_BRANDS>>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE, hepsi });
            return list;
        }

        public static List<EPM_PRODUCTION_SEASON> GetSeasonList()
        {
            string apiUrl = "GetSeasonList";
            var list = GetRequest<List<EPM_PRODUCTION_SEASON>>(EPMServiceType.Production, apiUrl);
            return list;
        }

        public static List<EPM_PRODUCTION_MARKET> GetMarketList(bool hepsi = true)
        {
            string apiUrl = "GetMarketList";
            var list = PostRequest<object[], List<EPM_PRODUCTION_MARKET>>(EPMServiceType.Production, apiUrl, new object[] { hepsi});
            return list;
        }

        public static List<EPM_PRODUCTION_ORDER_TYPES> GetOrderList(bool hepsi = true)
        {
            string apiUrl = "GetOrderList";
            var list = PostRequest<object[], List<EPM_PRODUCTION_ORDER_TYPES>>(EPMServiceType.Production, apiUrl, new object[] { hepsi });
            return list;
        }

        public static List<EPM_PRODUCTION_FABRIC_TYPES> GetFabricTypes(string USER_CODE, bool hepsi = true)
        {
            string apiUrl = "GetFabricTypes";
            var list = PostRequest<object[], List<EPM_PRODUCTION_FABRIC_TYPES>>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE,hepsi });
            return list;
        }

        public static List<EPM_PRODUCTION_TYPES> GetProductionTypes(string USER_CODE, bool hepsi = true)
        {
            string apiUrl = "GetProductionTypes";
            var list = PostRequest<object[], List<EPM_PRODUCTION_TYPES>>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE, hepsi });
            return list;
        }

        public static List<EPM_PRODUCT_COLLECTION_TYPES> GetCollectionTypes(bool hepsi = true)
        {
            string apiUrl = "GetCollectionTypes";
            var list = PostRequest<object[], List<EPM_PRODUCT_COLLECTION_TYPES>>(EPMServiceType.Production, apiUrl, new object[] {  hepsi });
            return list;
        }

        public static List<EPM_PRODUCTION_BAND_GROUP> GetBandList(bool hepsi = true)
        {
            string apiUrl = "GetBandList";
            var list = PostRequest<object[], List<EPM_PRODUCTION_BAND_GROUP>>(EPMServiceType.Production, apiUrl, new object[] { hepsi });
            return list;
        }

        public static List<ENUMMODEL> GetApprovalStatueList()
        {
            string apiUrl = "GetApprovalStatueList";
            var list = GetRequest<List<ENUMMODEL>>(EPMServiceType.Production, apiUrl);
            return list;
        }

        public static List<EPM_PRODUCTION_FLAGS> GetFlagList(bool hepsi = true)
        {
            string apiUrl = "GetFlagList";
            var list = PostRequest<object[], List<EPM_PRODUCTION_FLAGS>>(EPMServiceType.Production, apiUrl, new object[] { hepsi });
            return list;
        }

        public static List<EPM_PRODUCT_GROUP> GetProductGroupList(bool hepsi = true)
        {
            string apiUrl = "GetProductGroupList";
            var list = PostRequest<object[], List<EPM_PRODUCT_GROUP>>(EPMServiceType.Production, apiUrl, new object[] { hepsi });
            return list;
        }

        public static List<EPM_PRODUCTION_RECIPE> GetRecipeList(bool hepsi = true)
        {
            string apiUrl = "GetRecipeList";
            var list = PostRequest<object[], List<EPM_PRODUCTION_RECIPE>>(EPMServiceType.Production, apiUrl, new object[] { hepsi });
            return list;
        }

        public static List<EPM_PRODUCTION_RECIPE> GetRecipeListByType(bool hepsi = true, int TYPE = 1)
        {
            string apiUrl = "GetRecipeListByType";
            var list = PostRequest<object[], List<EPM_PRODUCTION_RECIPE>>(EPMServiceType.Production, apiUrl, new object[] { hepsi ,TYPE});
            return list;
        }

        public static List<EPM_PRODUCTION_CURRENCY_UNITS> GetCurrencyUnits()
        {
            string apiUrl = "GetCurrencyUnits";
            var list = GetRequest<List<EPM_PRODUCTION_CURRENCY_UNITS>>(EPMServiceType.Production, apiUrl);
            return list;
        }

        public static List<MasterList> OnayliUretimListesi(string USER_CODE, UretimOnayliListe liste)
        {
            string apiUrl = "OnayliUretimListesi";
            var list = PostRequest<object[], List<MasterList>>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE, liste });
            return list;
        }

        public static List<VerticalList> UretimListesiDikey(string USER_CODE, UretimOnayliListe liste)
        {
            string apiUrl = "UretimListesiDikey";
            var list = PostRequest<object[], List<VerticalList>>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE, liste });
            return list;
        }

        public static List<DetailList> OnayliUretimListesiDetail(int ID)
        {
            string apiUrl = "OnayliUretimListesiDetail";
            var list = PostRequest<object[], List<DetailList>>(EPMServiceType.Production, apiUrl, new object[] { ID });
            return list;
        }

        public static object[] UretimOnayliDetailUpdate(string USER_CODE, int Key, string Values)
        {
            string apiUrl = "UretimOnayliDetailUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE,Key,Values });
            return list;
        }

        public static object[] UretimOnayliDetailDelete(string USER_CODE, int Key)
        {
            string apiUrl = "UretimOnayliDetailDelete";
            var list = PostRequest<object[], object[]>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE,Key });
            return list;
        }

        public static EPM_MASTER_PRODUCTION_D UretimOnayliDetailInsert(string Values)
        {
            string apiUrl = "UretimOnayliDetailInsert";
            var list = PostRequest<object[], EPM_MASTER_PRODUCTION_D>(EPMServiceType.Production, apiUrl, new object[] { Values });
            return list;
        }

        public static object[] UretimOnayliUpdate(string USER_CODE, int Key, string Values)
        {
            string apiUrl = "UretimOnayliUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE, Key , Values });
            return list;
        }

        public static object[] UretimOnayliDelete(string USER_CODE, int Key)
        {
            string apiUrl = "UretimOnayliDelete";
            var list = PostRequest<object[], object[]>(EPMServiceType.Production, apiUrl, new object[] { USER_CODE,Key });
            return list;
        }

        public static EPM_MASTER_PRODUCTION_H UretimOnayliInsert(string Values)
        {
            string apiUrl = "UretimOnayliInsert";
            var list = PostRequest<object[], EPM_MASTER_PRODUCTION_H>(EPMServiceType.Production, apiUrl, new object[] { Values });
            return list;
        }

        public static List<LOG_EPM_MASTER_PRODUCTION_H> OnayliUretimListesiLog(int HEADER_ID)
        {
            string apiUrl = "OnayliUretimListesiLog";
            var list = PostRequest<object[], List<LOG_EPM_MASTER_PRODUCTION_H>>(EPMServiceType.Production, apiUrl, new object[] { HEADER_ID });
            return list;
        }

        public static List<LOG_EPM_MASTER_PRODUCTION_D> OnayliUretimListesiLogDetail(int DETAIL_ID)
        {
            string apiUrl = "OnayliUretimListesiLogDetail";
            var list = PostRequest<object[], List<LOG_EPM_MASTER_PRODUCTION_D>>(EPMServiceType.Production, apiUrl, new object[] { DETAIL_ID });
            return list;
        }
    }
}
