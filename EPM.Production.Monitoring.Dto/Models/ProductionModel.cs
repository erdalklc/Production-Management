using EPM.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Models
{
    public class ProductionModel : EPM_MASTER_PRODUCTION_H
    {
        public string PROCESS_INFO { get; set; }
        public int QUANTITY { get; set; }
        public string COMPANY_NAME { get; set; }
    }
}
