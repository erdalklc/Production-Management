using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.UretimTakip
{
    public class KumasDepo
    {
        public int TRANSACTION_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public string OLUSTURAN { get; set; }
        public string KALEM { get; set; }
        public string URUN_GRUBU { get; set; }
        public string ISLEM_TIPI { get; set; }
        public decimal ISLEM_MIKTARI { get; set; }
        public string BIRIM { get; set; }
    }
}
