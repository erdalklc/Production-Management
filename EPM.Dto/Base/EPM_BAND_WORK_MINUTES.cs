using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_BAND_WORK_MINUTES")]
    public class EPM_BAND_WORK_MINUTES
    {
        [Key]
        public int ID { get; set; }
        public int BAND_ID { get; set; }
        public int YEAR { get; set; }
        public int WEEK { get; set; }
        public int WORK_MINUTE { get; set; }
    }
}
