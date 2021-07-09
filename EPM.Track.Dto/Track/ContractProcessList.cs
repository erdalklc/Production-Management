using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Dto.Track
{
    public class ContractProcessList
    {
        public int ID { get; set; }
        public bool ISACTIVE { get; set; }
        public int PROCESS_ID { get; set; }
        public string PROCESS_NAME { get; set; } 
        public int PROCESS_TIME { get; set; }
        public DateTime END_DATE { get; set; }
    }
}
