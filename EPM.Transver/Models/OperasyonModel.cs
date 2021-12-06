using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Transver.Models
{
    [Table("FDEIT005.EPM_OPERATION_QUANTITYS")]
    public class OperasyonModel
    {
        public string MODEL_ADI { get; set; }
        public string RENK_ADI { get; set; }
        public string BEDEN_ADI { get; set; }
        public string SEZON_ADI { get; set; }
        public string PAZAR_ADI { get; set; }
        public string SIPARIS_TIPI { get; set; }
        public string KESIM_FOYU_NO { get; set; }
        public string KESIM_FOYU_NOT { get; set; }
        public int START_WEEK { get; set; }
        public int START_YEAR { get; set; }
        public int OPERASYON_ID { get; set; }
        public int MIKTAR { get; set; }
    }
}
