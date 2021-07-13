using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Admin.Dto.Admin
{
    public class KullaniciListe
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string USER_CODE { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_NAME { get; set; }
    }
}
