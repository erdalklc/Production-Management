using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Models
{
    public class MarketReleasedModel
    {
        public string TYPE { get; set; } 
        public string PAZAR_ADI { get; set; }
        public string SEZON_ADI { get; set; }
        public int MIKTAR { get; set; }
    }
}
