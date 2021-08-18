using EPM.Fason.Dto.Extensions;
using EPM.Fason.Dto.Fason;
using EPM.Fason.Repository.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static EPM.Fason.Dto.Extensions.Enums;

namespace EPM.Fason.Service.Services
{
    public class InspectionService : IInspectionService
    {
        private readonly IFasonRepository _fasonRepository;
        private readonly IFasonService _fasonService;
        public InspectionService(IFasonRepository fasonRepository,IFasonService fasonService)
        {
            _fasonRepository = fasonRepository;
            _fasonService = fasonService;
        }

        public List<PRODUCTION_FASON_USERS> GetFasonUsers(bool hepsi)
        {
            string sql = "";
            if (hepsi)
                sql = "SELECT 0 ID,'HEPSİ' NAME,'' EMAIL,'' PASSWORD UNION ALL SELECT * FROM PRODUCTION_FASON_USERS";
            else sql = "SELECT * FROM PRODUCTION_FASON_USERS";
            return _fasonRepository.DeserializeList<PRODUCTION_FASON_USERS>(sql);
        }

        public List<PRODUCTION_FASON_MENU> GetMenuList()
        {
            List<PRODUCTION_FASON_MENU> menu = new List<PRODUCTION_FASON_MENU>();
            menu.Add(new PRODUCTION_FASON_MENU()
            {
                ID = 1,
                ISEXPANDED = true,
                ICON = "product",
                TEXT = "INSPECTION YÖNETİMİ",
                ISVISIBLE = true,
                SELECTED = true
            });
            menu.Add(new PRODUCTION_FASON_MENU()
            {
                ID = 2,
                CATEGORY_ID = 1,
                CONTROLLER = "Inspection",
                TEXT = "SİPARİŞ LİSTESİ",
                ICON = "spinright",
                ACTION = "SiparisListesi",
                ISVISIBLE = true,
                SELECTED = true,
                ISEXPANDED = true,
                LOW_TEXT = "IL"

            });
            menu.Add(new PRODUCTION_FASON_MENU()
            {
                ID = 3,
                CATEGORY_ID = 1,
                CONTROLLER = "Inspection",
                TEXT = "INSPECTION LİSTESİ",
                ICON = "spinright",
                ACTION = "InspectionListesi",
                ISVISIBLE = true,
                SELECTED = false,
                ISEXPANDED = true,
                LOW_TEXT = "SL"

            });
            return menu;
        } 

        public IEnumerable<SIPARIS_LISTESI_DETAIL> GetSiparisDetailList(int ENTEGRASYON_ID)
        {
            string sql = "SELECT PRODUCT_SIZE,SUM(QUANTITY)QUANTITY FROM PRODUCTION_DETAIL WHERE 0=0 AND  ENTEGRATION_HEADER_ID=" + ENTEGRASYON_ID+ "  GROUP BY PRODUCT_SIZE order by PRODUCT_SIZE";
            return _fasonRepository.DeserializeList<SIPARIS_LISTESI_DETAIL>(sql);
        }

        public IEnumerable<SIPARIS_LISTESI> GetSiparisList(INSPECTION_FILTER liste)
        {
            string sql = "SELECT * FROM  PRODUCTION_LIST_V WHERE 0=0";
            if (liste.FIRMA_ID != 0)
                sql += " AND FIRMA_ID=" + liste.FIRMA_ID;
            if (liste.SEASON != "HEPSİ")
                sql += " AND SEASON='" + liste.SEASON + "'";
            if (liste.MODEL != null && liste.MODEL != null) sql += string.Format(" AND MODEL='{0}'", liste.MODEL);
            if (liste.COLOR != null && liste.COLOR != null) sql += string.Format(" AND COLOR='{0}'", liste.COLOR);

            return _fasonRepository.DeserializeList<SIPARIS_LISTESI>(sql);
        }

        public List<SEASONMODEL> GetSeasonList(bool hepsi)
        {
            string sql = "SELECT DISTINCT SEASON AS ADI FROM PRODUCTION_HEADER";
            List<SEASONMODEL> list= _fasonRepository.DeserializeList<SEASONMODEL>(sql);
            if (hepsi)
                list.Insert(0, new SEASONMODEL() { ADI = "HEPSİ" });
            return list;
        }

