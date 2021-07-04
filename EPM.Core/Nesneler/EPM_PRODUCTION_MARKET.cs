using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_PRODUCTION_SEASON")]
    public class EPM_PRODUCTION_MARKET
    {
        [Key]
        public int ID { get; set; }
        public string ADI { get; set; }
        public string EGEMEN_ADI { get; set; }
    }
}
