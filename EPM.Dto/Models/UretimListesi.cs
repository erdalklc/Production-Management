using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Dto.Models
{
    public class UretimListesi
    {
        public int TAKIP_NO { get; set; }
        public string MODEL { get; set; }
        public string RENK { get; set; }
        public string SEZON { get; set; }
        public string RECETE { get; set; }
        public int ADET { get; set; }
        public DateTime TARIH { get; set; } 
        public string OLUSTURAN { get; set; }
        public string FIRMA_ADI { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public string PROCESSNAME { get; set; }
        public string OLMASIGEREKEN { get; set; }
        public string PROCESSINFO { get; set; }
        public int SIPARISDURUMU { get; set; }
        public string DEVAMEDEN { get; set; }
    }
}
