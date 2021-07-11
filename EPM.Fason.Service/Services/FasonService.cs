using EPM.Fason.Dto.Fason;
using EPM.Fason.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EPM.Fason.Dto.Extensions.Enums;

namespace EPM.Fason.Service.Services
{
    public class FasonService : IFasonService
    {
        private readonly IFasonRepository _fasonRepository;
        public FasonService(IFasonRepository fasonRepository)
        {
            _fasonRepository = fasonRepository;
        }
        public List<PRODUCTION_FASON_MENU> GetMenuList()
        {
            List<PRODUCTION_FASON_MENU> menu = new List<PRODUCTION_FASON_MENU>();
            menu.Add(new PRODUCTION_FASON_MENU()
            {
                ID=1,
                ISEXPANDED=true, 
                ICON="product", 
                TEXT="SİPARİŞ YÖNETİMİ",
                ISVISIBLE = true,
                SELECTED=true
            });
            menu.Add(new PRODUCTION_FASON_MENU()
            {
                ID=2,
                CATEGORY_ID=1,
                CONTROLLER="Fason",
                TEXT="SİPARİŞ LİSTESİ",
                ICON="spinright",
                ACTION= "SiparisListesi",
                ISVISIBLE=true,
                SELECTED=true,
                ISEXPANDED=true,
                LOW_TEXT="SL"
                
            });

            return menu;
        }

        public IEnumerable<SIPARIS_LISTESI_DETAIL> GetSiparisDetailList(int ENTEGRASYON_ID)
        {
            string sql = "SELECT * FROM  PRODUCTION_DETAIL_LIST_V WHERE 0=0 AND  ENTEGRATION_HEADER_ID="+ENTEGRASYON_ID;
            return _fasonRepository.DeserializeList<SIPARIS_LISTESI_DETAIL>(sql);
        }

        public IEnumerable<SIPARIS_LISTESI> GetSiparisList(SIPARIS_FILTER liste)
        {
            string sql = "SELECT * FROM  PRODUCTION_LIST_V WHERE 0=0";

            sql += string.Format(" AND DEADLINE_CUSTOMER BETWEEN {0} AND {1}",_fasonRepository.DateTimeString(liste.BASLANGIC_TARIHI), _fasonRepository.DateTimeString(liste.BITIS_TARIHI));
            if (liste.MODEL != null && liste.MODEL != null) sql += string.Format(" AND MODEL='{0}'", liste.MODEL);
            if (liste.COLOR != null && liste.COLOR != null) sql += string.Format(" AND COLOR='{0}'", liste.COLOR); 

            return _fasonRepository.DeserializeList<SIPARIS_LISTESI>(sql);
        }


        List<PRODUCTION_LIST_V> GetProcessList(int ENTEGRASYON_ID)
        {
            string sql = string.Format(@"SELECT PL.ID DETAIL_ID,PL.HEADER_ID,PH.ENTEGRATION_ID,PR.NAME PROCESS_NAME,PL.START_DATE,PL.END_DATE,PL.STATUS,dbo.SENDSTATUSEX(PL.STATUS) STATUS_EX,PL.QUEUE FROM PRODUCTION_LIST_L PL
INNER JOIN PRODUCTION_LIST_H PH ON PH.ID=PL.HEADER_ID
INNER JOIN PRODUCTION_PROCESS PR ON PR.ID=PL.PROCESS_ID WHERE PH.ENTEGRATION_ID={0} ORDER BY PL.QUEUE", ENTEGRASYON_ID);
            var list = _fasonRepository.DeserializeList<PRODUCTION_LIST_V>(sql);
            return list;
        }
        public IEnumerable<PRODUCTION_LIST_V> GetSiparisProcessList(int ENTEGRASYON_ID)
        {
            return GetProcessList(ENTEGRASYON_ID); 
        }

        public object[] SurecIlerlet(PRODUCTION_LIST_V surec)
        {
            object[] ok = { true, "" };
            var list = GetProcessList(surec.ENTEGRATION_ID);

            try
            {
                if (surec.STATUS == (int)LINESTATUS.WAITINGFORSTART)
                {
                    PRODUCTION_LIST_L line = _fasonRepository.Deserialize<PRODUCTION_LIST_L>(surec.DETAIL_ID);
                    line.STATUS = (int)LINESTATUS.STARTED;
                    _fasonRepository.Serialize(line);
                    PRODUCTION_LIST_H header = _fasonRepository.Deserialize<PRODUCTION_LIST_H>(surec.HEADER_ID);
                    header.STATUS = (int)HEADERSTATUS.STARTED; 
                    _fasonRepository.Serialize(header);
                }
                else if (surec.STATUS == (int)LINESTATUS.STARTED)
                {

                    PRODUCTION_LIST_L line = _fasonRepository.Deserialize<PRODUCTION_LIST_L>(surec.DETAIL_ID);
                    line.STATUS = (int)LINESTATUS.FINISHED;
                    _fasonRepository.Serialize(line);
                    var tt = list.FindAll(ob => ob.QUEUE > line.QUEUE);
                    if (tt.Count > 0)
                    {
                        tt = tt.OrderBy(ob => ob.QUEUE).ToList();
                        line = _fasonRepository.Deserialize<PRODUCTION_LIST_L>(tt[0].DETAIL_ID);
                        line.STATUS = (int)LINESTATUS.WAITINGFORSTART;
                        _fasonRepository.Serialize(line);
                    }
                    else
                    {
                        PRODUCTION_LIST_H header = _fasonRepository.Deserialize<PRODUCTION_LIST_H>(surec.HEADER_ID);
                        header.STATUS = (int)HEADERSTATUS.FINISHED;
                    }
                }
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = ex.Message;
            }

            return ok;
        }
    }
}
