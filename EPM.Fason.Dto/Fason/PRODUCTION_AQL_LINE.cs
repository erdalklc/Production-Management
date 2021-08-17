using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_AQL_LINE
    {
        [Key]
        public int ID { get; set; } 
        public int HEADER_ID { get; set; }
        public int QUESTION_ID { get; set; }
        public int MINOR_QUANTITY { get; set; }
        public int MAJOR_QUANTITY { get; set; }
        public int TOTAL_QUANTITY { get; set; }
        public int TOTAL_QUANTITY_RELEASE { get; set; }
        public string BEDEN { get; set; } 
    }
}
