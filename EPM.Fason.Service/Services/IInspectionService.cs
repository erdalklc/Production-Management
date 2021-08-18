using EPM.Fason.Dto.Fason;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EPM.Fason.Service.Services
{
    public interface IInspectionService
    {
        public List<PRODUCTION_FASON_MENU> GetMenuList();

        public List<SEASONMODEL> GetSeasonList(bool hepsi);

        public List<PRODUCTION_FASON_USERS> GetFasonUsers(bool hepsi);

        public List<PRODUCTION_FASON_INSPECTORS> GetInspectorList(bool hepsi);

        public IEnumerable<SIPARIS_LISTESI> GetSiparisList(INSPECTION_FILTER liste);

        IEnumerable<SIPARIS_LISTESI_DETAIL> GetSiparisDetailList(int ENTEGRASYON_ID);

        public Tuple<PRODUCTION_AQL_HEADER, DataTable, List<PRODUCTION_AQL_QUANTITYS>, AQL_ANALIZER> GetAQL(int USER_ID, int ENTEGRATION_HEADER_ID,int TYPE);

        public TaskResponse UpdateAQL(JObject elem);

        public TaskResponse UpdateNumbers(JObject elem);

        public SIPARIS_ISLEMLER_KONTROL SiparisIslemlerKontrol(int ENTEGRATION_HEADER_ID);

        public TaskResponse SaveAQL(JObject elem);

        public List<INSPECTION_LIST> GetInspectionList(INSPECTION_FILTER filter);

        public List<PRODUCTION_AQL_INLINE> GetInspectionInlineAQL(int USER_ID, int ENTEGRATION_HEADER_ID);

        public List<PRODUCTION_PROCESS> GetProcessList(bool hepsi);

        public PRODUCTION_AQL_INLINE InsertInspection(int USER_ID, string values);

        public Tuple<TaskResponse,PRODUCTION_AQL_INLINE> UpdateInspection(int Key, string Values);

        public TaskResponse DeleteInspection(int Key);
    }
}
