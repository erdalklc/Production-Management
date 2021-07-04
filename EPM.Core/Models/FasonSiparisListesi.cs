using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Core.Models
{
    public class FasonSiparisListesi
    {
        public int TAKIP_NO { get; set; }
        public string MODEL { get; set; }
        public string RENK { get; set; }
        public string SEZON { get; set; }
        public string USERNAME { get; set; }
        public string FIRMA_ADI { get; set; }
        public int ADET { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime TAKE_DATE { get; set; }
        public int SIPARISDURUMU { get; set; }
        public string DEVAMEDEN { get; set; }
    }
}
