using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.Ayarlar
{
    public class KullaniciYetki
    {
        public int ID { get; set; }
        public string USER_CODE { get; set; }
        public int RESPONSIBILITY_ID { get; set; }
        public string RESPONSIBILITY_NAME { get; set; } 
        public bool ISACTIVE { get; set; }
    }
}
