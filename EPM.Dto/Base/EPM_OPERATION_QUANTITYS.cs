using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_OPERATION_QUANTITYS")]
    public class EPM_OPERATION_QUANTITYS
    {
        public string MODEL_ADI { get; set; }
        public string RENK_ADI { get; set; }
        public string SEZON_ADI { get; set; }
        public int OPERASYON_ID { get; set; }
        public int MIKTAR { get; set; }
    }
}
