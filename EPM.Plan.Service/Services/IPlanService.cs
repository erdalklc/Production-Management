using EPM.Dto.Base;
using EPM.Plan.Dto.Plan;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Plan.Service.Services
{
    public interface IPlanService
    {
        public object GetPlan(string USER_CODE, int BRAND, int SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE);

        public object UpdateInsertPlan(string USER_CODE, JObject obj);

        public List<EPM_PRODUCTION_BAND_CAPASITIES> GetKapasitePlanList();

        public List<KapasitePlanListChart> GetKapasitePlanListChart();

        public object[] KapasitePlanListUpdate(int Key, string Values);
        public object[] BandWorkersUpdate(int Key, string Values);
        public object[] BandWorkMinutesUpdate(int Key, string Values);

        public List<KapasitePlanUyum> GetKapasiteUyumList(int YEAR, int BAND_GROUP);
        public List<KapasitePlanPerformans> GetKapasitePerformansList(int YEAR, int BAND_GROUP);
        public object GetPlanByChart(KapasiyeUyumChart_Filter filter);

        public List<EpmBandWorkModel> GetBandWorkers(int YEAR, int BAND_GROUP);
        public List<EpmBandWorkMinuteModel> GetBandWorkMinutes(int YEAR, int BAND_GROUP);

    }
}
