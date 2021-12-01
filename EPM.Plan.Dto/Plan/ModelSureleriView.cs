using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Plan.Dto.Plan
{
    public class ModelSureleriView : ModelSureleri
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
    }
}
