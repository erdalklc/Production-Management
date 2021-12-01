using EPM.Dto.Base;
using EPM.Plan.Dto.Plan;
using EPM.Tools.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.ServiceHelper
{
    public class PlanServiceHelper: EPMHttpCaller
    {
        public static object GetPlan(string USER_CODE, int BRAND, int SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE)
        {
            string apiUrl = "GetPlan";
            var list = PostRequest<object[], object>(EPMServiceType.Plan, apiUrl, new object[] { USER_CODE
                , BRAND,SEASON,MODEL,COLOR,ORDER_TYPE,PRODUCTION_TYPE,FABRIC_TYPE });
            return list;
        }
        public static object GetPlanByChart(KapasiyeUyumChart_Filter filter)
        {
            string apiUrl = "GetPlanByChart";
            var list = PostRequest<KapasiyeUyumChart_Filter, object>(EPMServiceType.Plan, apiUrl, filter);
            return list;
        }
        public static object UpdateInsertPlan(string USER_CODE, JObject obj)
        {
            string apiUrl = "UpdateInsertPlan";
            var list = PostRequest<object[], object>(EPMServiceType.Plan, apiUrl, new object[] { USER_CODE , obj });
            return list;
        }

        public static List<EPM_PRODUCTION_BAND_CAPASITIES> GetKapasitePlanList()
        {
            string apiUrl = "GetKapasitePlanList";
            var list = GetRequest<List<EPM_PRODUCTION_BAND_CAPASITIES>>(EPMServiceType.Plan, apiUrl);
            return list;
        }

        public static List<KapasitePlanListChart> GetKapasitePlanListChart()
        {
            string apiUrl = "GetKapasitePlanListChart";
            var list = GetRequest<List<KapasitePlanListChart>>(EPMServiceType.Plan, apiUrl);
            return list;
        }

        public static object[] KapasitePlanListUpdate(int Key, string Values)
        {
            string apiUrl = "KapasitePlanListUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Plan, apiUrl, new object[] { Key
                , Values });
            return list;
        }
        public static object[] BandWorkersUpdate(int Key, string Values)
        {
            string apiUrl = "BandWorkersUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Plan, apiUrl, new object[] { Key
                , Values });
            return list;
        }
        public static object[] BandWorkMinutesUpdate(int Key, string Values)
        {
            string apiUrl = "BandWorkMinutesUpdate";
            var list = PostRequest<object[], object[]>(EPMServiceType.Plan, apiUrl, new object[] { Key
                , Values });
            return list;
        }

        public static List<KapasitePlanUyum> GetKapasiteUyumList(int YEAR, int BAND_GROUP)
        {
            string apiUrl = "GetKapasiteUyumList";
            var list = PostRequest<object[], List<KapasitePlanUyum>>(EPMServiceType.Plan, apiUrl, new object[] { YEAR
                , BAND_GROUP });
            return list;
        }
        public static List<KapasitePlanPerformans> GetKapasitePerformansList(int YEAR, int BAND_GROUP)
        {
            string apiUrl = "GetKapasitePerformansList";
            var list = PostRequest<object[], List<KapasitePlanPerformans>>(EPMServiceType.Plan, apiUrl, new object[] { YEAR
                , BAND_GROUP });
            return list;
        }

        public static List<EpmBandWorkModel> GetBandWorkers(int YEAR, int BAND_GROUP)
        {
            string apiUrl = "GetBandWorkers";
            var list = PostRequest<object[], List<EpmBandWorkModel>>(EPMServiceType.Plan, apiUrl, new object[] { YEAR
                , BAND_GROUP });
            return list;
        }
        public static List<EpmBandWorkMinuteModel> GetBandWorkMinutes(int YEAR, int BAND_GROUP)
        {
            string apiUrl = "GetBandWorkMinutes";
            var list = PostRequest<object[], List<EpmBandWorkMinuteModel>>(EPMServiceType.Plan, apiUrl, new object[] { YEAR
                , BAND_GROUP });
            return list;
        }

    }
}
