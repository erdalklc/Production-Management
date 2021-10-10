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
        public int HEADER_ID { get; set; }
    }
}
