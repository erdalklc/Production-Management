using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    [Table("FDEIT005.EPM_CREATE_PO_TEMP")]
    public class EPM_CREATE_PO_TEMP
    {
        public string USER_CODE { get; set; }
        public string SIPARIS_NO { get; set; }
        public string KALEM_KODU { get; set; }
        public decimal ADET { get; set; }
        public decimal BIRIM_FIYAT { get; set; }
        public string SEZON { get; set; }
        public string MASTER_SIPARIS { get; set; }
        public DateTime SATIR_TERMIN { get; set; }
    }
}
