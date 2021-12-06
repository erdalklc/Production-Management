using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Models
{
    public class ProductionDetailModel
    {
        public string MODEL { get; set; }
        public string RENK { get; set; }
        public string BEDEN { get; set; }
        public string PAZAR { get; set; }
        public string SEZON { get; set; }
        public string SIPARIS_TIPI { get; set; }
        public string MIKTAR { get; set; }
        public int START_WEEK { get; set; } 
        public int START_YEAR { get; set; }   
        public string IKINCI_KALITE { get; set; }
        public string KESIM_FOYU_NOT { get; set; }
        public string KESIM_FOYU_NO { get; set; }
    }
}
