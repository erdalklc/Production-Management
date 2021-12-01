using EPM.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Plan.Dto.Plan
{
    public  class EpmBandWorkMinuteModel : EPM_BAND_WORK_MINUTES
    {
        public string ROW_ID { get; set; } = Guid.NewGuid().ToString();
    }
}
