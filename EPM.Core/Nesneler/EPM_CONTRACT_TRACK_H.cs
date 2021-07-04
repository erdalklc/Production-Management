using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Nesneler
{
    [Table("FDEIT005.EPM_CONTRACT_TRACK_H")]
    public class EPM_CONTRACT_TRACK_H
    {
        [Key]
        public int ID { get; set; }
        public int HEADER_ID { get; set; }
        public int STATUS { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public int CREATED_BY { get; set; }
    }
}
