using EPM.Fason.Dto.Fason;
using EPM.Fason.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IEnumerable<SIPARIS_LISTESI> GetSiparisList(SIPARIS_FILTER liste)
        {
            string sql = "SELECT * FROM  PRODUCTION_LIST_V WHERE 0=0";

            sql += string.Format(" AND DEADLINE_CUSTOMER BETWEEN {0} AND {1}",_fasonRepository.DateTimeString(liste.BASLANGIC_TARIHI), _fasonRepository.DateTimeString(liste.BITIS_TARIHI));
            if (liste.MODEL != null && liste.MODEL != null) sql += string.Format(" AND MODEL='{0}'", liste.MODEL);
            if (liste.COLOR != null && liste.COLOR != null) sql += string.Format(" AND COLOR='{0}'", liste.COLOR); 

            return _fasonRepository.DeserializeList<SIPARIS_LISTESI>(sql);
        }
         
    }
}
