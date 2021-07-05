using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Core.Models
{
    public class PRODUCTION_STATUS
    {
        public int HEADER_ID { get; set; }
        public string LAST_STATE { get; set; }
        public DateTime MUST_STATE_DATE { get; set; }
        public string MUST_STATE { get; set; }
    }
}
