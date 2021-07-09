﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{ 
    [Table("FDEIT005.EPM_PRODUCTION_BAND_GROUP")]
    public class EPM_PRODUCTION_BAND_GROUP
    {
        [Key]
        public int ID { get; set; }
        public string ADI { get; set; }
    }
}
