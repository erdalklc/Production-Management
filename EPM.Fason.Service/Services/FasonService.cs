using EPM.Fason.Dto.Fason;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Service.Services
{
    public class FasonService : IFasonService
    {
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


//        SELECT PH.* 
//, (SELECT SUM(QUANTITY) FROM PRODUCTION_DETAIL PL WHERE PL.ENTEGRATION_HEADER_ID = PH.ENTEGRATION_ID) AS QUANTITY
// , ST.STATUSEX
//,ST.NAME PROCESS_NAME
//, ST.START_DATE
//,ST.END_DATE
//FROM  PRODUCTION_HEADER PH
//LEFT JOIN(
//SELECT STATUS, dbo.SENDSTATUSEX(STATUS) STATUSEX, PRC.NAME, PRH.ENTEGRATION_ID, PRL.END_DATE, PRL.START_DATE FROM PRODUCTION_LIST_L PRL
//INNER JOIN PRODUCTION_LIST_H PRH ON PRH.ID= PRL.HEADER_ID
//INNER JOIN PRODUCTION_PROCESS PRC ON PRC.ID= PRL.PROCESS_ID
//WHERE STATUS NOT IN(0,3)
//) ST ON ST.ENTEGRATION_ID=PH.ENTEGRATION_ID

//SELECT dbo.SENDSTATUSEX(1)

//SELECT STATUS, PRL.END_DATE,PRL.START_DATE,PRC.NAME,PRH.ENTEGRATION_ID FROM PRODUCTION_LIST_L PRL
//INNER JOIN PRODUCTION_LIST_H PRH ON PRH.ID=PRL.HEADER_ID
//INNER JOIN PRODUCTION_PROCESS PRC ON PRC.ID= PRL.PROCESS_ID
//WHERE HEADER_ID = 3 AND STATUS NOT IN(0,3) ORDER BY QUEUE


//  CREATE FUNCTION dbo.SENDSTATUSEX(@input int)
//RETURNS VARCHAR(250)
//AS BEGIN
//    DECLARE @Work VARCHAR(250)

//    if @input =0 
//		SET @Work = 'BEKLENİYOR'
//	else if @input=1
//		SET @Work = 'BAŞLAMASI BEKLENİYOR' 
//	else if @input=2
//		SET @Work = 'BAŞLADI' 
//	else if @input=3
//		SET @Work = 'TAMAMLANDI'

//    RETURN @work
//END
    }
}
