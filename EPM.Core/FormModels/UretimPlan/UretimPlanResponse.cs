using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.UretimPlan
{
    public class UretimPlanResponse
    {
        public bool isOK { get; set; }
        public string errorText { get; set; }
        public object oldValue { get; set; }
    }
}
