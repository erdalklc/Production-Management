using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class SIPARIS_FILTER
    {
        private DateTime? _BASLANGIC_TARIHI=null;
        private DateTime? _BITIS_TARIHI = null;
        public DateTime? BASLANGIC_TARIHI
        {
            get
            {
                if (_BASLANGIC_TARIHI == null)
                    _BASLANGIC_TARIHI = new DateTime(DateTime.Now.Year-2, DateTime.Now.Month, 1);
                return _BASLANGIC_TARIHI;
            }
            set { _BASLANGIC_TARIHI = value; }
        }
        public DateTime? BITIS_TARIHI
        {
            get
            {
                if (_BITIS_TARIHI == null)
                    _BITIS_TARIHI = new DateTime(DateTime.Now.Year+2, DateTime.Now.Month + 1, 1).AddSeconds(-1);
                return _BITIS_TARIHI;
            }
            set { _BITIS_TARIHI = value; }
        } 
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public string SEASON { get; set; }
    }
}
