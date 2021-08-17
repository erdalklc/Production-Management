using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class AQL_MODEL
    {
        public int ID { get; set; }
        public int HEADER_ID { get; set; }
        public int QUESTION_ID { get; set; }
        public string PRODUCT_SIZE { get; set; }
        public int MAJOR_QUANTITY { get; set; }
        public int MINOR_QUANTITY { get; set; }
        public string QUESTION { get; set; }
        public string QUANTITY { get; set; }
        public string QUANTITY_RELEASE { get; set; }
    }
}
