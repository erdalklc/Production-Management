using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_USER_PRODUCTION_TYPES")]
    public class EPM_USER_PRODUCTION_TYPES
    {
        [Key]
        public int ID { get; set; }
        public int PRODUCTION_TYPE_ID { get; set; }
        public string USER_CODE { get; set; }
    }
}
