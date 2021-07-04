using Dapper.Contrib.Extensions;
using EPM.Core.Helpers;
using EPM.Core.Managers;
using EPM.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_PRODUCTION_TRACKING_LIST")]
    public class EPM_PRODUCTION_TRACKING_LIST
    {
        [Key]
        public long ID { get; set; }
        public int PO_HEADER_ID { get; set; }
        public int HEADER_ID { get; set; }
        public int PROCESS_ID { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public int STATUS { get; set; }
        public long DETAIL_ID { get; set; }
        public decimal BEKLENEN_MIKTAR { get; set; }
        public decimal GERCEKLESEN_MIKTAR { get; set; }

        public static List<EPM_PRODUCTION_TRACKING_LIST> GETLISTMODEL(int PO_HEADER_ID, int DETAIL_ID,int RECIPE,int HEADER_ID)
        {
            string sql = string.Format(@"
                           SELECT PL.*,RD.QUEUE
  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
       INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI ON PI.ID = PL.PROCESS_ID
       INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE RH ON RH.ID={2}
       INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD ON RD.HEADER_ID=RH.ID AND RD.PROCESS_ID=PI.ID
 WHERE PL.PO_HEADER_ID = {0} AND PL.DETAIL_ID = {1} AND PL.HEADER_ID={3} ORDER BY RD.QUEUE", PO_HEADER_ID, DETAIL_ID, RECIPE, HEADER_ID);
            return OracleServer.DeserializeList<EPM_PRODUCTION_TRACKING_LIST>(sql);
        }
        public static bool ISTHERELIST(int PO_HEADER_ID, int DETAIL_ID ,int HEADER_ID)
        {
            bool var = false;

            if (OracleServer.ReadInteger(string.Format(@"SELECT Count(*) From FDEIT005.EPM_PRODUCTION_TRACKING_LIST WHERE PO_HEADER_ID={0} AND  DETAIL_ID={1} AND HEADER_ID={2}", PO_HEADER_ID, DETAIL_ID,HEADER_ID)) > 0)
                var = true;
            return var;
        }
        public static void CREATELIST(EPM_MASTER_PRODUCTION_ORDERS order)
        {
            DataTable dtDetail = OracleServer.QueryFill(ServiceHelper.DETAILSQL() + " AND PO_HEADER_ID=" + order.PO_HEADER_ID + " ORDER BY DETAIL_TAKIP_NO"); 
            EPM_MASTER_PRODUCTION_H master = OracleServer.Deserialize<EPM_MASTER_PRODUCTION_H>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_H WHERE ID=" + order.HEADER_ID);
            List<EPM_MASTER_PRODUCTION_D> detaiList = OracleServer.DeserializeList<EPM_MASTER_PRODUCTION_D>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE HEADER_ID=" + master.ID);
            List<EPM_PRODUCTION_TRACKING_LIST> uretimList = new List<EPM_PRODUCTION_TRACKING_LIST>();
            foreach (DataRow row in dtDetail.Rows)
            { 
                string HEADER_ID = row["TAKIP_NO"].ToString();
                int DETAIL_ID = row["DETAIL_TAKIP_NO"].IntParse();
                int ADET = detaiList.Sum(ob => ob.QUANTITY);
                DateTime URETIMTARIHI = master.DEADLINE;

                if (ISTHERELIST(order.PO_HEADER_ID, DETAIL_ID,order.HEADER_ID)) return;
                List<ReceteProcessModel> m = OracleServer.DeserializeList<ReceteProcessModel>(@"
                                SELECT RH.ADI,
                                     PI.PROCESS_NAME,
                                     PI.PROCESS_TIME, 
                                     RD.PROCESS_ID,
                                     RD.QUEUE,
                                     RH.ID AS RECETEHEADERID
                                FROM FDEIT005.EPM_PRODUCTION_RECIPE RH
                                     INNER JOIN FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD ON RD.HEADER_ID = RH.ID
                                     INNER JOIN FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI ON PI.ID = RD.PROCESS_ID
                            ORDER BY RECETEHEADERID, QUEUE").FindAll(ob => ob.RECETEHEADERID == master.RECIPE).OrderByDescending(ob => ob.QUEUE).ToList();
                int i = 0;
                foreach (ReceteProcessModel RECETE in m)
                {
                    i++;
                    EPM_PRODUCTION_TRACKING_LIST uret = new EPM_PRODUCTION_TRACKING_LIST();
                    uret.PO_HEADER_ID = order.PO_HEADER_ID;
                    uret.HEADER_ID = order.HEADER_ID;
                    uret.DETAIL_ID = DETAIL_ID;
                    if (i == m.Count) uret.STATUS = 1;
                    uret.PROCESS_ID = RECETE.PROCESS_ID;
                    switch (RECETE.QUEUE)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5: 
                        case 7:
                        case 8:
                            uret.END_DATE = URETIMTARIHI;
                            URETIMTARIHI = URETIMTARIHI.AddBusinessDays(-RECETE.PROCESS_TIME);
                            uret.START_DATE = URETIMTARIHI;
                            break;
                        case 6:
                            uret.END_DATE = URETIMTARIHI;
                            URETIMTARIHI = URETIMTARIHI.AddBusinessDays(-Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(ADET) / Convert.ToDecimal(1000)))));
                            uret.START_DATE = URETIMTARIHI;
                            break;
                        default: break;
                    }
                    uretimList.Add(uret);
                }

            }
            if (uretimList.Count > 0)
                OracleServer.BulkInsert(uretimList);
        }
    }
}
