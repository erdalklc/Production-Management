using EPM.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.Uretim
{
    public class VerticalList : EPM_MASTER_PRODUCTION_H
    {
        public int DETAIL_ID { get; set; }
        public int QUANTITY { get; set; }
        public int MARKET { get; set; }
        public string PRODUCT_SIZE { get; set; }
    }
}
