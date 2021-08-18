using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class INSPECTION_LIST
    {
        public int ENTEGRATION_HEADER_ID { get; set; }
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string BRAND { get; set; }
        public string SEASON { get; set; }
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public int STATUS { get; set; }
        public string STATUS_EX
        {
            get
            {
                if (STATUS == 0) return "TAMAMLANDI";
                else if (STATUS == 5) return "DEVAM EDİYOR";
                else return "TANIMSIZ";
            }
        }
        public string TYPE { get; set; }
        public string TYPEEX { get; set; }
        public string FIRMA { get; set; }
        public string INSPECTOR { get; set; }
        public int QUANTITY { get; set; }
        public int QUANTITY_RELEASE { get; set; }
        public int QUANTITY_SAMPLE { get; set; }
        public DateTime START_DATE { get; set; }
    }
}
