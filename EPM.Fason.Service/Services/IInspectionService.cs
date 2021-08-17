using EPM.Fason.Dto.Fason;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Service.Services
{
    public interface IInspectionService
    {
        public List<PRODUCTION_FASON_MENU> GetMenuList();

        public List<SEASONMODEL> GetSeasonList(bool hepsi);

        public List<PRODUCTION_FASON_USERS> GetFasonUsers(bool hepsi);

        public IEnumerable<SIPARIS_LISTESI> GetSiparisList(INSPECTION_FILTER liste);

        IEnumerable<SIPARIS_LISTESI_DETAIL> GetSiparisDetailList(int ENTEGRASYON_ID);

        public List<AQL_MODEL> GetAQL(int USER_ID, int ENTEGRATION_HEADER_ID);
    }
}
