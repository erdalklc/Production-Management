using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_PRODUCTION_BAND_CAPASITIES")]
    public class EPM_PRODUCTION_BAND_CAPASITIES
    {
        [Key]
        public int ID { get; set; }
        public int BAND_ID { get; set; }
        public int CAPACITY { get; set; }
    }
}
