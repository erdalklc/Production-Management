using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL")]
    public class EPM_PRODUCTION_RECIPE_DETAIL
    {
        [Key]
        public int ID { get; set; }
        public int HEADER_ID { get; set; }
        public int PROCESS_ID { get; set; }
        public int QUEUE { get; set; }
    }
}
