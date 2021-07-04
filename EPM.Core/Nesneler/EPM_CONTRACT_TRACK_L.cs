using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_CONTRACT_TRACK_L")]
    public class EPM_CONTRACT_TRACK_L
    {
        public int ID { get; set; }
        public int HEADER_ID { get; set; }
        public int PROCESS_ID { get; set; }
        public int QUEUE { get; set; }
        public int STATUS { get; set; }
        public DateTime PLANNING_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public DateTime START_DATE { get; set; }
    }
}
