using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.FasonTakip
{
    public class PRODUCTION_HEADER
    {
        public int ENTEGRATION_ID { get; set; }
        public string BRAND { get; set; }
        public string SUB_BRAND { get; set; }
        public string SEASON { get; set; }
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public string PRODUCT_GROUP { get; set; }
        public string FABRIC_TYPE { get; set; }
        public string PRODUCTION_TYPE { get; set; }
        public string ORDER_TYPE { get; set; }
        public string RECIPE { get; set; }
        public DateTime DEADLINE { get; set; }
        public string ATTRIBUTE1 { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }
        public string ATTRIBUTE6 { get; set; }
        public string ATTRIBUTE7 { get; set; }
        public string ATTRIBUTE8 { get; set; }
        public string ATTRIBUTE9 { get; set; }
        public string ATTRIBUTE10 { get; set; }
        public string TEMA { get; set; }
        public string ANA_TEMA { get; set; }
        public string ROYALTY { get; set; }
        public string COLLECTION_TYPE { get; set; } 
        public DateTime CREATE_DATE { get; set; }
        public List<PRODUCTION_DETAIL> DETAIL { get; set; }
    }
}
