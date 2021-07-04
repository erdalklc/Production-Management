using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_PRODUCTION_RECIPE")]
    public class EPM_PRODUCTION_RECIPE
    {
        [Key]
        public int ID { get; set; }
        public string ADI { get; set; }
    }
}
