using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPM.Fason.Dto.Fason
{ 
    public class PRODUCTION_DETAIL
    { 
        public int ENTEGRATION_ID { get; set; } 
        public int ENTEGRATION_HEADER_ID { get; set; }
        public int INVENTORY_ITEM_ID { get; set; } 
        public string MARKET { get; set; } 
        public string PRODUCT_SIZE { get; set; } 
        public int QUANTITY { get; set; }
    }
}
