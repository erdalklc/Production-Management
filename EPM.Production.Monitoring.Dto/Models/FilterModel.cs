using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Models
{
    public class FilterModel
    {
        public int BRAND { get; set; } = 1;
        public int SEASON { get; set; } = 2;
        public int ORDER_TYPE { get; set; } = 0;
        public int BAND { get; set; } = 0;
        public int PRODUCT_GROUP { get; set; } = 0;
        public string MODEL { get; set; }
        public string COLOR { get; set; }
    }
}
