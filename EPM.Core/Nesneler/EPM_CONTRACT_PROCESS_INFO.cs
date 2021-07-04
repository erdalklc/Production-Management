using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_CONTRACT_PROCESS_INFO")]
    public class EPM_CONTRACT_PROCESS_INFO
    {
        [Key]
        public int ID { get; set; }
        public int STATUS { get; set; }
        public string PROCESS_NAME { get; set; }
        public int PROCESS_TIME { get; set; }
    }
}
