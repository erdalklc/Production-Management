using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Dto.Models
{
    public class FasonSiparisListesiDetay
    {
        public int DETAIL_ID { get; set; }
        public int TAKIP_NO { get; set; }
        public string MODEL { get; set; }
        public string RENK { get; set; }
        public string SEZON { get; set; }
        public string PROCESSNAME { get; set; }
        public int STATUS { get; set; }
        public int QUEUE { get; set; }
        public DateTime PLANNING_DATE { get; set; }
        public DateTime END_DATE { get; set; }
    }
}
