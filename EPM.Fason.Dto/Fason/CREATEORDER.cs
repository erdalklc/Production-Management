using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Fason
{
    public class CREATEORDER
    {
        public PRODUCTION_HEADER header { get; set; }
        public List<PRODUCTION_PROCESS> plan { get; set; }
        public int firma { get; set; }
        public DateTime termin { get; set; }
    }
}
