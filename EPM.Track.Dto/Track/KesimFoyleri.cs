using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Dto.Track
{
    public class KesimFoyleri
    {
        public int IS_EMRI_ID { get; set; }
        public string WIP_ENTITY_NAME { get; set; }
        public string ROTA { get; set; }
        public string MAMUL_RENGI { get; set; }
        public decimal PLANLANAN_KESIM { get; set; }
        public decimal FIILI_KESIM { get; set; }
        public decimal TASNIF_MIKTARI { get; set; }
        public string IS_EMRI_NO { get; set; }
        public string BEDEN { get; set; }
        public string MODEL { get; set; }
        public string SEZON_BILGISI { get; set; }
        public string DEGERLENDIRME { get; set; }
    }
}
