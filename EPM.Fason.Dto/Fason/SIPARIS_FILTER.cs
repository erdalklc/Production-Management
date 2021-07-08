using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class SIPARIS_FILTER
    {
        public DateTime BASLANGIC_TARIHI { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public DateTime BITIS_TARIHI { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddSeconds(-1);
        public string MODEL { get; set; }
        public string COLOR { get; set; }
    }
}
