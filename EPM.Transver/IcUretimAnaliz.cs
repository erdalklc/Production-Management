using System;
using System.Collections.Generic;
using System.Text;
using EPM.Core.Helpers;
using EPM.Core.Managers;
using EPM.Core.Nesneler;
namespace EPM.Transver
{
    public class IcUretimAnaliz
    {
        public void AnalizEt()
        {
            List<EPM_MASTER_PRODUCTION_ORDERS> orders = OracleServer.DeserializeList<EPM_MASTER_PRODUCTION_ORDERS>("SELECT * FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS ORDER BY ID DESC"); 
            Console.WriteLine("Toplam Analiz Veri Sayısı= "+orders.Count); 
            Console.WriteLine("Analiz Başladı !\t" + DateTime.Now.ToString("g"));
            int i = 1;
            foreach (var item in orders)
            {
                Console.WriteLine(i++ + ". Process Analiz Ediliyor.... ID="+item.HEADER_ID);
                ProcessHelper.SETPROCESS(item);
            }
            Console.WriteLine("Analiz Tamamlandı !\t" + DateTime.Now.ToString("g"));
        }
    }
}
