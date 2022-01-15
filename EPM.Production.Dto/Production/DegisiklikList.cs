using EPM.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Dto.Production
{
    public class DegisiklikList : EPM_MASTER_PRODUCTION_H
    {
        public int QUANTITY { get; set; }
        public string COLUMN_NAME { get; set; }
        public string USER { get; set; }
        public object OLD_VALUE { get; set; }
        public object NEW_VALUE { get; set; }
    }
}
