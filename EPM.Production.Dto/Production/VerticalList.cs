using EPM.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Dto.Production
{
    public class VerticalList : EPM_MASTER_PRODUCTION_H
    {
        public int DETAIL_ID { get; set; }
        public int QUANTITY { get; set; }
        public int MARKET { get; set; }
        public string PRODUCT_SIZE { get; set; }
    }
}
