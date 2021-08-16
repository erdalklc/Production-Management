using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_AQL_QUESTION_GROUPS
    {
        [Key]
        public int ID { get; set; }
        public string GROUP_NAME { get; set; }
    }
}
