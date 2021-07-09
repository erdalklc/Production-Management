using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Base
{
    public class EPM_PRODUCTION_SEASON_WEEKS
    {
        public int ID { get; set; }
        public int SEASON_ID { get; set; }
        public int START_WEEK { get; set; }
        public int START_YEAR { get; set; }
        public int END_WEEK { get; set; }
        public int END_YEAR { get; set; }
    }
}
