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
        public static object[] FasonSiparisOlustur(PRODUCTION_HEADER header, List<PRODUCTION_PROCESS> plan, int firmaBilgi, DateTime terminTarihi)
        {
            CREATEORDER order = new CREATEORDER();
            order.header = header;
            order.plan = plan;
            order.firma = firmaBilgi;
            order.termin = terminTarihi;

            object[] sonuc = PostRequest<CREATEORDER, object[]>(EPMServiceType.FasonTakip, "CreateOrder", order);
            return sonuc;
        }
    }
}
