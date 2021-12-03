using EPM.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Plan.Dto.Plan
{
    public class EpmBandWorkModel : EPM_BAND_WORKERS
    {
        public string ROW_ID { get; set; } = Guid.NewGuid().ToString();
        public string PRODUCT_GROUP_NAME { get; set; }
    }
}
