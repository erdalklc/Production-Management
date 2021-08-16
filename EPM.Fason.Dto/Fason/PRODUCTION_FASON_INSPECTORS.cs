using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_FASON_INSPECTORS
    {
        [Key]
        public int ID { get; set; }
        public int STATUS { get; set; }
        public string USER_CODE { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
    }
}