        public Tuple<PRODUCTION_AQL_HEADER,DataTable, List<PRODUCTION_AQL_QUANTITYS>, AQL_ANALIZER> GetAQL(int USER_ID,int ENTEGRATION_HEADER_ID, int TYPE)
        {

            
            IEnumerable<SIPARIS_LISTESI_DETAIL> detail = GetSiparisDetailList(ENTEGRATION_HEADER_ID);
            IEnumerable<PRODUCTION_AQL_QUESTIONS> questions = _fasonRepository.DeserializeList<PRODUCTION_AQL_QUESTIONS>("SELECT * FROM PRODUCTION_AQL_QUESTIONS");
            string mjBottom = "", mjBottomSecond = "", mnBottom = "", mnBottomSecond = "",mjSum="",mnSum="";
            PRODUCTION_AQL_HEADER header = _fasonRepository.Deserialize<PRODUCTION_AQL_HEADER>("SELECT * FROM PRODUCTION_AQL_HEADER WHERE ENTEGRATION_HEADER_ID=" + ENTEGRATION_HEADER_ID + " AND TYPE=" + TYPE + "");
            header.PRODUCTION_HEADER = _fasonRepository.Deserialize<PRODUCTION_HEADER>("SELECT * FROM PRODUCTION_HEADER WHERE ENTEGRATION_ID=" + ENTEGRATION_HEADER_ID);
            header.FIRMA_ADI = _fasonRepository.ReadString("SELECT NAME FROM PRODUCTION_FASON_USERS WHERE ID =(SELECT FIRMA_ID FROM PRODUCTION_LIST_H WHERE ENTEGRATION_ID=" + ENTEGRATION_HEADER_ID + ")");
            var AQL_ANALIZER = new AQL_ANALIZER().GetAQLPointsByOrder(detail.Sum(ob => ob.QUANTITY));
            if (header.ID==0)
            {
                header.ID = 0;
                header.ENTEGRATION_HEADER_ID = ENTEGRATION_HEADER_ID;
                header.DESCRIPTION = "";
                header.TYPE = TYPE;
                header.END_DATE = DateTime.Now;
                header.START_DATE = DateTime.Now;
                header.STATUS = (int)AQLHEADERSTATUS.FIRST_INIT;
                header.USER_ID = USER_ID;
                header = _fasonRepository.Serialize(header);
                header.PRODUCTION_HEADER = _fasonRepository.Deserialize<PRODUCTION_HEADER>("SELECT * FROM PRODUCTION_HEADER WHERE ENTEGRATION_ID=" + ENTEGRATION_HEADER_ID);
                var sampleCount = AQL_ANALIZER.SAMPLE_QUANTITY;
                int sampleBySKU = Convert.ToInt32((sampleCount / detail.Count()));
                var i = 0;
                foreach (var item in detail)
                {
                    i++;
                    PRODUCTION_AQL_QUANTITYS quantity = new PRODUCTION_AQL_QUANTITYS();
                    quantity.PRODUCT_SIZE = item.PRODUCT_SIZE;
                    quantity.HEADER_ID = header.ID;
                    quantity.QUANTITY = item.QUANTITY;
                    if (sampleCount >= 0)
                    {
                        quantity.QUANTITY_SAMPLE = sampleBySKU;
                        sampleCount = sampleCount- sampleBySKU;
                        if (sampleCount < 0)
                        {
                            sampleBySKU = sampleBySKU + sampleCount;
                            sampleCount = 0;
                        }
                    }
                    if (i == detail.Count() && sampleCount > 0)
                        quantity.QUANTITY_SAMPLE += sampleCount;
                    _fasonRepository.Serialize(quantity);
                    mjBottom += "[MJ_" + item.PRODUCT_SIZE+"],";
                    mjBottomSecond += "0 AS [MJ_" + item.PRODUCT_SIZE+"],";
                    mnBottom += "[MN_" + item.PRODUCT_SIZE+"],";
                    mnBottomSecond += "0 AS [MN_" + item.PRODUCT_SIZE+"],";
                    mjSum += "SUM([MJ_" + item.PRODUCT_SIZE + "]) AS [MJ_" + item.PRODUCT_SIZE + "] ,";
                    mnSum += "SUM([MN_" + item.PRODUCT_SIZE + "]) AS [MN_" + item.PRODUCT_SIZE + "] ,";
                    foreach (var ques in questions)
                    {

                        PRODUCTION_AQL_LINE line = new PRODUCTION_AQL_LINE();
                        line.BEDEN = item.PRODUCT_SIZE;
                        line.MAJOR_QUANTITY = 0;
                        line.MINOR_QUANTITY = 0;
                        line.QUESTION_ID = ques.ID;
                        line.TOTAL_QUANTITY = item.QUANTITY;
                        line.TOTAL_QUANTITY_RELEASE = 0;
                        line.HEADER_ID = header.ID;
                        _fasonRepository.Serialize(line);
                    }
                }

            }
            else
            {
                foreach (var item in detail)
                {
                    mjBottom += "[MJ_" + item.PRODUCT_SIZE + "],";
                    mjBottomSecond += "0 AS [MJ_" + item.PRODUCT_SIZE + "],";
                    mnBottom += "[MN_" + item.PRODUCT_SIZE + "],";
                    mnBottomSecond += "0 AS [MN_" + item.PRODUCT_SIZE + "],";
                    mjSum += "SUM([MJ_" + item.PRODUCT_SIZE + "]) AS [MJ_" + item.PRODUCT_SIZE + "] ,";
                    mnSum += "SUM([MN_" + item.PRODUCT_SIZE + "]) AS [MN_" + item.PRODUCT_SIZE + "] ,"; 
                }

            }
            mjBottom = mjBottom.TrimEnd(',');
            mnBottom = mnBottom.TrimEnd(',');
            mjBottomSecond = mjBottomSecond.TrimEnd(',');
            mnBottomSecond = mnBottomSecond.TrimEnd(',');
            mjSum = mjSum.TrimEnd(',');
            mnSum = mnSum.TrimEnd(',');

            string sql = string.Format(@"
SELECT QUESTION,QUESTION_ID,{5},{6},{7} AS HEADER_ID FROM (
SELECT QUESTION,QUESTION_ID,{1},{2} FROM (
SELECT 'MJ_'+L.BEDEN PRODUCT_SIZE  
,L.MAJOR_QUANTITY  
,Q.QUESTION  
,Q.ID AS QUESTION_ID
FROM PRODUCTION_AQL_QUESTIONS Q 
LEFT JOIN PRODUCTION_AQL_LINE L ON Q.ID=L.QUESTION_ID 
LEFT JOIN PRODUCTION_AQL_HEADER H ON H.ID=L.HEADER_ID AND TYPE={8}
WHERE H.ENTEGRATION_HEADER_ID={0}
 ) A PIVOT  
(  
  SUM(MAJOR_QUANTITY) 
  FOR PRODUCT_SIZE IN ({1})  
) AS PivotTable
 UNION ALL
SELECT QUESTION,QUESTION_ID,{4},{3} FROM (
SELECT 'MN_'+L.BEDEN PRODUCT_SIZE  
,L.MINOR_QUANTITY  
,Q.QUESTION  
,Q.ID AS QUESTION_ID
FROM PRODUCTION_AQL_QUESTIONS Q 
LEFT JOIN PRODUCTION_AQL_LINE L ON Q.ID=L.QUESTION_ID 
LEFT JOIN PRODUCTION_AQL_HEADER H ON H.ID=L.HEADER_ID AND TYPE={8}
WHERE H.ENTEGRATION_HEADER_ID={0}
 ) A PIVOT  
(  
  SUM(MINOR_QUANTITY) 
  FOR PRODUCT_SIZE IN ({3})  
) AS PivotTable
) A GROUP BY QUESTION,QUESTION_ID
", ENTEGRATION_HEADER_ID, mjBottom, mnBottomSecond, mnBottom, mjBottomSecond, mjSum, mnSum, header.ID, TYPE);
            DataTable dt = _fasonRepository.QueryFill(sql);
            List<PRODUCTION_AQL_QUANTITYS> quantitys = _fasonRepository.DeserializeList<PRODUCTION_AQL_QUANTITYS>("SELECT * FROM PRODUCTION_AQL_QUANTITYS WHERE HEADER_ID=" + header.ID + "");

            var list = _fasonService.GetProcessList(ENTEGRATION_HEADER_ID);
            PRODUCTION_LIST_V pr = new PRODUCTION_LIST_V();
            if (TYPE == 1)
                pr = list.Find(ob => ob.PROCESS_NAME == "AQL - ÜRETIM");
            else if (TYPE == 2)
                pr = list.Find(ob => ob.PROCESS_NAME == "AQL - ÇORLU");

            if (pr.DETAIL_ID > 0)
            {
                if(pr.STATUS== (int)LINESTATUS.WAITINGFORSTART)
                    _fasonService.SurecIlerlet(pr);
            }
            return new Tuple<PRODUCTION_AQL_HEADER, DataTable, List<PRODUCTION_AQL_QUANTITYS>, AQL_ANALIZER>(header, dt, quantitys, AQL_ANALIZER);
        }

        public TaskResponse UpdateAQL(JObject elem)
        {
            TaskResponse response = new TaskResponse() { isOK = true, errorText = "" };
            try
            {
                dynamic value = elem;
                string newValue = Convert.ToString(value.newValue);
                string oldValues = Convert.ToString(value.oldValue);
                Dictionary<string, object> newItem = JsonConvert.DeserializeObject<Dictionary<string, object>>(newValue);
                Dictionary<string, object> oldItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(oldValues);

                int QUANTITY = newItem.ElementAt(0).Value.IntParse();
                string elementName = newItem.ElementAt(0).Key.ToString();
                object QUESTION_ID = 0, HEADER_ID = 0;

                oldItems.TryGetValue("QUESTION_ID", out QUESTION_ID);
                oldItems.TryGetValue("HEADER_ID", out HEADER_ID);
                if (elementName.StartsWith("MN_"))
                {
                    string sql = string.Format("UPDATE PRODUCTION_AQL_LINE SET MINOR_QUANTITY={0} WHERE BEDEN='{1}' AND HEADER_ID={2} AND QUESTION_ID={3}", QUANTITY, elementName.Replace("MN_", ""), HEADER_ID, QUESTION_ID);
                    _fasonRepository.ExecSql(sql);
                }
                else
                {
                    string sql = string.Format("UPDATE PRODUCTION_AQL_LINE SET MAJOR_QUANTITY={0} WHERE BEDEN='{1}' AND HEADER_ID={2} AND QUESTION_ID={3}", QUANTITY, elementName.Replace("MJ_", ""), HEADER_ID, QUESTION_ID);
                    _fasonRepository.ExecSql(sql);
                }
            }
            catch (Exception ex)
            {
                response.isOK = false;
                response.errorText = ex.Message;
            }
            
            return response;
        }

        public TaskResponse UpdateNumbers(JObject elem)
        {
            TaskResponse response = new TaskResponse() { isOK = true, errorText = "" };
            try
            {
                dynamic value = elem;
                string newValue = Convert.ToString(value.newValue);
                string oldValues = Convert.ToString(value.oldValue);
                Dictionary<string, object> newItem = JsonConvert.DeserializeObject<Dictionary<string, object>>(newValue);
                Dictionary<string, object> oldItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(oldValues);

                int QUANTITY = newItem.ElementAt(0).Value.IntParse();
                string elementName = newItem.ElementAt(0).Key.ToString();
                object ID = 0;
                oldItems.TryGetValue("ID", out ID);

                string sql = string.Format("UPDATE PRODUCTION_AQL_QUANTITYS SET {0}={1} WHERE ID={2}", elementName, QUANTITY, ID);
                _fasonRepository.ExecSql(sql);
            }
            catch (Exception ex)
            {
                response.isOK = false;
                response.errorText = ex.Message;
            }
           
            return response;
        }

        public SIPARIS_ISLEMLER_KONTROL SiparisIslemlerKontrol(int ENTEGRATION_HEADER_ID)
        {
            SIPARIS_ISLEMLER_KONTROL kontrol = new SIPARIS_ISLEMLER_KONTROL();
            PRODUCTION_LIST_V productStatus = _fasonRepository.Deserialize<PRODUCTION_LIST_V>(@"select * from PRODUCTION_LIST_V WHERE ENTEGRATION_ID="+ ENTEGRATION_HEADER_ID + "");
            if(productStatus.PROCESS_NAME== "AQL - ÜRETIM")
            {
                kontrol.CAN_CREATE_AQL_FIRMA = true;
            }
            if (productStatus.PROCESS_NAME == "AQL - ÇORLU")
            {
                kontrol.CAN_CREATE_AQL_CORLU = true;
            }

            if (kontrol.CAN_CREATE_AQL_CORLU || kontrol.CAN_CREATE_AQL_FIRMA)
                kontrol.CAN_CREATE_AQL_INLINE = false;
            kontrol.ENTEGRATION_HEADER_ID = ENTEGRATION_HEADER_ID;
            return kontrol;
        }

        public TaskResponse SaveAQL(JObject elem)
        {
            TaskResponse response = new TaskResponse() { isOK = true, errorText = "" };
            try
            {
                dynamic value = elem;
                int ENTEGRATION_ID = value.ENTEGRATION_ID;
                int TYPE = value.TYPE;
                int STATUS = value.STATUS;
                bool CHECKED_INLINE = value.CHECKED_INLINE;
                string DESCRIPTION = value.DESCRIPTION;
                PRODUCTION_AQL_HEADER header = _fasonRepository.Deserialize<PRODUCTION_AQL_HEADER>("SELECT * FROM PRODUCTION_AQL_HEADER WHERE ENTEGRATION_HEADER_ID=" + ENTEGRATION_ID + " AND TYPE=" + TYPE + "");
                header.CHECKED_INLINE = CHECKED_INLINE;
                if (STATUS == 1)
                    header.STATUS = (int)AQLHEADERSTATUS.FINISHED;
                header.DESCRIPTION = DESCRIPTION;
                _fasonRepository.Serialize(header);
                var list = _fasonService.GetProcessList(ENTEGRATION_ID);
                PRODUCTION_LIST_V pr = new PRODUCTION_LIST_V();
                if (TYPE == 1)
                    pr = list.Find(ob => ob.PROCESS_NAME == "AQL - ÜRETIM");
                else if (TYPE == 2)
                    pr = list.Find(ob => ob.PROCESS_NAME == "AQL - ÇORLU");

                if (pr.DETAIL_ID > 0)
                {
                    _fasonService.SurecIlerlet(pr);
                }
            }
            catch (Exception ex)
            {
                response.isOK = false;
                response.errorText = ex.Message;
            }
            return response;

        }

        public List<INSPECTION_LIST> GetInspectionList(INSPECTION_FILTER filter)
        {
            string sql = @"SELECT PH.MODEL,PH.COLOR,PH.SEASON,PAH.STATUS,USR.NAME AS FIRMA,CASE WHEN PAH.TYPE=1 THEN 'AQL-ÜRETİM' ELSE 'AQL-ÇORLU' END TYPEEX,PAH.TYPE,INC.NAME +' '+INC.SURNAME AS INSPECTOR
,(SELECT SUM(QUANTITY) FROM PRODUCTION_AQL_QUANTITYS QYS WHERE QYS.HEADER_ID=PAH.ID) AS QUANTITY
,(SELECT SUM(QUANTITY_RELEASE) FROM PRODUCTION_AQL_QUANTITYS QYS WHERE QYS.HEADER_ID=PAH.ID) AS QUANTITY_RELEASE
,(SELECT SUM(QUANTITY_SAMPLE) FROM PRODUCTION_AQL_QUANTITYS QYS WHERE QYS.HEADER_ID=PAH.ID) AS QUANTITY_SAMPLE
,PAH.START_DATE
,PAH.ENTEGRATION_HEADER_ID
FROM PRODUCTION_AQL_HEADER PAH
INNER JOIN PRODUCTION_HEADER PH ON PH.ENTEGRATION_ID=PAH.ENTEGRATION_HEADER_ID
INNER JOIN PRODUCTION_LIST_H LH ON LH.ENTEGRATION_ID=PAH.ENTEGRATION_HEADER_ID
INNER JOIN PRODUCTION_FASON_INSPECTORS INC ON INC.ID=PAH.USER_ID
INNER JOIN PRODUCTION_FASON_USERS USR ON USR.ID=LH.FIRMA_ID WHERE 0=0";
            if (filter.SEASON.ToStringParse() != "HEPSİ")
                sql += " AND PH.SEASON ='" + filter.SEASON + "'";
            if (filter.MODEL.ToStringParse() != "")
                sql += " AND PH.MODEL ='" + filter.MODEL + "'";
            if (filter.COLOR.ToStringParse() != "")
                sql += " AND PH.COLOR ='" + filter.COLOR + "'";
            if (filter.INSPECTOR_ID !=0)
                sql += " AND INC.ID =" + filter.INSPECTOR_ID + "";
            if (filter.FIRMA_ID != 0)
                sql += " AND USR.ID =" + filter.FIRMA_ID + "";
            return _fasonRepository.DeserializeList<INSPECTION_LIST>(sql);
        }

        public List<PRODUCTION_FASON_INSPECTORS> GetInspectorList(bool hepsi)
        {
            string sql = "";
            if (hepsi)
                sql = "SELECT 0 ID,'HEPSİ' NAME   UNION ALL SELECT ID,NAME+'' +SURNAME FROM PRODUCTION_FASON_INSPECTORS";
            else sql = "SELECT ID,NAME+'' +SURNAME FROM PRODUCTION_FASON_INSPECTORS";
            return _fasonRepository.DeserializeList<PRODUCTION_FASON_INSPECTORS>(sql);
        }

        public List<PRODUCTION_AQL_INLINE> GetInspectionInlineAQL(int USER_ID, int ENTEGRATION_HEADER_ID)
        {
            string sql = "SELECT * FROM PRODUCTION_AQL_INLINE WHERE HEADER_ID=" + ENTEGRATION_HEADER_ID;
            return _fasonRepository.DeserializeList<PRODUCTION_AQL_INLINE>(sql);
        }

        public List<PRODUCTION_PROCESS> GetProcessList(bool hepsi)
        {
            string sql = "";
            if (hepsi)
                sql = "SELECT 0 ID,'HEPSİ' NAME   UNION ALL SELECT ID,NAME FROM PRODUCTION_PROCESS";
            else sql = "SELECT ID,NAME FROM PRODUCTION_PROCESS";
            return _fasonRepository.DeserializeList<PRODUCTION_PROCESS>(sql);
        }

        public PRODUCTION_AQL_INLINE InsertInspection(int USER_ID, string values)
        {
            PRODUCTION_AQL_INLINE detail = new PRODUCTION_AQL_INLINE();
            JsonConvert.PopulateObject(values, detail); 
            detail.USER_ID = USER_ID;
            _fasonRepository.Serialize<PRODUCTION_AQL_INLINE>(detail);
            return detail;
        }

        public Tuple<TaskResponse, PRODUCTION_AQL_INLINE> UpdateInspection(int Key, string Values)
        {
            TaskResponse response = new TaskResponse() { isOK = true, errorText = "" };

            PRODUCTION_AQL_INLINE detail = _fasonRepository.Deserialize<PRODUCTION_AQL_INLINE>(Key);
            try
            {
                JsonConvert.PopulateObject(Values, detail);
                _fasonRepository.Serialize(detail);
            }
            catch (Exception ex)
            {
                response.isOK =false;
                response.errorText = ex.Message;
            }

            return new Tuple<TaskResponse, PRODUCTION_AQL_INLINE>(response,detail);
        }

        public TaskResponse DeleteInspection(int Key)
        {
            TaskResponse response = new TaskResponse() { isOK = true, errorText = "" };

            try
            {
                string sql = "DELETE FROM PRODUCTION_AQL_INLINE WHERE ID=" + Key;
                _fasonRepository.ExecSql(sql);
            }
            catch (Exception ex)
            {
                response.isOK = false;
                response.errorText = ex.Message;
            }

            return response;
        }
    }
}
