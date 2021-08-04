using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_MASTER_PRODUCTION_D")]
    public class EPM_MASTER_PRODUCTION_D
    {
        [Dapper.Contrib.Extensions.Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "HEADER ID Bilgisi Gereklidir")]
        public int HEADER_ID { get; set; }
        public int INVENTORY_ITEM_ID { get; set; }
        [Required(ErrorMessage = "Market Bilgisi Gereklidir")]
        public int MARKET { get; set; }
        [Required(ErrorMessage = "Beden Bilgisi Gereklidir")]
        public string PRODUCT_SIZE { get; set; }
        [Required(ErrorMessage = "Adet Bilgisi Gereklidir")]
        public int QUANTITY { get; set; }
        public int STATUS { get; set; } = 1;
    }
}
