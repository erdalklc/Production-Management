using EPM.Core.FormModels.Uretim;
using EPM.Core.FormModels.UretimTakip;
using EPM.Core.Nesneler;
using EPM.Fason.Dto.Fason;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Core.Services
{
    public interface IUretimTakipService
    {
        public List<SatinAlmaDetay> SatinAlmaDetay(int HEADER_ID);

        public List<SurecBilgileri> GetProcessInformations(int PO_HEADER_ID, int DETAIL_ID, int HEADER_ID);

        public object GetProductionDetail(int PO_HEADER_ID, int DETAIL_TAKIP_NO, int ITEM_ID, string RENKDETAY, string MODEL);

        public List<KaliteGerceklesen> GetKaliteGerceklesenler(KaliteGerceklesenFilter filter);

        public IEnumerable<UretimTakipListesi> GetUretimTakipListesi(HttpContext context, UretimOnayliListe liste);

        public IEnumerable<EPM_MASTER_PRODUCTION_ORDERS> ProductionOrderList(int HEADER_ID);

        public object[] ProductionOrdersInsert(string Values);

        public object[] ProductionOrdersUpdate(int Key, string Values);

        public object[] ProductionOrdersDelete(int Key);

        public IEnumerable<EgemenOrmeSiparis> GetEgemenOrmeList(string TAKIP_NO, int DETAIL_TAKIP_NO);

        public IEnumerable<KumasDepo> GetKumasDepoList(int ITEM_ID, int PO_HEADER_ID);

        public IEnumerable<KesimFoyleri> GetKesimFoyuList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY);

        public IEnumerable<TabPanel> GetTabList();

        public IEnumerable<BantBitisleri> GetBantList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY);

        public IEnumerable<BantBitisleri> GetKaliteList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY);

        public IEnumerable<EPM_CONTRACT_WEB_USERS> GetContractList();

        public IEnumerable<ContractProcessList> GetContractProcessList(HttpContext context, int HEADER_ID);

        public Task<Tuple<PRODUCTION_HEADER, List<PRODUCTION_PROCESS>, List<PRODUCTION_FASON_USERS>>> FasonTakipListeAyarlaAsync(string Url, int HEADER_ID);

        public Task<object[]> FasonSiparisOlusturAsync(string url, PRODUCTION_HEADER header, List<PRODUCTION_PROCESS> plan, int firmaBilgi, DateTime terminTarihi);
    }
}
