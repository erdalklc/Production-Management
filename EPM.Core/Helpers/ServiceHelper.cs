using EPM.Core.Managers;
using EPM.Core.Models; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq; 

namespace EPM.Core.Helpers
{
    public class ServiceHelper
    {
        public static string DETAILSQL()
        {
            string sql = @" 
                            SELECT * FROM (
                            SELECT
                            PH.SEGMENT1 AS TAKIP_NO  
                            ,PH.PO_HEADER_ID    
                            ,AG.FIRST_NAME ||' '||AG.LAST_NAME AS AGENT_NAME,PH.CREATION_DATE
                            ,PL.ITEM_ID
                            ,MSI.DESCRIPTION URUN
                            ,MSI.SEGMENT1 ||'.'||MSI.SEGMENT2||'.'||MSI.SEGMENT3 AS KALEM 
                            ,MSI.SEGMENT1 MODELDETAY
                            ,MSI.SEGMENT2 RENKDETAY
                            ,PL.UNIT_MEAS_LOOKUP_CODE AS BIRIM
                            ,PL.UNIT_PRICE BIRIMFIYAT
                            ,PL.QUANTITY AS TEDARIK
                            ,PL.UNIT_PRICE*PL.QUANTITY AS TUTAR
                            ,PL.PO_LINE_ID DETAIL_TAKIP_NO
                            FROM APPS.PO_HEADERS_ALL PH 
                            INNER JOIN APPS.PER_PEOPLE_F AG ON AG.PERSON_ID=PH.AGENT_ID 
                            INNER JOIN APPS.PO_LINES_ALL PL ON PL.PO_HEADER_ID=PH.PO_HEADER_ID
                            INNER JOIN APPS.MTL_SYSTEM_ITEMS_B MSI ON MSI.INVENTORY_ITEM_ID=PL.ITEM_ID AND  MSI.ORGANIZATION_ID=111
                            WHERE  AG.EMPLOYEE_NUMBER IS NOT NULL 
                            AND TRUNC (SYSDATE) BETWEEN AG.EFFECTIVE_START_DATE AND AG.EFFECTIVE_END_DATE ) A WHERE 0=0";
            return sql;
        }
        public static DataTable SURECBILGILERINIGETIR(int HEADER_ID,int DETAIL_ID)
        {
            string sql = string.Format(@"
                        SELECT PL.ID, PL.HEADER_ID,
                             PL.PROCESS_ID,
                             PL.START_DATE,
                             PL.END_DATE,
                             PL.STATUS,
                             PI.PROCESSNAME,
                             PI.PROCESSTAG
                        FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
                             INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI ON PI.ID = PL.PROCESS_ID 
                             INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD ON RD.HEADER_ID=(SELECT CAST(PO.ATTRIBUTE9 AS NUMBER) FROM APPS.PO_HEADERS_ALL PO WHERE PO.SEGMENT1=CAST(PL.HEADER_ID AS VARCHAR2(100)) and rownum=1) AND RD.PROCESS_ID=PL.PROCESS_ID
                             WHERE PL.HEADER_ID={0} AND PL.DETAIL_ID={1}
                    ORDER BY RD.QUEUE", HEADER_ID, DETAIL_ID);
            return OracleServer.QueryFill(sql);
        }
         
    }
}
