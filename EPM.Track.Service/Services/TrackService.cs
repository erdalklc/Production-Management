using EPM.Dto.Base;
using EPM.Track.Dto.Track;
using EPM.Track.Dto.Extensions;
using EPM.Track.Repository.Repository;
using EPM.Track.Service.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EPM.Fason.Dto.Fason;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace EPM.Track.Service.Services
{
    public class TrackService :ITrackService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IEgemenRepository _egemenRepository;
        private readonly ILogger<TrackService> _logger;
        public TrackService(ITrackRepository trackRepository, IEgemenRepository egemenRepository, ILogger<TrackService> logger)
        {
            _trackRepository = trackRepository;
            _egemenRepository = egemenRepository;
            _logger = logger;
        }

        public List<SatinAlmaDetay> SatinAlmaDetay(int HEADER_ID)
        {
            string sql = DetailSQL() + " AND PO_HEADER_ID IN (SELECT PO_HEADER_ID FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE HEADER_ID=" + HEADER_ID + ")";
            List<SatinAlmaDetay> detay = _trackRepository.DeserializeList<SatinAlmaDetay>(sql);
            detay.ForEach(ob => ob.HEADER_ID = HEADER_ID);
            return detay;
        }

        public List<SurecBilgileri> GetProcessInformations(int PO_HEADER_ID, int DETAIL_ID, int HEADER_ID)
        {
            string sql = string.Format(@"SELECT TL.ID, TL.PO_HEADER_ID,
                             TL.PROCESS_ID,
                             TL.START_DATE,
                             TL.END_DATE,
                             TL.STATUS,
                             PI.PROCESS_NAME 
  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST TL
       INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI
          ON PI.ID = TL.PROCESS_ID
       INNER JOIN
       FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD
          ON     RD.PROCESS_ID = TL.PROCESS_ID
             AND RD.HEADER_ID =
                    (SELECT RECIPE
                       FROM FDEIT005.EPM_MASTER_PRODUCTION_H H
                      WHERE H.ID = TL.HEADER_ID)
WHERE TL.PO_HEADER_ID={0} AND TL.DETAIL_ID={1} AND TL.HEADER_ID={2}
ORDER BY RD.QUEUE", PO_HEADER_ID, DETAIL_ID, HEADER_ID);

            return _trackRepository.DeserializeList<SurecBilgileri>(sql);

        }

        public List<EgemenOrmeSiparis> GetEgemenOrmeList(string TAKIP_NO, int DETAIL_TAKIP_NO)
        {
            List<EgemenOrmeSiparis> listEgemenOrme = _egemenRepository.DeserializeList<EgemenOrmeSiparis>(new EgemenOrmeHelper().ORMEALINANSIPARISSATIRTAKIP(TAKIP_NO, DETAIL_TAKIP_NO.ToString()),FirebirdConnectionDB.ORME);
            return listEgemenOrme;

        }

        public List<KumasDepo> GetKumasDepoList(int ITEM_ID, int PO_HEADER_ID)
        {
            List<KumasDepo> listKumas = _trackRepository.DeserializeList<KumasDepo>(new KumasDepoHelper().KumasDepoHareketleri(ITEM_ID, PO_HEADER_ID));
            return listKumas; 
        } 

        public List<KesimFoyleri> GetKesimFoyuList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY)
        {
            EPM_MASTER_PRODUCTION_ORDERS order = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_ORDERS>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE PO_HEADER_ID=" + PO_HEADER_ID);
            EPM_MASTER_PRODUCTION_H header = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_H>(order.HEADER_ID);
            List<KesimFoyleri> listKesim = _trackRepository.DeserializeList<KesimFoyleri>(new KesimFoyuHelper().KesimFoyleri(ITEM_ID, PO_HEADER_ID, RENK_DETAY, header.MODEL));
            return listKesim;

        }

        public List<BantBitisleri> GetBantList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY)
        {
            List<BantBitisleri> bantBitisler;
            EPM_MASTER_PRODUCTION_ORDERS order = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_ORDERS>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE PO_HEADER_ID=" + PO_HEADER_ID);
            EPM_MASTER_PRODUCTION_H header = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_H>(order.HEADER_ID);
            List<string> listKesim = _trackRepository.DeserializeList<string>("SELECT DISTINCT WIP_ENTITY_NAME FROM (" + new KesimFoyuHelper().KesimFoyleri(ITEM_ID, PO_HEADER_ID, RENK_DETAY, header.MODEL) + ")A");
            string kesimFoyleri = "";
            foreach (var item in listKesim)
                kesimFoyleri += "'" + item + "',";
            kesimFoyleri = kesimFoyleri.TrimEnd(',');
            if (kesimFoyleri != "")
                bantBitisler = _egemenRepository.DeserializeList<BantBitisleri>(new EgemenDevanlayHelper().BantBitisleriSQL(header.COLOR,kesimFoyleri),FirebirdConnectionDB.DEVANLAY);
            else bantBitisler = new List<BantBitisleri>();

            return bantBitisler;

        }

        public List<BantBitisleri> GetKaliteList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY)
        {
            List<BantBitisleri> bantBitisler;
            EPM_MASTER_PRODUCTION_ORDERS order = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_ORDERS>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE PO_HEADER_ID=" + PO_HEADER_ID);
            EPM_MASTER_PRODUCTION_H header = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_H>(order.HEADER_ID);
            List<string> listKesim = _trackRepository.DeserializeList<string>("SELECT DISTINCT WIP_ENTITY_NAME FROM (" + new KesimFoyuHelper().KesimFoyleri(ITEM_ID, PO_HEADER_ID, RENK_DETAY, header.MODEL) + ")A");
            string kesimFoyleri = "";
            foreach (var item in listKesim)
                kesimFoyleri += "'" + item + "',";
            kesimFoyleri = kesimFoyleri.TrimEnd(',');

            if (kesimFoyleri != "")
                bantBitisler = _egemenRepository.DeserializeList<BantBitisleri>(new EgemenDevanlayHelper().KaliteBitisleriSQL(header.COLOR,kesimFoyleri), FirebirdConnectionDB.DEVANLAY);
            else bantBitisler = new List<BantBitisleri>();
            return bantBitisler;

        }

        public object GetProductionDetail(int PO_HEADER_ID, int DETAIL_TAKIP_NO, int ITEM_ID, string RENKDETAY, string MODEL)
        {
            EgemenDevanlayHelper devanlayHelper = new EgemenDevanlayHelper();

            EPM_MASTER_PRODUCTION_ORDERS order = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_ORDERS>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE PO_HEADER_ID=" + PO_HEADER_ID);
            EPM_MASTER_PRODUCTION_H header = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_H>(order.HEADER_ID);
            int bant = 0;
            int kalite = 0;
            int kesim = 0;
            int tasnif = 0;
            int planlanan = 0;
            DataSet set = new DataSet();
            DataTable dtBant = new DataTable();
            DataTable dtKalite = new DataTable();
            List<EgemenOrmeSiparis> listEgemenOrme = _egemenRepository.DeserializeList<EgemenOrmeSiparis>(new EgemenOrmeHelper().ORMEALINANSIPARISSATIRTAKIP(PO_HEADER_ID.ToString(), DETAIL_TAKIP_NO.ToString()));
            List<KumasDepo> listKumas = _trackRepository.DeserializeList<KumasDepo>(new KumasDepoHelper().KumasDepoHareketleri(ITEM_ID, PO_HEADER_ID));
            List<KesimFoyleri> listKesim = _trackRepository.DeserializeList<KesimFoyleri>(new KesimFoyuHelper().KesimFoyleri(ITEM_ID, PO_HEADER_ID, RENKDETAY, MODEL));
            string _kesimler = "";
            if (listKesim.Count > 0)
                _kesimler = string.Join(',', listKesim.Select(ob => ob.WIP_ENTITY_NAME).Distinct().ToList());
            if (_kesimler != "")
            {
                bant = _egemenRepository.ReadInteger(devanlayHelper.BantBitisleriToplam(header.COLOR,_kesimler));
                kalite = _egemenRepository.ReadInteger(devanlayHelper.KaliteBitisleriToplam(header.COLOR,_kesimler));
            }
            kesim = listKesim.Sum(ob => ob.FIILI_KESIM).IntParse();
            tasnif = listKesim.Sum(ob => ob.TASNIF_MIKTARI).IntParse();
            planlanan = listKesim.Sum(ob => ob.PLANLANAN_KESIM).IntParse();

            return 1;
        }

        string DetailSQL()
        {
            string sql = @"
                    SELECT *
        FROM (SELECT PH.SEGMENT1 AS TAKIP_NO,
               PH.PO_HEADER_ID,
               PL.PO_LINE_ID DETAIL_TAKIP_NO, 
               (SELECT   pov.vendor_name
                            FROM po.po_vendor_sites_all pos, hr_locations hr, 
                            po.po_vendors pov, apps.org_organization_definitions ood 
                            WHERE hr.location_id = pos.bill_to_location_id 
                            AND pos.vendor_id = pov.vendor_id --and pov.END_DATE_ACTIVE is null 
                            --and pos.INACTIVE_DATE is null
                            AND ood.organization_id = hr.inventory_organization_id 
                            AND ood.operating_unit = pos.org_id 
                            AND pos.vendor_site_id =PH.VENDOR_SITE_ID) FIRMA,
               AG.FIRST_NAME || ' ' || AG.LAST_NAME AS AGENT_NAME,
               PH.CREATION_DATE,
               PL.ITEM_ID,
               MSI.DESCRIPTION URUN,
               MSI.SEGMENT1 || '.' || MSI.SEGMENT2 || '.' || MSI.SEGMENT3
                  AS KALEM,
               MSI.SEGMENT1 MODELDETAY,
               MSI.SEGMENT2 RENKDETAY,
               PL.UNIT_MEAS_LOOKUP_CODE AS BIRIM,
               PL.UNIT_PRICE BIRIM_FIYAT,
               PL.QUANTITY AS TEDARIK,
               PL.UNIT_PRICE * PL.QUANTITY AS TUTAR
          FROM APPS.PO_HEADERS_ALL PH
               INNER JOIN APPS.PER_PEOPLE_F AG ON AG.PERSON_ID = PH.AGENT_ID
               INNER JOIN APPS.PO_LINES_ALL PL
                  ON PL.PO_HEADER_ID = PH.PO_HEADER_ID
               INNER JOIN
               APPS.MTL_SYSTEM_ITEMS_B MSI
                  ON     MSI.INVENTORY_ITEM_ID = PL.ITEM_ID
                     AND MSI.ORGANIZATION_ID = 111
         WHERE     AG.EMPLOYEE_NUMBER IS NOT NULL
               AND TRUNC (SYSDATE) BETWEEN AG.EFFECTIVE_START_DATE
                                       AND AG.EFFECTIVE_END_DATE) A
        WHERE 0 = 0 ";
            return sql;
        }

        public List<KaliteGerceklesen> GetKaliteGerceklesenler(KaliteGerceklesenFilter filter)
        { 
            return _trackRepository.DeserializeList<KaliteGerceklesen>(new EgemenDevanlayHelper().KaliteGerceklesenSQL(filter.MODEL, filter.COLOR, filter.BEDEN, "", filter.SEASON));
        }

        public List<UretimTakipListesi> GetUretimTakipListesi(string USER_CODE, TrackList_Filter liste)
        {
            if (liste.PRODUCTION_TYPE == 1)
            {
                string sql = @" SELECT A.*,
       CASE
          WHEN SATIN_ALMA_BAGLANTI > 0
          THEN
             CASE
                WHEN TANIMLANAN = 0
                THEN
                   'TAKIP BASLATILMADI'
                ELSE
                   CASE
                      WHEN TAMAMLANAN = TANIMLANAN
                      THEN
                         'TAMAMLANDI'
                      ELSE
                         CASE
                            WHEN LAST_STATE IS NULL THEN 'YOK'
                            ELSE LAST_STATE
                         END
                   END
             END
          ELSE
             'SATIN ALMA EŞLEŞTİRİLMEDİ'
       END
          AS PROCESS_INFO,
       CASE
          WHEN FABRIC_TYPE = 1 AND PRODUCTION_TYPE = 1
          THEN
             (SELECT TAKIP_NO
                FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS O
               WHERE O.HEADER_ID = A.ID AND ROWNUM = 1)
          ELSE
             ''
       END
          TAKIP_NO
  FROM (SELECT H.*,
 VL.BANT AS BANT_ADET
                ,VL.KESIM AS KESIM_ADET
                ,VL.TASNIF AS TASNIF_ADET
                ,VL.KALITE AS KALITE_ADET,
               (SELECT SUM (D.QUANTITY)
                  FROM FDEIT005.EPM_MASTER_PRODUCTION_D D
                 WHERE D.HEADER_ID = H.ID)
                  AS QUANTITY,
               CASE WHEN RECIPE IN (1,2,3)  THEN (SELECT END_DATE
                  FROM (  SELECT PI.PROCESS_NAME,
                                 PL.END_DATE,
                                 PL.PO_HEADER_ID,
                                 PL.HEADER_ID,
                                 RD.HEADER_ID AS RECETE_ID,
                                 CASE
                                    WHEN RECIPE IN (1, 2, 3)
                                    THEN
                                       (SELECT COUNT (*)
                                          FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                         WHERE     STATUS = 2
                                               AND PL2.PO_HEADER_ID =
                                                      PL.PO_HEADER_ID)
                                    ELSE
                                       0
                                 END
                                    AS TAMAMLANAN,
                                 (SELECT COUNT (*)
                                    FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                   WHERE PL2.PO_HEADER_ID = PL.PO_HEADER_ID)
                                    AS TANIMLANAN
                            FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
                                 INNER JOIN
                                 FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI
                                    ON PI.ID = PL.PROCESS_ID
                                 INNER JOIN
                                 FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD
                                    ON RD.PROCESS_ID = PL.PROCESS_ID
                           WHERE PL.STATUS = 1
                        ORDER BY PL.PO_HEADER_ID, RD.QUEUE) DE
                 WHERE     DE.HEADER_ID = H.ID
                       AND DE.RECETE_ID = H.RECIPE
                       AND ROWNUM = 1) ELSE NULL END
                  END_DATE,
               CASE WHEN RECIPE IN (1,2,3)  THEN (SELECT PROCESS_NAME
                  FROM (  SELECT PI.PROCESS_NAME,
                                 PL.END_DATE,
                                 PL.PO_HEADER_ID,
                                 PL.HEADER_ID,
                                 RD.HEADER_ID AS RECETE_ID,
                                 (SELECT COUNT (*)
                                    FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                   WHERE     STATUS = 2
                                         AND PL2.PO_HEADER_ID = PL.PO_HEADER_ID)
                                    AS TAMAMLANAN,
                                 (SELECT COUNT (*)
                                    FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                   WHERE PL2.PO_HEADER_ID = PL.PO_HEADER_ID)
                                    AS TANIMLANAN
                            FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
                                 INNER JOIN
                                 FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI
                                    ON PI.ID = PL.PROCESS_ID
                                 INNER JOIN
                                 FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD
                                    ON RD.PROCESS_ID = PL.PROCESS_ID
                           WHERE PL.STATUS = 1
                        ORDER BY PL.PO_HEADER_ID, RD.QUEUE) DE
                 WHERE     DE.HEADER_ID = H.ID
                       AND DE.RECETE_ID = H.RECIPE
                       AND ROWNUM = 1) ELSE '' END
                  LAST_STATE,
               CASE WHEN RECIPE IN (1,2,3)  THEN(SELECT FDEIT005.PRODUCTION_FND_STATE (H.ID) FROM DUAL) ELSE '' END
                  MUSTBE_STATE,
               CASE WHEN RECIPE IN (1,2,3)  THEN (SELECT COUNT (*)
                  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                 WHERE STATUS IN (2, 4) AND PL2.HEADER_ID = H.ID)
                  ELSE 0 END AS TAMAMLANAN,
              CASE WHEN RECIPE IN (1,2,3)  THEN (SELECT COUNT (*)
                  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                 WHERE PL2.HEADER_ID = H.ID)
                  ELSE 0 END AS TANIMLANAN,
                CASE WHEN RECIPE IN (1,2,3)  THEN (SELECT MIN (START_DATE)
                  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST
                 WHERE     PROCESS_ID =
                              (SELECT ID
                                 FROM FDEIT005.EPM_PRODUCTION_PROCESS_INFO
                                WHERE ID = 8)
                       AND HEADER_ID = H.ID) ELSE NULL END AS 
                  KUMAS_START,
               CASE
                  WHEN RECIPE IN (1, 2, 3)
                  THEN
                     (SELECT COUNT (*)
                        FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS O
                       WHERE O.HEADER_ID = H.ID)
                  ELSE
                     3
               END
                  AS SATIN_ALMA_BAGLANTI
          FROM FDEIT005.EPM_MASTER_PRODUCTION_H H
          LEFT JOIN FDEIT005.EPM_TRACKING_PROCESS_VALUES VL ON VL.HEADER_ID = H.ID 
         WHERE     0 = 0 AND H.STATUS=0 AND H.APPROVAL_STATUS=1  ";

                if (liste.BRAND != 0)
                    sql += " AND H.BRAND=" + liste.BRAND;
                if (liste.ORDER_TYPE != 0)
                    sql += " AND H.ORDER_TYPE=" + liste.ORDER_TYPE;
                if (liste.FABRIC_TYPE != 0)
                    sql += " AND H.FABRIC_TYPE=" + liste.FABRIC_TYPE;
                else sql += " AND H.FABRIC_TYPE IN (SELECT FABRIC_TYPE_ID FROM FDEIT005.EPM_USER_FABRIC_TYPES WHERE USER_CODE='" + USER_CODE + "')";
                if (liste.PRODUCTION_TYPE != 0)
                    sql += " AND H.PRODUCTION_TYPE=" + liste.PRODUCTION_TYPE;
                else sql += " AND H.PRODUCTION_TYPE IN (SELECT PRODUCTION_TYPE_ID FROM FDEIT005.EPM_USER_PRODUCTION_TYPES WHERE USER_CODE='" + USER_CODE + "')";
                if (liste.COLOR != null && liste.COLOR != "")
                    sql += " AND H.COLOR='" + liste.COLOR + "'";
                if (liste.MODEL != null && liste.MODEL != "")
                    sql += " AND H.MODEL='" + liste.MODEL + "'";
                sql += " AND H.SEASON=" + liste.SEASON;

                if (liste.RECIPE != 0)
                    sql += " AND H.RECIPE=" + liste.RECIPE;

                sql += " ) A";

                return _trackRepository.DeserializeList<UretimTakipListesi>(sql);
            }
            else
            {
                string sql = @" SELECT A.*,
       '' PROCESS_INFO,
       '' TAKIP_NO
  FROM (SELECT H.*,
               (SELECT SUM (D.QUANTITY)
                  FROM FDEIT005.EPM_MASTER_PRODUCTION_D D
                 WHERE D.HEADER_ID = H.ID)
                  AS QUANTITY,
               SYSDATE  END_DATE,
               '' LAST_STATE,
               '' MUSTBE_STATE,
               0 AS TAMAMLANAN,
              0 TANIMLANAN,
                null KUMAS_START,
               0 AS SATIN_ALMA_BAGLANTI
          FROM FDEIT005.EPM_MASTER_PRODUCTION_H H
         WHERE     0 = 0 AND H.APPROVAL_STATUS=1 AND H.STATUS=0";
                if (liste.BRAND != 0)
                    sql += " AND H.BRAND=" + liste.BRAND;
                if (liste.ORDER_TYPE != 0)
                    sql += " AND H.ORDER_TYPE=" + liste.ORDER_TYPE;
                if (liste.FABRIC_TYPE != 0)
                    sql += " AND H.FABRIC_TYPE=" + liste.FABRIC_TYPE;
                else sql += " AND H.FABRIC_TYPE IN (SELECT FABRIC_TYPE_ID FROM FDEIT005.EPM_USER_FABRIC_TYPES WHERE USER_CODE='" + USER_CODE + "')";
                if (liste.PRODUCTION_TYPE != 0)
                    sql += " AND H.PRODUCTION_TYPE=" + liste.PRODUCTION_TYPE;
                else sql += " AND H.PRODUCTION_TYPE IN (SELECT PRODUCTION_TYPE_ID FROM FDEIT005.EPM_USER_PRODUCTION_TYPES WHERE USER_CODE='" + USER_CODE + "')";
                if (liste.COLOR != null && liste.COLOR != "")
                    sql += " AND H.COLOR='" + liste.COLOR + "'";
                if (liste.MODEL != null && liste.MODEL != "")
                    sql += " AND H.MODEL='" + liste.MODEL + "'";
                sql += " AND H.SEASON=" + liste.SEASON;

                if (liste.RECIPE != 0)
                    sql += " AND H.RECIPE=" + liste.RECIPE;

                sql += " ) A";
                return _trackRepository.DeserializeList<UretimTakipListesi>(sql);
            }
            

        }

        public List<EPM_MASTER_PRODUCTION_ORDERS> ProductionOrderList(int HEADER_ID)
        {
            string sql = "SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE HEADER_ID=" + HEADER_ID;
            return _trackRepository.DeserializeList<EPM_MASTER_PRODUCTION_ORDERS>(sql);
        }

        void UpdateOrders()
        {
            try
            {

                _trackRepository.ExecSql(@"UPDATE FDEIT005.EPM_MASTER_PRODUCTION_ORDERS O  SET O.PO_HEADER_ID =(SELECT PO_HEADER_ID FROM APPS.PO_HEADERS_ALL PH
               INNER JOIN APPS.PER_PEOPLE_F AG ON AG.PERSON_ID = PH.AGENT_ID  
         WHERE     AG.EMPLOYEE_NUMBER IS NOT NULL AND PH.ORG_ID=105
               AND TRUNC (SYSDATE) BETWEEN AG.EFFECTIVE_START_DATE
                                       AND AG.EFFECTIVE_END_DATE AND SEGMENT1 = O.TAKIP_NO AND ROWNUM=1)");
            }
            catch (Exception ex)
            {
            }
        }

        public object[] ProductionOrdersInsert(string Values)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                EPM_MASTER_PRODUCTION_ORDERS detail = new EPM_MASTER_PRODUCTION_ORDERS();
                JsonConvert.PopulateObject(Values, detail);
                detail.PO_HEADER_ID = 0;
                if (detail.TAKIP_NO != null && detail.TAKIP_NO != "")
                {
                    _trackRepository.Serialize<EPM_MASTER_PRODUCTION_ORDERS>(detail);
                    UpdateOrders();
                }
                else
                {
                    ok[0] = false;
                    ok[1] = "Takip No Boş Olamaz";
                }
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;

        }

        public object[] ProductionOrdersUpdate(int Key, string Values)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                EPM_MASTER_PRODUCTION_ORDERS detail = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_ORDERS>(@"SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS D  WHERE D.ID=" + Key);
                JsonConvert.PopulateObject(Values, detail);
                _trackRepository.Serialize<EPM_MASTER_PRODUCTION_ORDERS>(detail);
                UpdateOrders();
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public object[] ProductionOrdersDelete(int Key)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {

                _trackRepository.ExecSql("DELETE FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE ID=" + Key);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public List<TabPanel> GetTabList()
        {
            return new TabPanel().GetList();
        }

        public List<EPM_CONTRACT_WEB_USERS> GetContractList()
        {
            return _trackRepository.DeserializeList<EPM_CONTRACT_WEB_USERS>("SELECT * FROM FDEIT005.EPM_CONTRACT_WEB_USERS");
        }

        public List<ContractProcessList> GetContractProcessList( int HEADER_ID)
        { 
            EPM_MASTER_PRODUCTION_H master = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_H>(HEADER_ID);
            EPM_CONTRACT_TRACK_H header = _trackRepository.Deserialize<EPM_CONTRACT_TRACK_H>("SELECT * FROM FDEIT005.EPM_CONTRACT_TRACK_H WHERE HEADER_ID=" + HEADER_ID);
            List<EPM_CONTRACT_TRACK_L> detail = _trackRepository.DeserializeList<EPM_CONTRACT_TRACK_L>("SELECT * FROM FDEIT005.EPM_CONTRACT_TRACK_H WHERE HEADER_ID=" + header.ID);
            List<EPM_CONTRACT_PROCESS_INFO> processInfo = _trackRepository.DeserializeList<EPM_CONTRACT_PROCESS_INFO>("SELECT * FROM FDEIT005.EPM_CONTRACT_PROCESS_INFO ORDER BY ID");

            List<ContractProcessList> list = new List<ContractProcessList>();
            DateTime endDate = master.DEADLINE.DatetimeParse();
            foreach (var item in processInfo)
            {
                list.Add(new ContractProcessList()
                {
                    ID = 0,
                    PROCESS_ID = item.ID,
                    PROCESS_NAME = item.PROCESS_NAME,
                    PROCESS_TIME = item.PROCESS_TIME,
                    ISACTIVE = false
                });
            }



            foreach (var item in detail)
            {
                ContractProcessList p = list.Find(ob => ob.PROCESS_ID == item.PROCESS_ID);
                if (p != null)
                {
                    p.ISACTIVE = true;
                    p.ID = item.ID;
                    p.END_DATE = item.END_DATE;
                }
            }

            list = list.OrderByDescending(ob => ob.PROCESS_ID).ToList();
            foreach (var item in list)
            {
                item.END_DATE = endDate;
                endDate = endDate.AddDays(-item.PROCESS_TIME);
            }
            list = list.OrderBy(ob => ob.PROCESS_ID).ToList();
            return list;
        }

        public PRODUCTION_HEADER GetProductionList(int HEADER_ID)
        {
            object[] obj = { "",""};
            string sql = @"SELECT  DISTINCT ID AS ENTEGRATION_ID, 
   BRAND,
   SUB_BRAND,
   SEASON,
   MODEL,
   COLOR,
   PRODUCT_GROUP,
   FABRIC_TYPE,
   PRODUCTION_TYPE,
   RECIPE,
   DEADLINE,
   ORDER_TYPE,
   CREATE_DATE,  
   ATTRIBUTE1,
   ATTRIBUTE2,
   ATTRIBUTE3,
   ATTRIBUTE4,
   ATTRIBUTE5,
   ATTRIBUTE6,
   ATTRIBUTE7,
   ATTRIBUTE8,
   ATTRIBUTE9,
   ATTRIBUTE10,
   TEMA,
   ANA_TEMA,
   ROYALTY,
   COLLECTION_TYPE FROM FDEIT005.EPM_PRODUCTION_ORDER_V   WHERE ID=" + HEADER_ID;
            PRODUCTION_HEADER  header= _trackRepository.Deserialize<PRODUCTION_HEADER>(sql);


            header.DETAIL = _trackRepository.DeserializeList<PRODUCTION_DETAIL>(string.Format(@"SELECT  DETAIL_ID AS ENTEGRATION_ID, 
   ID AS ENTEGRATION_HEADER_ID,
   MARKET,  
   PRODUCT_SIZE,
   QUANTITY
   FROM FDEIT005.EPM_PRODUCTION_ORDER_V   WHERE ID={0}", HEADER_ID));

            return header;

        }


        #region PROCESS ANALİZLERİ

        public async Task PROCESSLERIANALIZET(CancellationToken stoppingToken)
        {

            List<EPM_MASTER_PRODUCTION_ORDERS> orders = _trackRepository.DeserializeList<EPM_MASTER_PRODUCTION_ORDERS>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS ORDER BY ID DESC");

            foreach (var item in orders)
            {
                try
                {
                    _logger.LogInformation($"Operasyon yapılıyor HeaderId={item.HEADER_ID}");
                    SETPROCESS(item);
                    await Task.Delay(1000, stoppingToken);
                }
                catch (Exception ex)
                { 
                }
            }
        }

        void SETPROCESS(EPM_MASTER_PRODUCTION_ORDERS order)
        {
            _trackRepository.ExecSql("DELETE FDEIT005.EPM_PRODUCTION_TRACKING_LIST WHERE PO_HEADER_ID=" + order.PO_HEADER_ID + " AND HEADER_ID=" + order.HEADER_ID + "");
            EPM_MASTER_PRODUCTION_H master = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_H>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_H WHERE ID=" + order.HEADER_ID);
            List<EPM_MASTER_PRODUCTION_D> detaiList = _trackRepository.DeserializeList<EPM_MASTER_PRODUCTION_D>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE HEADER_ID=" + master.ID);
            DataTable dtDetail = _trackRepository.QueryFill(DetailSQL() + " AND PO_HEADER_ID=" + order.PO_HEADER_ID + " ORDER BY DETAIL_TAKIP_NO");
            CREATELIST(order);
            foreach (DataRow rowDetail in dtDetail.Rows)
            {

                int DETAIL_ID = rowDetail["DETAIL_TAKIP_NO"].IntParse();
                string TAKIP_NO = rowDetail["TAKIP_NO"].ToString();
                int ITEM_ID = rowDetail["ITEM_ID"].IntParse();
                string RENKDETAY = rowDetail["RENKDETAY"].ToString();
                string ANAMODEL = master.MODEL;
                int HEADER_ID = master.ID;
                int URETIM_ADET = detaiList.Sum(ob => ob.QUANTITY).IntParse();
                int TEDARIK = rowDetail["TEDARIK"].IntParse();
                DateTime URETIMTARIHI = master.DEADLINE.DatetimeParse();
                string KesimFoyleri = "";

                List<ReceteProcessModel> m = _trackRepository.DeserializeList<ReceteProcessModel>(@"
                                SELECT RH.ADI,
                                     PI.PROCESS_NAME,
                                     PI.PROCESS_TIME, 
                                     RD.PROCESS_ID,
                                     RD.QUEUE,
                                     RH.ID AS RECETEHEADERID
                                FROM FDEIT005.EPM_PRODUCTION_RECIPE RH
                                     INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD ON RD.HEADER_ID = RH.ID
                                     INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI ON PI.ID = RD.PROCESS_ID
                            ORDER BY RECETEHEADERID, QUEUE").FindAll(ob => ob.RECETEHEADERID == master.RECIPE).OrderByDescending(ob => ob.QUEUE).ToList();
                List<EPM_PRODUCTION_TRACKING_LIST> uretimList = GETLISTMODEL(order.PO_HEADER_ID, DETAIL_ID, master.RECIPE, order.HEADER_ID);
                DataRow[] rows;
                if (uretimList.Count > 0)
                {

                    DataTable dtOrme = _egemenRepository.QueryFill(new EgemenOrmeHelper().ORMEALINANSIPARISSATIRTAKIP(TAKIP_NO.ToString(), DETAIL_ID.ToString()), FirebirdConnectionDB.ORME);
                    DataTable dtKumasDepo = _trackRepository.QueryFill(new KumasDepoHelper().KumasDepoHareketleri(ITEM_ID, order.PO_HEADER_ID));
                    DataTable dtKesimTasnif = _trackRepository.QueryFill(new KesimFoyuHelper().KesimFoyleri(ITEM_ID, order.PO_HEADER_ID, RENKDETAY, ANAMODEL));
                    DataTable dtBant = new DataTable();
                    DataTable dtKalite = new DataTable();
                    if (dtKesimTasnif.Rows.Count > 0)
                    {
                        KesimFoyleri = "";
                        List<string> kFoy = new DataView(dtKesimTasnif).ToTable(true, "WIP_ENTITY_NAME").AsEnumerable().Select(ob => ob.Field<string>("WIP_ENTITY_NAME")).ToList();
                        foreach (string s in kFoy)
                            KesimFoyleri += "'" + s + "',";
                        KesimFoyleri = KesimFoyleri.TrimEnd(',');
                        dtBant = _egemenRepository.QueryFill(new EgemenDevanlayHelper().BantBitisleriSQL(master.COLOR, KesimFoyleri), FirebirdConnectionDB.DEVANLAY);
                        dtKalite = _egemenRepository.QueryFill(new EgemenDevanlayHelper().KaliteBitisleriSQL(master.COLOR, KesimFoyleri), FirebirdConnectionDB.DEVANLAY);
                    }
                    else
                        KesimFoyleri = "";

                    for (int i = 0; i < uretimList.Count; i++)
                    {
                        EPM_PRODUCTION_TRACKING_LIST uretim = uretimList[i];
                        EPM_TRACKING_PROCESS_VALUES values = _trackRepository.Deserialize<EPM_TRACKING_PROCESS_VALUES>("SELECT * FROM FDEIT005.EPM_TRACKING_PROCESS_VALUES WHERE HEADER_ID=" + uretim.HEADER_ID);
                        SURECLER surec = (SURECLER)m.Find(ob => ob.PROCESS_ID == uretim.PROCESS_ID).PROCESS_ID;

                        int planlanan = dtKesimTasnif.Compute("SUM(PLANLANAN_KESIM)", string.Empty).IntParse();
                        int kesilen = dtKesimTasnif.Compute("SUM(FIILI_KESIM)", string.Empty).IntParse();
                        int tasnif = dtKesimTasnif.Compute("SUM(TASNIF_MIKTARI)", string.Empty).IntParse();
                        int bant = 0;
                        if (dtBant.Rows.Count > 0)
                            bant = dtBant.Compute("SUM(MIKTAR)", string.Empty).IntParse();
                        int kaliteMiktar = 0;
                        if (dtKalite.Rows.Count > 0)
                            kaliteMiktar = dtKalite.Compute("SUM(MIKTAR)", string.Empty).IntParse();
                        values.BANT = bant;
                        values.KALITE = kaliteMiktar;
                        values.KESIM = kesilen;
                        values.TASNIF = tasnif;
                        values.HEADER_ID = uretimList[i].HEADER_ID;
                        _trackRepository.Serialize(values);
                        switch (surec)
                        {
                            case SURECLER.IPLIK: //İPLİK
                                rows = dtOrme.Select("(HAREKET_ADI='Alış' AND URUN_TIPI_ADI='İplik') OR (HAREKET_ADI='Stok Kaydırma (Giriş)' AND URUN_TIPI_ADI='İplik')");
                                uretim.BEKLENEN_MIKTAR = TEDARIK;
                                if (rows != null && rows.Length > 0)
                                {
                                    decimal adetIplik = rows.CopyToDataTable<DataRow>().Compute("SUM(MIKTAR)", string.Empty).DecimalParse();
                                    if (adetIplik < TEDARIK)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        uretim.GERCEKLESEN_MIKTAR = adetIplik;
                                    }
                                    else
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        uretim.GERCEKLESEN_MIKTAR = adetIplik;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                _trackRepository.Serialize(uretim);
                                break;
                            case SURECLER.ORGU: //ÖRGÜ
                                rows = dtOrme.Select("(HAREKET_ADI='Alış' AND URUN_TIPI_ADI='Kumaş') OR (HAREKET_ADI='Stok Kaydırma (Giriş)' AND URUN_TIPI_ADI='Kumaş')");
                                uretim.BEKLENEN_MIKTAR = TEDARIK;
                                if (rows != null && rows.Length > 0)
                                {
                                    decimal adetKumas = rows.CopyToDataTable<DataRow>().Compute("SUM(MIKTAR)", string.Empty).DecimalParse();
                                    if (adetKumas < TEDARIK)
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumas;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumas;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                _trackRepository.Serialize(uretim);
                                break;
                            case SURECLER.KUMASBOYA://KUMAŞ BOYA
                                rows = dtOrme.Select("HAREKET_ADI='Fason Üretime Çıkış' AND FIRMAID='F005'");
                                uretim.BEKLENEN_MIKTAR = TEDARIK;
                                if (rows != null && rows.Length > 0)
                                {
                                    decimal adetKumasBoya = rows.CopyToDataTable<DataRow>().Compute("SUM(MIKTAR)", string.Empty).DecimalParse();
                                    if (adetKumasBoya < TEDARIK)
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumasBoya;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumasBoya;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                _trackRepository.Serialize(uretim);
                                break;
                            case SURECLER.KUMASDEPO://KUMAŞ DEPO
                                uretim.BEKLENEN_MIKTAR = TEDARIK;
                                if (dtKumasDepo.Rows.Count > 0)
                                {
                                    decimal adetKumasDepo = dtKumasDepo.Select("TRANSACTION_TYPE_ID=18").CopyToDataTable<DataRow>().Compute("SUM(ISLEM_MIKTARI)", string.Empty).DecimalParse();
                                    if (adetKumasDepo < TEDARIK)
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumasDepo;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                    {
                                        ONCEKILERITAMAMLA(uretimList, i);
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        uretim.GERCEKLESEN_MIKTAR = adetKumasDepo;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                _trackRepository.Serialize(uretim);
                                break;
                            case SURECLER.KESIM://KESİM 
                                uretim.BEKLENEN_MIKTAR = planlanan;
                                uretim.GERCEKLESEN_MIKTAR = kesilen;
                                if (planlanan > 0 & kesilen > 0)
                                {
                                    ONCEKILERITAMAMLA(uretimList, i);
                                    if (kesilen >= planlanan)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else if (kesilen < planlanan)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                _trackRepository.Serialize(uretim);

                                break;
                            case SURECLER.TASNIF://TASNİF
                                uretim.BEKLENEN_MIKTAR = kesilen;
                                uretim.GERCEKLESEN_MIKTAR = tasnif;
                                if (planlanan > 0 & tasnif > 0)
                                {
                                    ONCEKILERITAMAMLA(uretimList, i);
                                    if (tasnif >= kesilen && tasnif > 0)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else if (tasnif < kesilen)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                _trackRepository.Serialize(uretim);
                                break;
                            case SURECLER.BANT://BANT
                                uretim.BEKLENEN_MIKTAR = URETIM_ADET;
                                uretim.GERCEKLESEN_MIKTAR = bant;
                                if (dtBant.Rows.Count > 0)
                                {
                                    ONCEKILERITAMAMLA(uretimList, i);
                                    if (bant >= URETIM_ADET)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    else
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                _trackRepository.Serialize(uretim);
                                break;
                            case SURECLER.KALITE://KALİTE 
                                uretim.BEKLENEN_MIKTAR = URETIM_ADET;
                                uretim.GERCEKLESEN_MIKTAR = kaliteMiktar;
                                if (dtKalite.Rows.Count > 0)
                                {
                                    ONCEKILERITAMAMLA(uretimList, i);
                                    if (kaliteMiktar >= URETIM_ADET)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                    if (kaliteMiktar < URETIM_ADET)
                                    {
                                        uretim.STATUS = (int)SURECDURUMLARI.EKSIKTAMAMLANAN;
                                        SONRAKINEILERLET(uretimList, i);
                                    }
                                }
                                else
                                {
                                    if (uretim.STATUS != (int)SURECDURUMLARI.BASLADI && !ONCEKILERDEBASLAMISVARMI(uretimList, i))
                                        uretim.STATUS = (int)SURECDURUMLARI.VERIBULUNAMADI;
                                }
                                _trackRepository.Serialize(uretim);
                                break;
                            default: break;
                        }
                    }
                }
                else
                    _trackRepository.ExecSql("DELETE FDEIT005.EPM_PRODUCTION_TRACKING_LIST WHERE PO_HEADER_ID=" + order.PO_HEADER_ID + " AND HEADER_ID=" + order.HEADER_ID + "");
            }
        }

        List<EPM_PRODUCTION_TRACKING_LIST> GETLISTMODEL(int PO_HEADER_ID, int DETAIL_ID, int RECIPE, int HEADER_ID)
        {
            string sql = string.Format(@"
                           SELECT PL.*,RD.QUEUE
  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
       INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI ON PI.ID = PL.PROCESS_ID
       INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE RH ON RH.ID={2}
       INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD ON RD.HEADER_ID=RH.ID AND RD.PROCESS_ID=PI.ID
 WHERE PL.PO_HEADER_ID = {0} AND PL.DETAIL_ID = {1} AND PL.HEADER_ID={3} ORDER BY RD.QUEUE", PO_HEADER_ID, DETAIL_ID, RECIPE, HEADER_ID);
            return _trackRepository.DeserializeList<EPM_PRODUCTION_TRACKING_LIST>(sql);
        }

        bool ISTHERELIST(int PO_HEADER_ID, int DETAIL_ID, int HEADER_ID)
        {
            bool var = false;

            if (_trackRepository.ReadInteger(string.Format(@"SELECT Count(*) From FDEIT005.EPM_PRODUCTION_TRACKING_LIST WHERE PO_HEADER_ID={0} AND  DETAIL_ID={1} AND HEADER_ID={2}", PO_HEADER_ID, DETAIL_ID, HEADER_ID)) > 0)
                var = true;
            return var;
        }

        void CREATELIST(EPM_MASTER_PRODUCTION_ORDERS order)
        {
            DataTable dtDetail = _trackRepository.QueryFill(DetailSQL() + " AND PO_HEADER_ID=" + order.PO_HEADER_ID + " ORDER BY DETAIL_TAKIP_NO");
            EPM_MASTER_PRODUCTION_H master = _trackRepository.Deserialize<EPM_MASTER_PRODUCTION_H>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_H WHERE ID=" + order.HEADER_ID);
            List<EPM_MASTER_PRODUCTION_D> detaiList = _trackRepository.DeserializeList<EPM_MASTER_PRODUCTION_D>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE HEADER_ID=" + master.ID);
            List<EPM_PRODUCTION_TRACKING_LIST> uretimList = new List<EPM_PRODUCTION_TRACKING_LIST>();
            foreach (DataRow row in dtDetail.Rows)
            {
                string HEADER_ID = row["TAKIP_NO"].ToString();
                int DETAIL_ID = row["DETAIL_TAKIP_NO"].IntParse();
                int ADET = detaiList.Sum(ob => ob.QUANTITY);
                DateTime URETIMTARIHI = master.DEADLINE.DatetimeParse();

                if (ISTHERELIST(order.PO_HEADER_ID, DETAIL_ID, order.HEADER_ID)) return;
                List<ReceteProcessModel> m = _trackRepository.DeserializeList<ReceteProcessModel>(@"
                                SELECT RH.ADI,
                                     PI.PROCESS_NAME,
                                     PI.PROCESS_TIME, 
                                     RD.PROCESS_ID,
                                     RD.QUEUE,
                                     RH.ID AS RECETEHEADERID
                                FROM FDEIT005.EPM_PRODUCTION_RECIPE RH
                                     INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD ON RD.HEADER_ID = RH.ID
                                     INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI ON PI.ID = RD.PROCESS_ID
                            ORDER BY RECETEHEADERID, QUEUE").FindAll(ob => ob.RECETEHEADERID == master.RECIPE).OrderByDescending(ob => ob.QUEUE).ToList();
                int i = 0;
                foreach (ReceteProcessModel RECETE in m)
                {
                    i++;
                    EPM_PRODUCTION_TRACKING_LIST uret = new EPM_PRODUCTION_TRACKING_LIST();
                    uret.PO_HEADER_ID = order.PO_HEADER_ID;
                    uret.HEADER_ID = order.HEADER_ID;
                    uret.DETAIL_ID = DETAIL_ID;
                    if (i == m.Count) uret.STATUS = 1;
                    uret.PROCESS_ID = RECETE.PROCESS_ID;
                    switch (RECETE.QUEUE)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 7:
                        case 8:
                            uret.END_DATE = URETIMTARIHI;
                            URETIMTARIHI = URETIMTARIHI.AddBusinessDays(-RECETE.PROCESS_TIME);
                            uret.START_DATE = URETIMTARIHI;
                            break;
                        case 6:
                            uret.END_DATE = URETIMTARIHI;
                            URETIMTARIHI = URETIMTARIHI.AddBusinessDays(-Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(ADET) / Convert.ToDecimal(1000)))));
                            uret.START_DATE = URETIMTARIHI;
                            break;
                        default: break;
                    }
                    uretimList.Add(uret);
                }

            }
            if (uretimList.Count > 0)
                _trackRepository.BulkInsert(uretimList);
        }

        void ONCEKILERITAMAMLA(List<EPM_PRODUCTION_TRACKING_LIST> uretimList, int a)
        {
            for (int i = 0; i < a; i++)
            {
                EPM_PRODUCTION_TRACKING_LIST uretim = uretimList[i];
                uretim.STATUS = (int)SURECDURUMLARI.TAMAMLANDI;
                uretim = _trackRepository.Serialize(uretim);
            }
        }

        void SONRAKINEILERLET(List<EPM_PRODUCTION_TRACKING_LIST> uretimList, int i)
        {
            if (uretimList.Count - 1 >= i + 1)
            {
                uretimList[i + 1].STATUS = (int)SURECDURUMLARI.BASLADI;
                uretimList[i + 1] = _trackRepository.Serialize(uretimList[i + 1]);
            }
        }

        bool ONCEKILERDEBASLAMISVARMI(List<EPM_PRODUCTION_TRACKING_LIST> uretimList, int i)
        {
            bool evet = false;

            for (int a = 0; a < i; a++)
            {
                if (uretimList[a].STATUS == (int)SURECDURUMLARI.BASLADI)
                {
                    evet = true;
                    break;
                }
            }

            return evet;
        }

        public List<NOS_TRACK> GetNosTrack()
        {
            DateTime basTarih = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0).AddYears(-1).AddMonths(1);
            DateTime bitTarih = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0).AddYears(-1).AddMonths(2).AddSeconds(-1);
            string sql = string.Format(@"  
SELECT MA.MODEL,
           MA.RENK,
           MA.BEDEN,
           MA.MODEL||'.'||MA.RENK||'.'||MA.BEDEN AS SKU,
           MA.ADET AS MIN_ADET,
            NVL (NOTR.ENVANTER, 0)+  NVL (NOIHR.ENVANTER, 0) NOS_ADET, 
           NVL (NOTR.ENVANTER, 0) NOSTR_ADET, 
           NVL (NOIHR.ENVANTER, 0) NOSIHR_ADET, 
           NVL (LO.ENVANTER, 0) LOJ_ADET,
           STAY.SATIS_ADET_AY,
           cast((CASE WHEN ((NVL (NOTR.ENVANTER, 0)+  NVL (NOIHR.ENVANTER, 0)) / MA.ADET)*100.00 >100 THEN 100 ELSE ((NVL (NOTR.ENVANTER, 0)+  NVL (NOIHR.ENVANTER, 0)) / MA.ADET)*100.00 END) as integer) AS YUZDE,
           CASE
              WHEN MA.ADET - (NVL (NOTR.ENVANTER, 0)+  NVL (NOIHR.ENVANTER, 0)) >= 0
              THEN
                 MA.ADET - (NVL (NOTR.ENVANTER, 0)+  NVL (NOIHR.ENVANTER, 0))
              ELSE
                 0
           END
              AS SA_URETIM_ADET,
              0 PLANLANAN_URETIM_ADET
        FROM XXER_NOS_MIN_ADETLER MA
           LEFT JOIN
           (  SELECT msi.segment1 MODEL,
                     msi.segment2 RENK,
                     msi.segment3 BEDEN,
                     SUM (v.on_hand) AS ENVANTER
                FROM APPS.mtl_onhand_total_mwb_v v, APPS.mtl_system_items_b msi
               WHERE     1 = 1
                     AND v.organization_id = 1903
                     AND msi.inventory_item_id = v.inventory_item_id
                     AND msi.organization_id = 1903
                     AND v.subinventory_code IN ('CORLU')
            GROUP BY msi.segment1, msi.segment2, msi.segment3
              HAVING 1 = 1) LO
              ON LO.MODEL = MA.MODEL
                 AND LO.RENK = MA.RENK
                 AND LO.BEDEN = MA.BEDEN
LEFT JOIN (
                 SELECT  o3501345.DEPO as DEPO,o3501345.MIKTAR as ENVANTER,o3501345.MODEL ,o3501345.RENK ,o3501345.BEDEN 
FROM ( select 
msi.segment1 model,
msi.segment2 renk,
msi.segment3 beden, 
ws.SUBINVENTORY_CODE depo,
msi.ATTRIBUTE12,
sum(ws.TRANSACTION_QUANTITY) miktar,
(
select 
ml.segment1||'.'||
ml.segment2||'.'||
ml.segment3 
from
apps.mtl_item_locations ml
where ml.INVENTORY_LOCATION_ID=ws.LOCATOR_ID
and ml.ORGANIZATION_ID=ws.ORGANIZATION_ID)raf
from
apps.mtl_onhand_quantities_detail ws,
apps.mtl_system_items_b msi 
where 1=1 
and ws.ORGANIZATION_ID=105
and msi.INVENTORY_ITEM_ID=ws.INVENTORY_ITEM_ID
and msi.ORGANIZATION_ID=105   
and ws.SUBINVENTORY_CODE in ('NOSTR') 
group by  ws.SUBINVENTORY_CODE, 
msi.segment1,msi.segment2,msi.ATTRIBUTE12,
msi.segment3
) o3501345    ) NOTR ON NOTR.MODEL = MA.MODEL
                 AND NOTR.RENK = MA.RENK
                 AND NOTR.BEDEN = MA.BEDEN  
LEFT JOIN (
                 SELECT  o3501345.DEPO as DEPO,o3501345.MIKTAR as ENVANTER,o3501345.MODEL ,o3501345.RENK ,o3501345.BEDEN 
FROM ( select 
msi.segment1 model,
msi.segment2 renk,
msi.segment3 beden, 
ws.SUBINVENTORY_CODE depo,
msi.ATTRIBUTE12,
sum(ws.TRANSACTION_QUANTITY) miktar,
(
select 
ml.segment1||'.'||
ml.segment2||'.'||
ml.segment3 
from
apps.mtl_item_locations ml
where ml.INVENTORY_LOCATION_ID=ws.LOCATOR_ID
and ml.ORGANIZATION_ID=ws.ORGANIZATION_ID)raf
from
apps.mtl_onhand_quantities_detail ws,
apps.mtl_system_items_b msi 
where 1=1 
and ws.ORGANIZATION_ID=105
and msi.INVENTORY_ITEM_ID=ws.INVENTORY_ITEM_ID
and msi.ORGANIZATION_ID=105   
and ws.SUBINVENTORY_CODE in ('NOSIHR') 
group by  ws.SUBINVENTORY_CODE, 
msi.segment1,msi.segment2,msi.ATTRIBUTE12,
msi.segment3
) o3501345    ) NOIHR ON NOIHR.MODEL = MA.MODEL
                 AND NOIHR.RENK = MA.RENK
                 AND NOIHR.BEDEN = MA.BEDEN  
                                 LEFT JOIN
                (SELECT MODEL,
                          RENK,
                          BEDEN,
                          SUM (MIKTAR) AS SATIS_ADET_AY
                     FROM APPS.HERIT009_MAGAZA_SATIS_corlu_v
                    WHERE     DURUM<> 'IADE'
                             AND tarih BETWEEN {0}
                                           AND {1}
                    GROUP BY MODEL, RENK, BEDEN) STAY
                      ON STAY.MODEL = MA.MODEL
                         AND STAY.RENK = MA.RENK
                         AND STAY.BEDEN = MA.BEDEN
             ", _trackRepository.ToOracleTime(basTarih), _trackRepository.ToOracleTime(bitTarih));
            List<NOS_TRACK> analiz = _trackRepository.DeserializeList<NOS_TRACK>(sql);

            var tList = analiz.Select(ob => new { ob.MODEL, ob.RENK }).Distinct().ToList();

            foreach (var item in tList)
            {
                List<NOS_TRACK> tAnaliz = analiz.Where(ob => ob.MODEL == item.MODEL && ob.RENK == item.RENK).ToList();
                if (tAnaliz.Where(ob => ob.YUZDE <= 30).Count() > 0)
                {
                    foreach (var ana in tAnaliz)
                    {
                        ana.PLANLANAN_URETIM_ADET = ana.SA_URETIM_ADET;
                    }
                }
            }

            return analiz;
        }


        #endregion
    }
}
