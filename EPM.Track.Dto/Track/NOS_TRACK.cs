using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Dto.Track
{
    public class NOS_TRACK
    {
        //public string ID { get; set; } = Guid.NewGuid().ToString();
        public string YEAR { get; set; } = DateTime.Now.Year.ToString();
        public string MODEL { get; set; }
        public string RENK { get; set; }
        public string BEDEN { get; set; }
        public int MIN_ADET { get; set; }
        public int NOS_ADET { get; set; }
        public int NOSTR_ADET { get; set; }
        public int NOSIHR_ADET { get; set; }
        public int LOJ_ADET { get; set; }
        public int SATIS_ADET_AY { get; set; }
        public int YUZDE { get; set; }
        public int SA_URETIM_ADET { get; set; }
        public int PLANLANAN_URETIM_ADET { get; set; }
    }
}
