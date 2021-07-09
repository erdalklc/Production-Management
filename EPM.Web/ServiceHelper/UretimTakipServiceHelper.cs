using EPM.Dto.Base;
using EPM.Tools.Helpers;
using EPM.Track.Dto.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.ServiceHelper
{
    public class UretimTakipServiceHelper : EPMHttpCaller
    {
        public static List<SatinAlmaDetay> GetSatinAlmaDetay(int HEADER_ID)
        {
            string apiUrl = "GetSatinAlmaDetay";
            var list = PostRequest<object[], List<SatinAlmaDetay>>(EPMServiceType.Track, apiUrl, new object[] { HEADER_ID });
            return list;
        }

        public static List<SurecBilgileri> GetProcessInformations(int PO_HEADER_ID, int DETAIL_ID, int HEADER_ID)
        {
            string apiUrl = "GetProcessInformations";
            var list = PostRequest<object[], List<SurecBilgileri>>(EPMServiceType.Track, apiUrl, new object[] {  PO_HEADER_ID, DETAIL_ID, HEADER_ID});
            return list;
        }

        public static List<EgemenOrmeSiparis> GetEgemenOrmeList(string TAKIP_NO, int DETAIL_TAKIP_NO)
        {
            string apiUrl = "GetEgemenOrmeList";
            var list = PostRequest<object[], List<EgemenOrmeSiparis>>(EPMServiceType.Track, apiUrl, new object[] { TAKIP_NO, DETAIL_TAKIP_NO });
            return list;
        }

        public static List<KumasDepo> GetKumasDepoList(int ITEM_ID, int PO_HEADER_ID)
        {
            string apiUrl = "GetKumasDepoList";
            var list = PostRequest<object[], List<KumasDepo>>(EPMServiceType.Track, apiUrl, new object[] { ITEM_ID, PO_HEADER_ID });
            return list;
        }

        public static List<KesimFoyleri> GetKesimFoyuList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY)
        {
            string apiUrl = "GetKesimFoyuList";
            var list = PostRequest<object[], List<KesimFoyleri>>(EPMServiceType.Track, apiUrl, new object[] { ITEM_ID, PO_HEADER_ID, RENK_DETAY });
            return list;
        }

        public static List<BantBitisleri> GetBantList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY)
        {
            string apiUrl = "GetBantList";
            var list = PostRequest<object[], List<BantBitisleri>>(EPMServiceType.Track, apiUrl, new object[] { ITEM_ID, PO_HEADER_ID, RENK_DETAY });
            return list;
        }

        public static List<BantBitisleri> GetKaliteList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY)
        {
            string apiUrl = "GetKaliteList";
            var list = PostRequest<object[], List<BantBitisleri>>(EPMServiceType.Track, apiUrl, new object[] { ITEM_ID, PO_HEADER_ID, RENK_DETAY });
            return list;
        }

        public static object GetProductionDetail(int PO_HEADER_ID, int DETAIL_TAKIP_NO, int ITEM_ID, string RENKDETAY, string MODEL)
        {
            string apiUrl = "GetProductionDetail";
            var list = PostRequest<object[], object>(EPMServiceType.Track, apiUrl, new object[] { PO_HEADER_ID, DETAIL_TAKIP_NO, ITEM_ID, RENKDETAY , MODEL });
            return list;
        }

        public static List<KaliteGerceklesen> GetKaliteGerceklesenler(KaliteGerceklesenFilter filter)
        {
            string apiUrl = "GetKaliteGerceklesenler";
            var list = PostRequest<KaliteGerceklesenFilter, List<KaliteGerceklesen>>(EPMServiceType.Track, apiUrl, filter);
            return list;
        }

        public static List<UretimTakipListesi> GetUretimTakipListesi(string USER_CODE, TrackList_Filter liste)
        {
            string apiUrl = "GetUretimTakipListesi";
            var list = PostRequest<object[], List<UretimTakipListesi>>(EPMServiceType.Track, apiUrl, new object[] { USER_CODE, liste });
            return list;
        }

        public static List<EPM_MASTER_PRODUCTION_ORDERS> GetProductionOrderList(int HEADER_ID)
        {
            string apiUrl = "GetProductionOrderList";
            var list = PostRequest<object[], List<EPM_MASTER_PRODUCTION_ORDERS>>(EPMServiceType.Track, apiUrl, new object[] { HEADER_ID });
            return list;
        }

        public static object[] ProductionOrdersInsert(string Values)
        {
            string apiUrl = "ProductionOrdersInsert";
            var list = PostRequest<string, object[]>(EPMServiceType.Track, apiUrl, Values);
            return list;
        }

        public static object[] ProductionOrdersUpdate(int Key, string Values)
        {
            string apiUrl = "ProductionOrdersUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Track, apiUrl, new object[] { Key,Values });
            return list;
        }

        public static object[] ProductionOrdersDelete(int Key)
        {
            string apiUrl = "ProductionOrdersDelete";
            var list = PostRequest<object[], object[]>(EPMServiceType.Track, apiUrl, new object[] { Key });
            return list;
        }

        public static List<TabPanel> GetTabList()
        { 
            string apiUrl = "GetTabList";
            var list = GetRequest<List<TabPanel>>(EPMServiceType.Track, apiUrl);
            return list;
        }

        public static List<EPM_CONTRACT_WEB_USERS> GetContractList()
        {
            string apiUrl = "GetContractList";
            var list = GetRequest<List<EPM_CONTRACT_WEB_USERS>>(EPMServiceType.Track, apiUrl);
            return list;
        }

        public static List<ContractProcessList> GetContractProcessList(int HEADER_ID)
        {
            string apiUrl = "GetContractProcessList";
            var list = PostRequest<object[], List<ContractProcessList>>(EPMServiceType.Track, apiUrl, new object[] { HEADER_ID });
            return list;
        }
    }
}
