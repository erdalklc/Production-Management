using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_LIST_V
    {
        public int DETAIL_ID { get; set; }
        public int HEADER_ID { get; set; }
        public int ENTEGRATION_ID { get; set; }
        public string PROCESS_NAME { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public int STATUS { get; set; }
        public string STATUS_EX { get; set; }
        public string BUTTON { get; set; }
        public int QUEUE { get; set; }
        public int HANDLE_SIDE
    }
}
