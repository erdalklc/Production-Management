using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class SIPARIS_LISTESI_DETAIL
    {
        public string ID { get; set; } = Guid.NewGuid().ToString(); 
        public string PRODUCT_SIZE { get; set; }
        public int QUANTITY { get; set; }
    }
}
