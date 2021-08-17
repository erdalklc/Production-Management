using EPM.Fason.Dto.Fason;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Fason.Web.Helpers
{
    public class MenuHelper
    {
        public List<PRODUCTION_FASON_MENU> GetFasonMenuList()
        {
            List<PRODUCTION_FASON_MENU> menu = new List<PRODUCTION_FASON_MENU>();
            menu.Add(new PRODUCTION_FASON_MENU()
            {
                ID = 1,
                ISEXPANDED = true,
                ICON = "product",
                TEXT = "SİPARİŞ YÖNETİMİ",
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
                LOW_TEXT = "SL"

            });

            return menu;
        }
        public List<PRODUCTION_FASON_MENU> GetInspectionMenuList()
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
    }
}
