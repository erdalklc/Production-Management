using EPM.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Models
{
    public class PlanModel : EPM_PRODUCTION_PLAN
    {
        public string MARKET_NAME { get; set; }
        public int ORDER_QUANTITY { get; set; }
    }
}
