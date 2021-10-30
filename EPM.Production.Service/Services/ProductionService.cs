using Dapper.Oracle;
using EPM.Dto.Base;
using EPM.Dto.Loglar;
using EPM.Production.Dto.Extensions;
using EPM.Production.Dto.Production;
using EPM.Production.Repository.Repository;
using EPM.Tools.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Production.Service.Services
{
    public class ProductionService : IProductionService
    {

        private readonly IProductionRepository _productionRepository;
        private readonly ICacheService _cacheService;
        public ProductionService(IProductionRepository productionRepository, ICacheService cacheService)
        {
            _productionRepository = productionRepository;
            _cacheService = cacheService;
        }

        public List<UretimListesi> UretimListesi(string MODEL, string SEZON, string URETIM_TIPI, int DURUM)
        {
            MODEL = MODEL.InjectionString();
            SEZON = SEZON.InjectionString();
            URETIM_TIPI = URETIM_TIPI.InjectionString();
            List<UretimListesi> modelIcUretim = new List<UretimListesi>();
            List<UretimListesi> modelFason = new List<UretimListesi>();
            List<UretimListesi> returnList = new List<UretimListesi>();
            if (URETIM_TIPI == "İÇ ÜRETİM")
            {
                string sql = @"SELECT 
                                    TAKIP_NO
                                    ,MODEL
                                    ,'' RENK
                                    ,SEZON
                                    ,ADET
                                    ,TARIH
                                    ,RECETE
                                    ,AGENT_NAME OLUSTURAN
                                    ,'' FIRMA_ADI
                                    ,CREATION_DATE
                                    ,END_DATE
                                    ,PROCESSNAME
                                    ,OLMASIGEREKEN
                                    ,PROCESSINFO
                                     FROM (
                                    SELECT DISTINCT A.*
                                    ,CASE WHEN TANIMLANAN=0 THEN 'TAKIP BASLATILMADI' ELSE  CASE WHEN TAMAMLANAN =TANIMLANAN THEN 'TAMAMLANDI' ELSE CASE WHEN PROCESSNAME IS NULL THEN 'YOK' ELSE PROCESSNAME END END END AS PROCESSINFO 
                                    FROM (SELECT
                                    PH.SEGMENT1 AS TAKIP_NO 
                                    ,PH.COMMENTS AS MODEL
                                    ,PH.ATTRIBUTE1 AS SEZON
                                    ,CAST(PH.ATTRIBUTE11 AS NUMBER) AS ADET
                                    ,TO_DATE(PH.ATTRIBUTE12,'DD.MM.YYYY') AS TARIH
                                    ,(SELECT ADI FROM FDEIT005.EPM_PRODUCTION_RECIPE WHERE ID=PH.ATTRIBUTE9) RECETE
                                    ,CAST(PH.ATTRIBUTE9 AS NUMBER) AS RECETEID
                                    ,AG.FIRST_NAME ||' '||AG.LAST_NAME AS AGENT_NAME,PH.CREATION_DATE 
                                    ,(SELECT END_DATE FROM (SELECT PI.PROCESSNAME,
                                             PL.END_DATE,
                                             PL.HEADER_ID,
                                             RD.HEADER_ID AS RECETE_ID,
                                             (SELECT COUNT (*)
                                                FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                               WHERE STATUS = 2 AND PL2.HEADER_ID = PL.HEADER_ID)
                                                AS TAMAMLANAN,
                                             (SELECT COUNT (*)
                                                FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                               WHERE PL2.HEADER_ID = PL.HEADER_ID)
                                                AS TANIMLANAN
                                        FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
                                             INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI
                                                ON PI.ID = PL.PROCESS_ID
                                             INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD
                                                ON RD.PROCESS_ID = PL.PROCESS_ID
                                       WHERE PL.STATUS = 1
                                    ORDER BY PL.HEADER_ID, RD.QUEUE)DE WHERE DE.HEADER_ID = PH.SEGMENT1
                                         AND DE.RECETE_ID = CAST (PH.ATTRIBUTE9 AS NUMBER) AND ROWNUM=1) END_DATE, 
                                   (SELECT PROCESSNAME FROM (SELECT PI.PROCESSNAME,
                                             PL.END_DATE,
                                             PL.HEADER_ID,
                                             RD.HEADER_ID AS RECETE_ID,
                                             (SELECT COUNT (*)
                                                FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                               WHERE STATUS = 2 AND PL2.HEADER_ID = PL.HEADER_ID)
                                                AS TAMAMLANAN,
                                             (SELECT COUNT (*)
                                                FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                               WHERE PL2.HEADER_ID = PL.HEADER_ID)
                                                AS TANIMLANAN
                                        FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
                                             INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI
                                                ON PI.ID = PL.PROCESS_ID
                                             INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD
                                                ON RD.PROCESS_ID = PL.PROCESS_ID
                                       WHERE PL.STATUS = 1
                                    ORDER BY PL.HEADER_ID, RD.QUEUE)DE WHERE DE.HEADER_ID = PH.SEGMENT1
                                         AND DE.RECETE_ID = CAST (PH.ATTRIBUTE9 AS NUMBER) AND ROWNUM=1) PROCESSNAME  
                                         ,(SELECT FDEIT005.URETIM_FND_STATE(CAST (PH.SEGMENT1 AS NUMBER)) FROM DUAL) OLMASIGEREKEN
                                    ,(SELECT COUNT(*)  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2 WHERE STATUS IN (2,4) AND PL2.HEADER_ID=PH.SEGMENT1) AS TAMAMLANAN
                                    ,(SELECT COUNT(*)  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2 WHERE   PL2.HEADER_ID=PH.SEGMENT1) AS TANIMLANAN
                                    ,(SELECT MIN(START_DATE) FROM  FDEIT005.EPM_PRODUCTION_TRACKING_LIST WHERE PROCESS_ID=(SELECT ID FROM FDEIT005.EPM_PRODUCTION_PROCESS_INFO WHERE PROCESSTAG=4) AND HEADER_ID=CAST(PH.SEGMENT1 AS NUMBER)) KUMAS_START
                                    FROM APPS.PO_HEADERS_ALL PH 
                                    INNER JOIN APPS.PER_PEOPLE_F AG ON AG.PERSON_ID=PH.AGENT_ID  
                                    where   AG.EMPLOYEE_NUMBER IS NOT NULL  --AND  PH.ATTRIBUTE1 ='{0}'
                                    AND TRUNC (SYSDATE) BETWEEN AG.EFFECTIVE_START_DATE AND AG.EFFECTIVE_END_DATE
                                    AND PH.ATTRIBUTE1 IS NOT NULL
                                    AND PH.ATTRIBUTE11 IS NOT NULL
                                    AND PH.ATTRIBUTE9 IS NOT NULL
                                    AND PH.ATTRIBUTE12 IS NOT NULL ) A
                                    ) B WHERE 0=0";
                if (MODEL != "")
                    sql += " AND MODEL LIKE '" + MODEL + "%'";
                if (SEZON != "")
                    sql += " AND SEZON='" + SEZON + "'";
                if (DURUM == 1)
                    sql += " AND END_DATE<SYSDATE";
                else if (DURUM == 2)
                    sql += " AND END_DATE>=SYSDATE";
                modelIcUretim = _productionRepository.DeserializeList<UretimListesi>(sql);

                foreach (var item in modelIcUretim)
                {
                    try
                    {
                        if (item.MODEL.Contains('-'))
                        {
                            item.RENK = item.MODEL.ToString().Split('-')[1].ToString();
                            item.MODEL = item.MODEL.ToString().Split('-')[0].ToString();
                        }
                        else
                        {
                            item.RENK = item.MODEL.ToString().Split('.')[1].ToString();
                            item.MODEL = item.MODEL.ToString().Split('.')[0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }


                foreach (var item in modelIcUretim)
                    returnList.Add(item);

            }



            if (URETIM_TIPI == "FASON")
            {
                string sql = @"SELECT * FROM (SELECT H.ID AS TAKIP_NO,
        MODEL,
       RENK,
       SEZON,
       ADET,
       H.TAKE_DATE AS TARIH,
       'FASON' AS RECETE,
       US.DESCRIPTION AS OLUSTURAN,
       WF.ADI AS FIRMA_ADI, 
       CREATE_DATE AS CREATION_DATE, 
       (SELECT PROCESSNAME FROM FDEIT005.URETIM_FASON_PROCESSINFO WHERE ID = (SELECT PROCESS_ID FROM FDEIT005.URETIM_FASON_TAKIP_L WHERE HEADER_ID = H.ID AND STATUS IN(1, 2) AND ROWNUM = 1)) AS DEVAMEDEN,
        NVL((SELECT PLANNING_DATE FROM FDEIT005.URETIM_FASON_TAKIP_L WHERE HEADER_ID = H.ID AND STATUS IN(1, 2) AND ROWNUM = 1),SYSDATE) AS END_DATE,
       H.STATUS AS SIPARISDURUMU
  FROM FDEIT005.URETIM_FASON_TAKIP_H H
       INNER JOIN apps.fnd_user US ON US.USER_ID = H.CREATED_BY
       INNER JOIN FDEIT005.URETIM_FASON_WEB_FIRMA WF ON WF.ID = H.FIRMA_ID ";
                if (MODEL != "")
                    sql += " AND MODEL='" + MODEL + "'";
                if (SEZON != "")
                    sql += " AND SEZON='" + SEZON + "'";
                sql += ") A WHERE 0=0";

                if (DURUM == 1)
                    sql += " AND END_DATE<SYSDATE";
                else if (DURUM == 2)
                    sql += " AND END_DATE>=SYSDATE";

                modelFason = _productionRepository.DeserializeList<UretimListesi>(sql);
                foreach (var item in modelFason)
                    returnList.Add(item);
            }

            return returnList;
        } 

        public Tuple<UretimOnayliListe, List<UretimListesiAktarim>> UretimOnayliListe(Tuple<UretimOnayliListe, List<UretimListesiAktarim>> model)
        {
            return new Tuple<UretimOnayliListe, List<UretimListesiAktarim>>(new UretimOnayliListe(), new List<UretimListesiAktarim>());
        }

        public List<EPM_PRODUCTION_BRANDS> GetBrandList(string USER_CODE, List<EPM_USER_BRANDS> userBrands, bool hepsi = true)
        {
            List<EPM_PRODUCTION_BRANDS> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_BRANDS>>(0, "EPM_PRODUCTION_BRANDS");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_BRANDS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_BRANDS");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_BRANDS", list, TimeSpan.FromDays(4));
            }
            list = list.Where(ob => userBrands.Select(ob => ob.BRAND_ID).ToArray().Contains(ob.ID)).ToList();
            if (hepsi)
                list.Insert(0, new EPM_PRODUCTION_BRANDS() { ID = 0, ADI = "HEPSİ" }); 
             
            return list;
        }

        public List<EPM_PRODUCTION_SUB_BRANDS> GetSubBrandList(string USER_CODE, bool hepsi = true)
        {
            List<EPM_PRODUCTION_SUB_BRANDS> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_SUB_BRANDS>>(0, "EPM_PRODUCTION_SUB_BRANDS");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_SUB_BRANDS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SUB_BRANDS");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_SUB_BRANDS", list, TimeSpan.FromDays(4));
            }
            if (hepsi)
                list.Insert(0, new EPM_PRODUCTION_SUB_BRANDS() { ID = 0, ADI = "HEPSİ" }); 

            return list;
        }

        public List<EPM_PRODUCTION_SEASON> GetSeasonList()
        {
            List<EPM_PRODUCTION_SEASON> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_SEASON>>(0, "EPM_PRODUCTION_SEASON");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_SEASON>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SEASON");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_SEASON", list, TimeSpan.FromDays(4));
            } 
            return list; 
        }

        public List<EPM_PRODUCTION_MARKET> GetMarketList(bool hepsi = true)
        {
            List<EPM_PRODUCTION_MARKET> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_MARKET>>(0, "EPM_PRODUCTION_MARKET");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_MARKET>("SELECT * FROM FDEIT005.EPM_PRODUCTION_MARKET");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_MARKET", list, TimeSpan.FromDays(1));
            }

            if (hepsi)
                list.Insert(0, new EPM_PRODUCTION_MARKET() { ID = 0, ADI = "HEPSİ" }); 
            return list;
        }

        public List<EPM_PRODUCTION_ORDER_TYPES> GetOrderList(bool hepsi = true)
        {
            List<EPM_PRODUCTION_ORDER_TYPES> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_ORDER_TYPES>>(0, "EPM_PRODUCTION_ORDER_TYPES");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_ORDER_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_ORDER_TYPES");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_ORDER_TYPES", list, TimeSpan.FromDays(1));
            }

            if (hepsi)
                list.Insert(0, new EPM_PRODUCTION_ORDER_TYPES() { ID = 0, ADI = "HEPSİ" }); 
            return list; 
        }

        public List<EPM_PRODUCTION_FABRIC_TYPES> GetFabricTypes(string USER_CODE,List<EPM_USER_FABRIC_TYPES> userFabricTypes, bool hepsi = true)
        {
            List<EPM_PRODUCTION_FABRIC_TYPES> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_FABRIC_TYPES>>(0, "EPM_PRODUCTION_FABRIC_TYPES");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_FABRIC_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_FABRIC_TYPES");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_FABRIC_TYPES", list, TimeSpan.FromDays(1));
            }

            list = list.Where(ob => userFabricTypes.Select(ob => ob.FABRIC_TYPE_ID).ToArray().Contains(ob.ID)).ToList();
            if (hepsi)
                list.Insert(0, new EPM_PRODUCTION_FABRIC_TYPES() { ID = 0, ADI = "HEPSİ" }); 
            return list; 
        }

        public List<EPM_PRODUCTION_TYPES> GetProductionTypes(string USER_CODE, List<EPM_USER_PRODUCTION_TYPES> userProductionTypes, bool hepsi = true)
        {
            List<EPM_PRODUCTION_TYPES> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_TYPES>>(0, "EPM_PRODUCTION_TYPES");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_TYPES");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_TYPES", list, TimeSpan.FromDays(1));
            }

            list = list.Where(ob => userProductionTypes.Select(ob => ob.PRODUCTION_TYPE_ID).ToArray().Contains(ob.ID)).ToList();
            if (hepsi)
                list.Insert(0, new EPM_PRODUCTION_TYPES() { ID = 0, ADI = "HEPSİ" }); 
            return list;

             
        }

        public List<EPM_PRODUCT_COLLECTION_TYPES> GetCollectionTypes(bool hepsi = true)
        {
            List<EPM_PRODUCT_COLLECTION_TYPES> list;
            list = _cacheService.Get<List<EPM_PRODUCT_COLLECTION_TYPES>>(0, "EPM_PRODUCT_COLLECTION_TYPES");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCT_COLLECTION_TYPES>("SELECT * FROM FDEIT005.EPM_PRODUCT_COLLECTION_TYPES");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCT_COLLECTION_TYPES", list, TimeSpan.FromDays(1));
            }

            if (hepsi)
                list.Insert(0, new EPM_PRODUCT_COLLECTION_TYPES() { ID = 0, ADI = "HEPSİ" }); 
            return list; 
        }

        public List<EPM_PRODUCTION_BAND_GROUP> GetBandList(bool hepsi = true)
        {
            List<EPM_PRODUCTION_BAND_GROUP> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_BAND_GROUP>>(0, "EPM_PRODUCTION_BAND_GROUP");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_BAND_GROUP>("SELECT * FROM FDEIT005.EPM_PRODUCTION_BAND_GROUP");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_BAND_GROUP", list, TimeSpan.FromDays(1));
            }

            if (hepsi)
                list.Insert(0, new EPM_PRODUCTION_BAND_GROUP() { ID = 0, ADI = "HEPSİ" }); 
            return list;
        }
       
        public List<EPM_PRODUCT_GROUP> GetProductGroupList(bool hepsi = true)
        {
            List<EPM_PRODUCT_GROUP> list; 
            list = _cacheService.Get<List<EPM_PRODUCT_GROUP>>(0, "EPM_PRODUCT_GROUP");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCT_GROUP>("SELECT * FROM FDEIT005.EPM_PRODUCT_GROUP");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCT_GROUP", list, TimeSpan.FromDays(1));
            }

            if (hepsi)
                list.Insert(0, new EPM_PRODUCT_GROUP() { ID = 0, ADI = "HEPSİ" }); 
            return list;
        }

        public List<EPM_PRODUCTION_RECIPE> GetRecipeList(bool hepsi = true)
        {
            List<EPM_PRODUCTION_RECIPE> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_RECIPE>>(0, "EPM_PRODUCTION_RECIPE");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_RECIPE>("SELECT * FROM FDEIT005.EPM_PRODUCTION_RECIPE");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_RECIPE", list, TimeSpan.FromDays(1));
            }
            if (hepsi)
                list.Insert(0, new EPM_PRODUCTION_RECIPE() { ID = 0, ADI = "HEPSİ" }); 
            return list;
        }

        public List<EPM_PRODUCTION_RECIPE> GetRecipeListByType(bool hepsi = true,int TYPE=1)
        {
            List<EPM_PRODUCTION_RECIPE> list;
            list = _cacheService.Get<List<EPM_PRODUCTION_RECIPE>>(0, "EPM_PRODUCTION_RECIPE");
            if (list == null)
            {
                list = _productionRepository.DeserializeList<EPM_PRODUCTION_RECIPE>("SELECT * FROM FDEIT005.EPM_PRODUCTION_RECIPE");
                _cacheService.AddWithLifeTime(0, "EPM_PRODUCTION_RECIPE", list, TimeSpan.FromDays(1));
            }

            if (hepsi)
                list.Insert(0, new EPM_PRODUCTION_RECIPE() { ID = 0, ADI = "HEPSİ" });

            if (TYPE == 1)
                list = list.FindAll(ob => ob.ID == 1 || ob.ID == 2);
            else
                list = list.FindAll(ob => ob.ID == 3 || ob.ID == 4 || ob.ID == 5); 
            return list;
        }

        public List<ENUMMODEL> GetApprovalStatueList()
        {
            List<ENUMMODEL> enums = ((APPROVAL_STATUSES[])Enum.GetValues(typeof(APPROVAL_STATUSES))).Select(c => new ENUMMODEL() { ID = (int)c, ADI = c.ToString() }).ToList();
            return enums;
        }

        public List<MasterList> OnayliUretimListesi(string USER_CODE, UretimOnayliListe liste)
        {  
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_BRAND", liste.BRAND, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_SEASON", liste.SEASON, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_MODEL", liste.MODEL.ToStringParse(), OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_COLOR", liste.COLOR.ToStringParse(), OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_PRODUCT_GROUP", liste.PRODUCT_GROUP, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_FABRIC_TYPE", liste.FABRIC_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_PRODUCTION_TYPE", liste.PRODUCTION_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECIPE", liste.RECIPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_ORDER_TYPE", liste.ORDER_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_APPROVAL_STATUS", liste.APPROVAL_STATUS, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_BAND_ID", liste.BAND_ID, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_USER_CODE", USER_CODE, OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_STATUS", liste.STATUS, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            List<MasterList> productionTypes = _productionRepository.DeserializeListPROC<MasterList>("FDEIT005.GET_EPM_PRODUCTION_MASTER", dynamicParameters);
            return productionTypes; 

        } 

        public List<VerticalList> UretimListesiDikey(string USER_CODE, UretimOnayliListe liste)
        { 


            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_BRAND", liste.BRAND, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_SEASON", liste.SEASON, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_MODEL", liste.MODEL.ToStringParse(), OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_COLOR", liste.COLOR.ToStringParse(), OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_PRODUCT_GROUP", liste.PRODUCT_GROUP, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_FABRIC_TYPE", liste.FABRIC_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_PRODUCTION_TYPE", liste.PRODUCTION_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECIPE", liste.RECIPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_ORDER_TYPE", liste.ORDER_TYPE, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_APPROVAL_STATUS", liste.APPROVAL_STATUS, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_BAND_ID", liste.BAND_ID, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_USER_CODE", USER_CODE, OracleMappingType.Varchar2, ParameterDirection.Input);
            dynamicParameters.Add(":P_STATUS", liste.STATUS, OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            List<VerticalList> productionTypes = _productionRepository.DeserializeListPROC<VerticalList>("FDEIT005.GET_EPM_PRODUCTION_VERTICAL_M", dynamicParameters);
            return productionTypes; 
        }

        public List<DetailList> OnayliUretimListesiDetail(int ID)
        {
            return _productionRepository.DeserializeList<DetailList>(@" SELECT D.*
,H.EXPECTED_COST 
,H.CURRENCY_UNIT 
,0 AS REALESED_COST
FROM FDEIT005.EPM_MASTER_PRODUCTION_D D  
INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON H.ID=D.HEADER_ID 
WHERE HEADER_ID=" + ID);
        }

        public object[] UretimOnayliDetailUpdate(string USER_CODE, int Key, string Values, ILogService logRepository)
        {
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(Values);
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                LOG_EPM_MASTER_PRODUCTION_D log = new LOG_EPM_MASTER_PRODUCTION_D();
                EPM_MASTER_PRODUCTION_D detail = _productionRepository.Deserialize<EPM_MASTER_PRODUCTION_D>(@"SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_D D  WHERE D.ID=" + Key);
                log.OLD_VALUE = ProductionExtensions.GetPropValue(detail, dic.Keys.ToList()[0].ToString());
                JsonConvert.PopulateObject(Values, detail);
                log.NEW_VALUE = ProductionExtensions.GetPropValue(detail, dic.Keys.ToList()[0].ToString());
                _productionRepository.Serialize<EPM_MASTER_PRODUCTION_D>(detail);
                //detail.SaveLog(context);
                log.CHANGED_COLUMN = dic.Keys.ToList()[0].ToString();
                log.CREATE_DATE = DateTime.Now;
                log.CHANGED_BY = USER_CODE;
                log.DETAIL_ID = detail.ID;
                logRepository.SaveDetail(log);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public object[] UretimOnayliDetailDelete(string USER_CODE, int Key, ILogService logRepository)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                LOG_EPM_MASTER_PRODUCTION_D log = new LOG_EPM_MASTER_PRODUCTION_D();
                log.OLD_VALUE = 0;
                log.NEW_VALUE = 1;
                log.CHANGED_COLUMN = "STATUS";
                log.CREATE_DATE = DateTime.Now;
                log.CHANGED_BY = USER_CODE;
                log.DETAIL_ID = Key; 
                logRepository.SaveDetail(log);
                EPM_MASTER_PRODUCTION_D detail = _productionRepository.Deserialize<EPM_MASTER_PRODUCTION_D>(@"SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_D D  WHERE D.ID=" + Key);

                _productionRepository.ExecSql("UPDATE FDEIT005.EPM_MASTER_PRODUCTION_D SET STATUS=1 WHERE ID=" + detail.ID);
                _productionRepository.ExecSql("DELETE FROM FDEIT005.EPM_PRODUCTION_PLAN WHERE HEADER_ID=" + detail.HEADER_ID + " AND MARKET_ID=" + detail.MARKET);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public EPM_MASTER_PRODUCTION_D UretimOnayliDetailInsert(string Values)
        {
            EPM_MASTER_PRODUCTION_D detail = new EPM_MASTER_PRODUCTION_D();
            JsonConvert.PopulateObject(Values, detail);
            _productionRepository.Serialize<EPM_MASTER_PRODUCTION_D>(detail);
            return detail;

        }

        public object[] UretimOnayliUpdate(string USER_CODE, int Key, string Values,ILogService logRepository)
        {
            Dictionary<string, object> dic= JsonConvert.DeserializeObject<Dictionary<string, object>>(Values);
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                LOG_EPM_MASTER_PRODUCTION_H log = new LOG_EPM_MASTER_PRODUCTION_H();
                EPM_MASTER_PRODUCTION_H detail = _productionRepository.Deserialize<EPM_MASTER_PRODUCTION_H>(@"SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_H D  WHERE D.ID=" + Key);
                log.OLD_VALUE = ProductionExtensions.GetPropValue(detail, dic.Keys.ToList()[0].ToString());
                JsonConvert.PopulateObject(Values, detail);
                log.NEW_VALUE = ProductionExtensions.GetPropValue(detail, dic.Keys.ToList()[0].ToString());
                _productionRepository.Serialize<EPM_MASTER_PRODUCTION_H>(detail); 
                log.CHANGED_COLUMN = dic.Keys.ToList()[0].ToString();
                log.CREATE_DATE = DateTime.Now;
                log.CHANGED_BY = USER_CODE;
                log.HEADER_ID = detail.ID;
                logRepository.SaveMaster(log);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public object[] UretimOnayliDelete(string USER_CODE,int Key, ILogService logRepository)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                LOG_EPM_MASTER_PRODUCTION_H log = new LOG_EPM_MASTER_PRODUCTION_H();
                log.OLD_VALUE = 0;
                log.NEW_VALUE = 1;
                log.CHANGED_COLUMN = "STATUS";
                log.CREATE_DATE = DateTime.Now;
                log.CHANGED_BY = USER_CODE;
                log.HEADER_ID = Key;
                logRepository.SaveMaster(log);

                _productionRepository.ExecSql("UPDATE FDEIT005.EPM_MASTER_PRODUCTION_H SET STATUS=1 WHERE ID=" + Key);
                _productionRepository.ExecSql("UPDATE FDEIT005.EPM_MASTER_PRODUCTION_D SET STATUS=1 WHERE HEADER_ID=" + Key);
                _productionRepository.ExecSql("DELETE FROM FDEIT005.EPM_PRODUCTION_PLAN WHERE HEADER_ID=" + Key);
                _productionRepository.ExecSql("DELETE FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS WHERE HEADER_ID=" + Key);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public EPM_MASTER_PRODUCTION_H UretimOnayliInsert(string Values)
        {
            EPM_MASTER_PRODUCTION_H detail = new EPM_MASTER_PRODUCTION_H();
            JsonConvert.PopulateObject(Values, detail);
            _productionRepository.Serialize<EPM_MASTER_PRODUCTION_H>(detail);
            return detail;

        }

        public List<EPM_PRODUCTION_CURRENCY_UNITS> GetCurrencyUnits()
        { 
            return _productionRepository.DeserializeList<EPM_PRODUCTION_CURRENCY_UNITS>("SELECT ID,CURRENCY_UNIT FROM FDEIT005.EPM_PRODUCTION_CURRENCY_UNITS");
        }

        public List<LOG_EPM_MASTER_PRODUCTION_H> OnayliUretimListesiLog(int HEADER_ID, ILogService logRepository)
        {
            return logRepository.GetMaster().ToList().FindAll(ob=>ob.HEADER_ID==HEADER_ID);
        }

        public List<LOG_EPM_MASTER_PRODUCTION_D> OnayliUretimListesiLogDetail(int DETAIL_ID, ILogService logRepository)
        {
            return logRepository.GetDetail().ToList().FindAll(ob => ob.DETAIL_ID == DETAIL_ID);
        }

        public List<EPM_PRODUCTION_FLAGS> GetFlagList(bool hepsi)
        {
            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(":P_ALL", Convert.ToInt32(hepsi), OracleMappingType.Int32, ParameterDirection.Input);
            dynamicParameters.Add(":P_RECORDSET", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            List<EPM_PRODUCTION_FLAGS> list = _productionRepository.DeserializeListPROC<EPM_PRODUCTION_FLAGS>("FDEIT005.GET_EPM_PRODUCTION_FLAGS", dynamicParameters);
            return list;
        }
    }
}
