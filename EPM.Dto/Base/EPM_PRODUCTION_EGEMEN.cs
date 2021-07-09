using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_PRODUCTION_EGEMEN")]
    public class EPM_PRODUCTION_EGEMEN
    {
        public int TYPE { get; set; }
        public int SEASON { get; set; }
        public int MARKET { get; set; }
        public string FASON_FIRMA { get; set; }
        public int SIPARIS_TIPI { get; set; }
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public string PRODUCT_SIZE { get; set; }
        public string KESIM_FOYU_NO { get; set; }
        public int DEMET { get; set; }
        public int KAYIP { get; set; }
        public int BIRINCI_KALITE { get; set; }
        public int IKINCI_KALITE { get; set; }
    }
}
