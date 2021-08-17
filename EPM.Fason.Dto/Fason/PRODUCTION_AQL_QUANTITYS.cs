using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_AQL_QUANTITYS
    {
        [Key]
        public int ID { get; set; }
        public int HEADER_ID { get; set; }
        public string PRODUCT_SIZE { get; set; }
        public int QUANTITY { get; set; }
        public int QUANTITY_RELEASE { get; set; }
        public int QUANTITY_SAMPLE { get; set; }
    }
}
