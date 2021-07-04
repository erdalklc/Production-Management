using EPM.Core.Nesneler;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Models
{
    public class WebLogin
    {  
        public string USER_NAME { get; set; } = "";
        public string USER_CODE { get; set; } = "";
        public string EMAIL { get; set; } = "";
        public DateTime ENTERY_DATE { get; set; } = DateTime.Now;

        public List<EPM_USER_RESPONSIBILITY> responsibility { get; set; }
    }
}
