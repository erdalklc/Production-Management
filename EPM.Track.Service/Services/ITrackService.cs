using EPM.Dto.Base;
using EPM.Fason.Dto.Fason;
using EPM.Track.Dto.Track;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPM.Track.Service.Services
{
    public interface ITrackService
    {
        public List<SatinAlmaDetay> SatinAlmaDetay(int HEADER_ID);

        public List<SurecBilgileri> GetProcessInformations(int PO_HEADER_ID, int DETAIL_ID, int HEADER_ID);

        public object GetProductionDetail(int PO_HEADER_ID, int DETAIL_TAKIP_NO, int ITEM_ID, string RENKDETAY, string MODEL);

        public List<KaliteGerceklesen> GetKaliteGerceklesenler(KaliteGerceklesenFilter filter);

        public List<UretimTakipListesi> GetUretimTakipListesi(string USER_CODE, TrackList_Filter liste);

        public List<EPM_MASTER_PRODUCTION_ORDERS> ProductionOrderList(int HEADER_ID);

        public object[] ProductionOrdersInsert(string Values);

        public object[] ProductionOrdersUpdate(int Key, string Values);

        public object[] ProductionOrdersDelete(int Key);

        public List<EgemenOrmeSiparis> GetEgemenOrmeList(string TAKIP_NO, int DETAIL_TAKIP_NO);

        public List<KumasDepo> GetKumasDepoList(int ITEM_ID, int PO_HEADER_ID);

        public List<KesimFoyleri> GetKesimFoyuList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY);

        public List<TabPanel> GetTabList();

        public List<BantBitisleri> GetBantList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY);

        public List<BantBitisleri> GetKaliteList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY);

        public List<EPM_CONTRACT_WEB_USERS> GetContractList();

        public List<ContractProcessList> GetContractProcessList(int HEADER_ID);

        public PRODUCTION_HEADER GetProductionList(int HEADER_ID);

        public Task PROCESSLERIANALIZET(CancellationToken stoppingToken);

        public List<NOS_TRACK> GetNosTrack();

    }
}
