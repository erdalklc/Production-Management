using EPM.Core.FormModels.UretimPlan;
using EPM.Core.Nesneler;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text; 

namespace EPM.Core.Services
{
    public interface IUretimPlanService
    {
        public object GetPlan(HttpContext context, int BRAND, int SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE);

        public object UpdateInsertPlan(HttpContext context,JObject obj);

        public IEnumerable<EPM_PRODUCTION_BAND_CAPASITIES> GetKapasitePlanList();

        public IEnumerable<KapasitePlanListChart> GetKapasitePlanListChart();

        public object[] KapasitePlanListUpdate(int Key, string Values);

        public IEnumerable<KapasitePlanUyum> GetKapasiteUyumList(int YEAR,int BAND_GROUP);
    }
}
