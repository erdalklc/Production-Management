using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_PRODUCTION_PLAN")]
    public class EPM_PRODUCTION_PLAN
    {
        [Key]
        public int ID { get; set; }
        public string CREATED_BY { get; set; }
        public int HEADER_ID { get; set; }
        public int MARKET_ID { get; set; }
        public int WEEK { get; set; }
        public int YEAR { get; set; }
        public int QUANTITY { get; set; }
    }
}
