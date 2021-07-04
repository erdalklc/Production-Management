using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.Ayarlar
{
    public class KullaniciMarka
    {
        public int ID { get; set; }
        public string USER_CODE { get; set; }
        public int BRAND_ID { get; set; }
        public string BRAND_NAME { get; set; }
        public bool ISACTIVE { get; set; }
    }
}
