using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Loglar
{
    public class LOG_EPM_MASTER_PRODUCTION_H :LOG
    {
        public int HEADER_ID { get; set; }
    }
}
