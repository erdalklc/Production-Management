using EPM.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.Uretim
{
    public class DetailList : EPM_MASTER_PRODUCTION_D
    {
        public decimal EXPECTED_COST { get; set; } = 0;
        public int CURRENCY_UNIT { get; set; } = 2;
        public decimal REALESED_COST { get; set; } = 0;
    }
}
