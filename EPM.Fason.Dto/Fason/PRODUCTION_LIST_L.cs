using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_LIST_L
    {
        [Key]
        public int ID { get; set; }
        public int HEADER_ID { get; set; }
        public int STATUS { get; set; }
        public int PROCESS_ID { get; set; }
        public int QUEUE { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
    }
}
