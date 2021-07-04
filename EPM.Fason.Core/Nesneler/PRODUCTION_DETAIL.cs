using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPM.Fason.Core.Nesneler
{ 
    public class PRODUCTION_DETAIL
    { 
        public int ENTEGRATION_ID { get; set; }
        [Required(ErrorMessage = "HEADER ID Bilgisi Gereklidir")] 
        public int ENTEGRATION_HEADER_ID { get; set; }
        public int INVENTORY_ITEM_ID { get; set; }
        [Required(ErrorMessage = "Market Bilgisi Gereklidir")]
        public int MARKET { get; set; }
        [Required(ErrorMessage = "Beden Bilgisi Gereklidir")]
        public string PRODUCT_SIZE { get; set; }
        [Required(ErrorMessage = "Adet Bilgisi Gereklidir")]
        public int QUANTITY { get; set; }
    }
}
