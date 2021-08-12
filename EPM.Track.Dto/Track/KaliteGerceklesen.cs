using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Dto.Track
{
    public class KaliteGerceklesen
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string BRAND_NAME { get; set; }
        public string SUB_BRAND_NAME { get; set; }
        public string SEASON_NAME { get; set; }
        public string MARKET_NAME { get; set; }
        public string ORDER_TYPE { get; set; }
        public string EXTERNAL_COMPANY { get; set; }
        public string KESIM_FOYU_NO { get; set; }
        public string PRODUCT_GROUP { get; set; }
        public string FABRIC_TYPE { get; set; }
        public string PRODUCTION_TYPE { get; set; }
        public string RECIPE { get; set; }
        public string COLLECTION { get; set; }
        public string TEMA { get; set; }
        public string ANA_TEMA { get; set; }
        public string ROYALTY { get; set; }
        public DateTime DEADLINE { get; set; } 
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public string PRODUCT_SIZE { get; set; }
        public int PLANNED { get; set; }
        public int RELEASED { get; set; }
    }
}
