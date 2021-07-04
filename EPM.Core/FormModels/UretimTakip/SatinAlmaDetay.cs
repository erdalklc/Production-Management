using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.UretimTakip
{
    public class SatinAlmaDetay
    {
        public int PO_HEADER_ID { get; set; }
        public int HEADER_ID { get; set; }
        public string TAKIP_NO { get; set; }
        public int DETAIL_TAKIP_NO { get; set; }
        public string AGENT_NAME { get; set; }
        public string FIRMA { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public int ITEM_ID { get; set; }
        public string URUN { get; set; }
        public string KALEM { get; set; }
        public string MODELDETAY { get; set; }
        public string RENKDETAY { get; set; }
        public string BIRIM { get; set; }
        public decimal BIRIM_FIYAT { get; set; }
        public decimal TEDARIK { get; set; }
        public decimal TUTAR { get; set; }
    }
}
