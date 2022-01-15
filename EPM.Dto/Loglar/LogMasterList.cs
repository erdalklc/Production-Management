using EPM.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Loglar
{
    public class LogMasterList : EPM_MASTER_PRODUCTION_H
    {
        public int QUANTITY { get; set; }
        public int BANT_WEEK { get; set; }
        public int BANT_YEAR { get; set; }
        public int PLAN_QUANTITY { get; set; }
        public int KESIM { get; set; }
        public int TASNIF { get; set; }
        public int BANT { get; set; }
        public int KALITE { get; set; }
        public int KALITE_GERCEKLESEN { get; set; }
    }
}
