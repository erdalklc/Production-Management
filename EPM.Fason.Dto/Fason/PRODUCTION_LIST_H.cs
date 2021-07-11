using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class PRODUCTION_LIST_H
    {
        [Key]
        public int ID { get; set; }
        public int ENTEGRATION_ID { get; set; }
        public int FIRMA_ID { get; set; }
        public int STATUS { get; set; }
        public DateTime END_DATE { get; set; }
    }
}
