using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_PRODUCT_GROUP")]
    public class EPM_PRODUCT_GROUP
    {
        [Key]
        public int ID { get; set; }
        public string ADI { get; set; }
    }
}
