using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.FormModels.FasonTakip
{
    public class PRODUCTION_PROCESS
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public int TIME { get; set; }
        public int HANDLE_SIDE { get; set; }
    }
}
