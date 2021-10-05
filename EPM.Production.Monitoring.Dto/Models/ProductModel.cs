using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Models
{
    public class ProductModel
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public string MARKET { get; set; }
    }
}
