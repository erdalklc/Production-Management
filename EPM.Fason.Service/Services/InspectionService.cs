using EPM.Fason.Dto.Fason;
using EPM.Fason.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Service.Services
{
    public class InspectionService : IInspectionService
    {
        private readonly IFasonRepository _fasonRepository;
        public InspectionService(IFasonRepository fasonRepository)
        {
            _fasonRepository = fasonRepository;
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
                CONTROLLER = "Fason",
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
                ID = 2,
                CATEGORY_ID = 2,
                CONTROLLER = "Fason",
                TEXT = "INSPECTION LİSTESİ",
                ICON = "spinright",
                ACTION = "InspectionListesi",
                ISVISIBLE = true,
                SELECTED = true,
                ISEXPANDED = true,
                LOW_TEXT = "SL"

            });
            return menu;
        }
    }
}
