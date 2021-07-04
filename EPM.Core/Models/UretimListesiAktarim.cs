using EPM.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Models
{
    public class UretimListesiAktarim
    { 
        public int BRAND { get; set; }
        public int SUB_BRAND { get; set; }
        public int SEASON { get; set; }
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public int PRODUCT_GROUP { get; set; }
        public int FABRIC_TYPE { get; set; }
        public int PRODUCTION_TYPE { get; set; }
        public int ORDER_TYPE { get; set; }
        public int RECIPE { get; set; }
        public int BAND_ID { get; set; }
        public DateTime DEADLINE { get; set; }
        public DateTime SHIPMENT_DATE { get; set; }
        public bool APPROVAL_STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public int STATUS { get; set; }
        public int HEADER_ID { get; set; }
        public int INVENTORY_ITEM_ID { get; set; }
        public int MARKET { get; set; }
        public string PRODUCT_SIZE { get; set; }
        public int QUANTITY { get; set; }
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
        public int COLLECTION_TYPE { get; set; }
    }
}
