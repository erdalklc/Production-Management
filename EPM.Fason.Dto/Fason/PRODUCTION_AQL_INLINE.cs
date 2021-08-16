using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_AQL_INLINE
    {
        [Key]
        public int ID { get; set; }
        public int HEADER_ID { get; set; }
        public int USER_ID { get; set; }
        public int PROCESS_ID { get; set; }
        public string DESCRIPTION { get; set; }
    }
}
