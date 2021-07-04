using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_MASTER_PRODUCTION_ORDERS")]
    public class EPM_MASTER_PRODUCTION_ORDERS
    {
        [Key]
        public int ID { get; set; }
        public int STATUS { get; set; }
        public int HEADER_ID { get; set; }
        public string TAKIP_NO { get; set; } 
        public int PO_HEADER_ID { get; set; }
    }
}
