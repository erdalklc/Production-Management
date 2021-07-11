using EPM.Fason.Dto.Fason;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Service.Services
{
    public interface IFasonService
    {

        public List<PRODUCTION_FASON_MENU> GetMenuList();
        IEnumerable<SIPARIS_LISTESI> GetSiparisList(SIPARIS_FILTER liste);
        IEnumerable<SIPARIS_LISTESI_DETAIL> GetSiparisDetailList(int ENTEGRASYON_ID);
        IEnumerable<PRODUCTION_LIST_V> GetSiparisProcessList(int ENTEGRASYON_ID);
        object[] SurecIlerlet(PRODUCTION_LIST_V surec);

    }
}
