using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.UretimTakip
{
    public class EgemenOrmeSiparis
    {
        public string GUID { get{ return Guid.NewGuid().ToString(); } set { GUID = value; } }
        public string URUN_TIPI_ADI { get; set; }
        public string URUN_ADI { get; set; }
        public string RENK_ADI { get; set; }
        public DateTime TARIH { get; set; }
        public string HAREKET_ADI { get; set; }
        public string ISLEM_TIPI_ADI { get; set; }
        public string FIRMA_ADI { get; set; }
        public string KULLANICI_ADI { get; set; }
        public string ISLEM_ADI { get; set; }
        public decimal MIKTAR { get; set; }
        public string BIRIMID { get; set; }
    }
}
