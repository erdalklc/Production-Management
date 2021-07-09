using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Service.Helpers
{
    public class KumasDepoHelper
    {
        public string KumasDepoHareketleri(int ITEM_NUMBER, int PO_HEADER_ID, string TYPE = "")
        {
            string sql = string.Format(@"
                    SELECT 
                        TRA.TRANSACTION_ID
                        ,TRA.CREATION_DATE
                        ,US.DESCRIPTION AS OLUSTURAN
                        ,MSI.SEGMENT1||'.'||MSI.SEGMENT2||'.'||MSI.SEGMENT3 AS KALEM
                        ,MSI.DESCRIPTION AS URUN_GRUBU
                        ,TYP.TRANSACTION_TYPE_NAME ISLEM_TIPI
                        ,TRA.TRANSACTION_QUANTITY ISLEM_MIKTARI
                        ,TRA.TRANSACTION_UOM BIRIM 
                        ,TRA.TRANSACTION_TYPE_ID 
                        FROM  INV.MTL_MATERIAL_TRANSACTIONS TRA
                        INNER JOIN(
                        SELECT TRANSACTION_ID FROM INV.MTL_TRANSACTION_LOT_NUMBERS WHERE TRANSACTION_ID IN (SELECT TRANSACTION_ID FROM  INV.MTL_MATERIAL_TRANSACTIONS WHERE INVENTORY_ITEM_ID = {0}  AND TRANSACTION_TYPE_ID IN( 18,35))
                        AND LOT_NUMBER IN(
                        SELECT LOT_NUMBER FROM INV.MTL_TRANSACTION_LOT_NUMBERS WHERE TRANSACTION_ID IN(SELECT TRANSACTION_ID FROM  INV.MTL_MATERIAL_TRANSACTIONS WHERE INVENTORY_ITEM_ID = {0}  AND TRANSACTION_SOURCE_ID = {1}))
                        ) a on a.transaction_id = tra.transaction_id
                        INNER JOIN APPS.FND_USER US ON US.USER_ID=TRA.CREATED_BY
                        INNER JOIN APPS.MTL_SYSTEM_ITEMS_B MSI ON MSI.INVENTORY_ITEM_ID=TRA.INVENTORY_ITEM_ID AND MSI.ORGANIZATION_ID=111
                        INNER JOIN INV.MTL_TRANSACTION_TYPES TYP ON TYP.TRANSACTION_TYPE_ID=TRA.TRANSACTION_TYPE_ID
                       ", ITEM_NUMBER, PO_HEADER_ID);


            if (TYPE != "")
                sql += " WHERE TRA.TRANSACTION_TYPE_ID =" + TYPE;
            sql += " ORDER BY CREATION_DATE";
            return sql;
        }
    }
}
