using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Loglar
{
    public abstract class LOG
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string CHANGED_BY { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CHANGED_COLUMN { get; set; }
        public object OLD_VALUE { get; set; }
        public object NEW_VALUE { get; set; }
    }
}
