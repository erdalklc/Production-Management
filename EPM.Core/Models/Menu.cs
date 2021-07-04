using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Models
{
    [Table("FDEIT005.EPM_WEB_MENU")]
    public class Menu
    {
        public int ID { get; set; }
        public int CATEGORY_ID { get; set; }
        public string ICON { get; set; }
        public string TEXT { get; set; }
        public string LOW_TEXT { get; set; }
        public string CONTROLLER { get; set; }
        public string ACTION { get; set; }
        public string RESPONSIBILITYS { get; set; }
        public bool SELECTED { get; set; }
        public bool ISEXPANDED { get; set; }
        public bool ISVISIBLE { get; set; }
    }
}
