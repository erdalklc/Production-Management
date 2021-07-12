using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_TRACKING_PROCESS_VALUES")]
    public class EPM_TRACKING_PROCESS_VALUES
    {
        [Key]
        public int ID { get; set; }
        public int HEADER_ID { get; set; }
        public int BANT { get; set; }
        public int KALITE { get; set; }
        public int KESIM { get; set; }
        public int TASNIF { get; set; }
    }
}
