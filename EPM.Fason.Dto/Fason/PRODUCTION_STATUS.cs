using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_STATUS
    {
        public int STATUS { get; set; }
        public string STATUSEX { get; set; }
        public string PROCESS_NAME { get; set; }
        public int ENTEGRATION_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public DateTime DEADLINE_CUSTOMER { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; } 
    }
}
