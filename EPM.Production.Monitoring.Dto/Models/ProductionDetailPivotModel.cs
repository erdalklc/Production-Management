using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Models
{
    public class ProductionDetailPivotModel
    {
        public string ID { get; set; }=Guid.NewGuid().ToString();
        public int WEEK { get;set; }
        public int YEAR { get;set; }    
        public int MARKET_ID { get;set; }
        public string MARKET_NAME { get;set; }
        public string MODEL { get;set; }
        public string COLOR { get;set; }
        public string SEASON_NAME { get;set; }
        public string ORDER_NAME {get;set; }
        public int ORDER_TYPE { get;set; }
        public int ORDER_QUANTITY { get;set; }
        public int QUANTITY { get;set; }
        public int KESIM { get;set; }
        public int TASNIF { get;set; }
        public int BANT { get;set; }
        public int KALITE { get;set; }
        public int KALITE_GERCEKLESEN { get;set; }
    }
}
