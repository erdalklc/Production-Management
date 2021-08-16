using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_USER_LOGINS
    {
        public int USER_ID { get; set; } = 0;
        public string USER_NAME { get; set; } = "";
        public string EMAIL { get; set; } = "";
        public DateTime ENTERY_DATE { get; set; } = DateTime.Now;
        public bool ISINSPECTOR { get; set; }

    }
}
