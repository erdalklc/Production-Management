using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class SIPARIS_ISLEMLER_KONTROL
    {
        public bool CAN_CREATE_AQL_FIRMA { get; set; } = false;
        public bool CAN_CREATE_AQL_CORLU { get; set; } = false;
        public bool CAN_CREATE_AQL_INLINE { get; set; } = false;
        public int ENTEGRATION_HEADER_ID { get; set; }
    }
}
