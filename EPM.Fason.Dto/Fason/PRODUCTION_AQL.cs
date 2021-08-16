using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_AQL
    {
        [Key]
        public int ID { get; set; }
        public int TYPE { get; set; }
        public int HEADER_ID { get; set; }
        public int QUESTION_ID { get; set; }
        public int MINOR_QUANTITY { get; set; }
        public int MAJOR_QUANTITY { get; set; }
        public string BEDEN { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public int USER_ID { get; set; }
    }
}
