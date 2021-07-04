using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_USER_RESPONSIBILITY")]
    public class EPM_USER_RESPONSIBILITY
    {
        [Key]
        public int ID { get; set; }
        public int RESPONSIBILITY_ID { get; set; } 
        public string USER_CODE { get; set; }
    }
}
