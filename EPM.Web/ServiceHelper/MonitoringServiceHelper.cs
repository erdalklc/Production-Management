using EPM.Production.Monitoring.Dto.Models;
using EPM.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.ServiceHelper
{
    public class MonitoringServiceHelper : EPMHttpCaller
    {
        public static List<HaftaModel> GetTerminList(FilterModel model)
        {
            string apiUrl = "GetTerminList";
            var list = PostRequest<FilterModel, List<HaftaModel>>(EPMServiceType.Monitoring, apiUrl, model);
            return list;
        }
    }
}
