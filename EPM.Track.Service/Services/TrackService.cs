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

namespace EPM.Track.Service.Services
{
    public class TrackService :ITrackService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IEgemenRepository _egemenRepository;
        public TrackService(ITrackRepository trackRepository, IEgemenRepository egemenRepository)
        {
            _trackRepository = trackRepository;
            _egemenRepository = egemenRepository;
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
         WHERE     0 = 0 ";

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
         WHERE     0 = 0";
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
            DateTime endDate = master.DEADLINE;
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
    }
}
