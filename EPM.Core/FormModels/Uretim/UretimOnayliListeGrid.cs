using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.Uretim
{
    public class UretimOnayliListeGrid
    {
        public int ID { get; set; }
        public string BRAND { get; set; }
        public string SEASON { get; set; }
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public string PRODUCT_GROUP { get; set; }
        public string FABRIC_TYPE { get; set; }
        public string PRODUCTION_TYPE { get; set; }
        public string ORDER_TYPE { get; set; } 
        public string RECIPE { get; set; }
        public string BAND_NAME { get; set; }
        public string COLLECTION_TYPE { get; set; }
        public string ROYALTY { get; set; }
        public string TEMA { get; set; }
        public string ANA_TEMA { get; set; }
        public DateTime DEADLINE { get; set; }
        public DateTime SHIPMENT_DATE { get; set; }
        public int QUANTITY { get; set; }
    }
}
