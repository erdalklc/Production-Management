using EPM.Dto.Base;
using EPM.Plan.Dto.Plan;
using EPM.Tools.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.ServiceHelper
{
    public class PlanServiceHelper: EPMHttpCaller
    {
        public static object GetPlan(string USER_CODE, int BRAND, List<EPM_PRODUCTION_SEASON> SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE, int PLAN_DURUM, List<string> YEAR)
        {
            string apiUrl = "GetPlan";
            var list = PostRequest<object[], object>(EPMServiceType.Plan, apiUrl, new object[] { USER_CODE
                , BRAND,JsonConvert.SerializeObject( SEASON),MODEL,COLOR,ORDER_TYPE,PRODUCTION_TYPE,FABRIC_TYPE,PLAN_DURUM,JsonConvert.SerializeObject( YEAR) });
            return list;
        }
        public static object GetPlanBandGiris(string USER_CODE, int BRAND, List<EPM_PRODUCTION_SEASON> SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE, int PLAN_DURUM, List<string> YEAR)
        {
            string apiUrl = "GetPlanBandGiris";
            var list = PostRequest<object[], object>(EPMServiceType.Plan, apiUrl, new object[] { USER_CODE
                , BRAND,JsonConvert.SerializeObject( SEASON) ,MODEL,COLOR,ORDER_TYPE,PRODUCTION_TYPE,FABRIC_TYPE,PLAN_DURUM,JsonConvert.SerializeObject( YEAR) });
            return list;
        }
        public static object GetPlanByChart(KapasiyeUyumChart_Filter filter)
        {
            string apiUrl = "GetPlanByChart";
            var list = PostRequest<KapasiyeUyumChart_Filter, object>(EPMServiceType.Plan, apiUrl, filter);
            return list;
        }

        public static object GetPlanByChartBandGiris(KapasiyeUyumChart_Filter filter)
        {
            string apiUrl = "GetPlanByChartBandGiris";
            var list = PostRequest<KapasiyeUyumChart_Filter, object>(EPMServiceType.Plan, apiUrl, filter);
            return list;
        }
        public static object UpdateInsertPlan(string USER_CODE, JObject obj)
        {
            string apiUrl = "UpdateInsertPlan";
            var list = PostRequest<object[], object>(EPMServiceType.Plan, apiUrl, new object[] { USER_CODE , obj });
            return list;
        }
        public static object UpdateInsertPlanBandGiris(string USER_CODE, JObject obj)
        {
            string apiUrl = "UpdateInsertPlanBandGiris";
            var list = PostRequest<object[], object>(EPMServiceType.Plan, apiUrl, new object[] { USER_CODE, obj });
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

        public static List<EPM_PRODUCT_GROUP> GetProductGroupList(int BAND_GROUP)
        {
            string apiUrl = "GetProductGroupList";
            var list = PostRequest<object[], List<EPM_PRODUCT_GROUP>>(EPMServiceType.Plan, apiUrl, new object[] { BAND_GROUP });
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
        public static List<KapasitePlanUyum> GetYuklemeUyumList(int YEAR, int BAND_GROUP)
        {
            string apiUrl = "GetYuklemeUyumList";
            var list = PostRequest<object[], List<KapasitePlanUyum>>(EPMServiceType.Plan, apiUrl, new object[] { YEAR
                , BAND_GROUP });
            return list;
        }
        public static List<ModelSureleriView> ProductionTimesLoad()
        {
            string apiUrl = "ProductionTimesLoad";
            var list = GetRequest<List<ModelSureleriView>>(EPMServiceType.Plan, apiUrl);
            return list;
        }

        public static List<KapasitePlanPerformans> GetKapasitePerformansList(int YEAR, int BAND_GROUP)
        {
            string apiUrl = "GetKapasitePerformansList";
            var list = PostRequest<object[], List<KapasitePlanPerformans>>(EPMServiceType.Plan, apiUrl, new object[] { YEAR
                , BAND_GROUP });
            return list;
        }
        public static List<KapasitePlanPerformans> GetYuklemePerformansList(int YEAR, int BAND_GROUP)
        {
            string apiUrl = "GetYuklemePerformansList";
            var list = PostRequest<object[], List<KapasitePlanPerformans>>(EPMServiceType.Plan, apiUrl, new object[] { YEAR
                , BAND_GROUP });
            return list;
        }
        public static List<EpmBandWorkModel> GetBandWorkers(int YEAR, int BAND_GROUP,int PRODUCT_GROUP)
        {
            string apiUrl = "GetBandWorkers";
            var list = PostRequest<object[], List<EpmBandWorkModel>>(EPMServiceType.Plan, apiUrl, new object[] { YEAR
                , BAND_GROUP ,PRODUCT_GROUP});
            return list;
        }
        public static List<EpmBandWorkMinuteModel> GetBandWorkMinutes(int YEAR, int BAND_GROUP)
        {
            string apiUrl = "GetBandWorkMinutes";
            var list = PostRequest<object[], List<EpmBandWorkMinuteModel>>(EPMServiceType.Plan, apiUrl, new object[] { YEAR
                , BAND_GROUP });
            return list;
        }


        public static List<PlanStatus> GetPlanStatusList(bool hepsi = true)
        {
            string apiUrl = "GetPlanStatusList";
            var list = PostRequest<object[], List<PlanStatus>>(EPMServiceType.Plan, apiUrl, new object[] { hepsi });
            return list;
        }

        public static object GetUretimGerceklesenByChart(KapasiyeUyumChart_Filter filter)
        {
            string apiUrl = "GetUretimGerceklesenByChart";
            var list = PostRequest<KapasiyeUyumChart_Filter, object>(EPMServiceType.Plan, apiUrl, filter);
            return list;
        }

        public static object GeKapasiteListByChart(KapasiyeUyumChart_Filter filter)
        {
            string apiUrl = "GeKapasiteListByChart";
            var list = PostRequest<KapasiyeUyumChart_Filter, object>(EPMServiceType.Plan, apiUrl, filter);
            return list;
        }
        public static object GeKapasiteListByChartBandGiris(KapasiyeUyumChart_Filter filter)
        {
            string apiUrl = "GeKapasiteListByChartBandGiris";
            var list = PostRequest<KapasiyeUyumChart_Filter, object>(EPMServiceType.Plan, apiUrl, filter);
            return list;
        }

        public static bool UretimSureleriYenile()
        {
            string apiUrl = "UretimSureleriYenile";
            var list = GetRequest<bool>(EPMServiceType.Plan, apiUrl);
            return list;
        }
    }
}
