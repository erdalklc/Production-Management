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
        public InspectionService(IFasonRepository fasonRepository)
        {
            _fasonRepository = fasonRepository;
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
LEFT JOIN PRODUCTION_AQL_HEADER H ON H.ID=L.HEADER_ID
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
            kontrol.ENTEGRATION_HEADER_ID = ENTEGRATION_HEADER_ID;
            return kontrol;
        }
    }
}
