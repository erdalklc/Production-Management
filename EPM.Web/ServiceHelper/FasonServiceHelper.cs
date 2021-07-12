using EPM.Fason.Dto.Fason;
using EPM.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.ServiceHelper
{
    public class FasonServiceHelper : EPMHttpCaller
    {
        public static Tuple<PRODUCTION_HEADER, List<PRODUCTION_PROCESS>, List<PRODUCTION_FASON_USERS>> SiparisListesiGetir(int HEADER_ID)
        {
            PRODUCTION_HEADER header = PostRequest<object[], PRODUCTION_HEADER>(EPMServiceType.Track, "GetProductionList", new object[] { HEADER_ID });
            List<PRODUCTION_PROCESS> processList = GetRequest<List<PRODUCTION_PROCESS>>(EPMServiceType.Fason, "GetProcessList");
            List<PRODUCTION_FASON_USERS> fasonUsers = GetRequest<List<PRODUCTION_FASON_USERS>>(EPMServiceType.Fason, "GetFasonUsers"); 

            return new Tuple<PRODUCTION_HEADER, List<PRODUCTION_PROCESS>, List<PRODUCTION_FASON_USERS>>(header, processList, fasonUsers);

        }
        public static List<PRODUCTION_LIST_V> FasonTakipListeAyarla(int HEADER_ID)
        {   
            return PostRequest<object[], List<PRODUCTION_LIST_V>>(EPMServiceType.Fason, "GetProcessStatusList", new object[] { HEADER_ID });

        }
        public static object[] FasonSiparisOlustur(PRODUCTION_HEADER header, List<PRODUCTION_PROCESS> plan, int firmaBilgi, DateTime terminTarihi)
        {
            CREATEORDER order = new CREATEORDER();
            order.header = header;
            order.plan = plan;
            order.firma = firmaBilgi;
            order.termin = terminTarihi;

            object[] sonuc = PostRequest<CREATEORDER, object[]>(EPMServiceType.Fason, "CreateOrder", order);
            return sonuc;
        }
    }
}
