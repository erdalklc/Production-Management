using Dapper.Contrib.Extensions; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_PRODUCTION_TRACKING_LIST")]
    public class EPM_PRODUCTION_TRACKING_LIST
    {
        [Key]
        public long ID { get; set; }
        public DateTime CREATE_DATE { get; set; } = DateTime.Now;
        public int PO_HEADER_ID { get; set; }
        public int HEADER_ID { get; set; }
        public int PROCESS_ID { get; set; } 
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public int STATUS { get; set; }
        public long DETAIL_ID { get; set; }
        public decimal BEKLENEN_MIKTAR { get; set; }
        public decimal GERCEKLESEN_MIKTAR { get; set; }
         
    }
}
