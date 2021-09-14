using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Models
{
    public class HaftaModel
    {
        private string _id = "";
        public string ID { get { if (_id == "") _id = YEAR.ToString() + WEEK.ToString(); return _id; } set { _id = value; } }
        public int WEEK { get; set; }
        public int YEAR { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
    }
}
