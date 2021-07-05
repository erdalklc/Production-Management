using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Core.Nesneler
{
    public class PRODUCTION_FASON_USERS
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
    }
}
