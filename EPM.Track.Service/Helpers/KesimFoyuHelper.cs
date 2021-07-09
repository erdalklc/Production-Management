using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Service.Helpers
{
    public class KesimFoyuHelper
    {
        public string KesimFoyleri(int ITEM_NUMBER, int PO_HEADER_ID, string MAMUL_RENGI, string ANAMODEL)
        {
            string sql = string.Format(@"
                            SELECT 
                            WE.WIP_ENTITY_ID
                             ,WE.WIP_ENTITY_NAME
                            ,IM.ROTA
                            ,IM.MAMUL_RENGI
                            ,CAST(IM.PLANLANAN_KESIM AS NUMBER) PLANLANAN_KESIM
                            ,CAST(IM.FIILI_KESIM AS NUMBER) FIILI_KESIM
                            ,CAST(IM.TASNIF_MIKTARI AS NUMBER) TASNIF_MIKTARI
                            ,IM.IS_EMRI_NO
                            ,IM.ID AS IS_EMRI_ID
                            ,IM.BEDEN
                            ,IM.MODEL
                            ,IM.SEZON_BILGISI
                            ,IM.DEGERLENDIRME
                            FROM  APPS.WIP_ENTITIES  WE
                            INNER JOIN ( 
                            SELECT TRANSACTION_SOURCE_ID FROM INV.MTL_TRANSACTION_LOT_NUMBERS WHERE TRANSACTION_ID IN (SELECT TRANSACTION_ID FROM  INV.MTL_MATERIAL_TRANSACTIONS WHERE INVENTORY_ITEM_ID ={0}  AND TRANSACTION_TYPE_ID=35) 
                            AND LOT_NUMBER IN (
                            SELECT LOT_NUMBER FROM INV.MTL_TRANSACTION_LOT_NUMBERS WHERE TRANSACTION_ID IN (SELECT TRANSACTION_ID FROM  INV.MTL_MATERIAL_TRANSACTIONS WHERE INVENTORY_ITEM_ID ={0}  AND TRANSACTION_SOURCE_ID={1}))
                            ) KESIM ON KESIM.TRANSACTION_SOURCE_ID=WE.WIP_ENTITY_ID
                            INNER JOIN XXXT.XXXT_IS_EMIRLERI IM ON 'K'||IM.KESIM_FOYU_NO =WE.WIP_ENTITY_NAME
                            WHERE MAMUL_RENGI='{2}' AND IM.ROTA='{3}'
", ITEM_NUMBER, PO_HEADER_ID, MAMUL_RENGI, ANAMODEL + ".X.X");
            return sql;
        }

        public string KesimFoyleri2(int ITEM_NUMBER, int PO_HEADER_ID, string MAMUL_RENGI, string ANAMODEL)
        {
            string sql = string.Format(@"
                            SELECT 
                              WE.WIP_ENTITY_NAME
                            ,IM.ROTA
                            ,IM.MAMUL_RENGI
                            ,CAST(IM.PLANLANAN_KESIM AS NUMBER) PLANLANAN_KESIM
                            ,CAST(IM.FIILI_KESIM AS NUMBER) FIILI_KESIM
                            ,CAST(IM.TASNIF_MIKTARI AS NUMBER) TASNIF_MIKTARI
                            ,IM.IS_EMRI_NO
                            ,IM.BEDEN
                            ,IM.SEZON_BILGISI
                            ,IM.DEGERLENDIRME
                            FROM  APPS.WIP_ENTITIES  WE 
                            INNER JOIN XXXT.XXXT_IS_EMIRLERI IM ON 'K'||IM.KESIM_FOYU_NO =WE.WIP_ENTITY_NAME
                            WHERE MAMUL_RENGI='{0}' AND IM.ROTA='{1}'
", MAMUL_RENGI, ANAMODEL + ".X.X");
            return sql;
        }

        public string KesimFoyleri3(string KesimFoyu)
        {
            string sql = string.Format(@"
                            SELECT 
                              WE.WIP_ENTITY_NAME
                            ,IM.ROTA
                            ,IM.MAMUL_RENGI
                            ,CAST(IM.PLANLANAN_KESIM AS NUMBER) PLANLANAN_KESIM
                            ,CAST(IM.FIILI_KESIM AS NUMBER) FIILI_KESIM
                            ,CAST(IM.TASNIF_MIKTARI AS NUMBER) TASNIF_MIKTARI
                            ,IM.IS_EMRI_NO
                            ,IM.BEDEN
                            ,IM.SEZON_BILGISI
                            ,IM.DEGERLENDIRME
                            FROM  APPS.WIP_ENTITIES  WE 
                            INNER JOIN XXXT.XXXT_IS_EMIRLERI IM ON 'K'||IM.KESIM_FOYU_NO =WE.WIP_ENTITY_NAME
                            WHERE WE.WIP_ENTITY_NAME='{0}'", KesimFoyu);
            return sql;
        }
    }
}
