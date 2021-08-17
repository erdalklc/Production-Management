using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_AQL_HEADER
    {
        [Key]
        public int ID { get; set; }
        public int ENTEGRATION_HEADER_ID { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string DESCRIPTION { get; set; }
        public int USER_ID { get; set; }
        public int STATUS { get; set; }
    }
}
