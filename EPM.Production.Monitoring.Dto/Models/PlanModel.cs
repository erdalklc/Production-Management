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
        public int RELASED_QUANTITY { get; set; }
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public string SEASON_NAME { get; set; }
        public int KALITE_GERCEKLESEN { get; set; }
        public int KALITE { get; set; }
        public int BANT { get; set; }
        public int TASNIF { get; set; }
        public int KESIM { get; set; }
    }
}
