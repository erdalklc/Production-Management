using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Models
{
    public class ReceteProcessModel
    {
        public string ADI { get; set; }
        public string PROCESS_NAME { get; set; }
        public int PROCESS_TIME { get; set; } 
        public int PROCESS_ID { get; set; }
        public int QUEUE { get; set; }
        public int RECETEHEADERID { get; set; }
    }
}
