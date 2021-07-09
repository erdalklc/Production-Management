using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_USER_FABRIC_TYPES")]
    public class EPM_USER_FABRIC_TYPES
    {
        [Key]
        public int ID { get; set; }
        public int FABRIC_TYPE_ID { get; set; } 
        public string USER_CODE { get; set; }
    }
}
