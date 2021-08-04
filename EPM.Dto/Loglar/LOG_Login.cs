using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Dto.Loglar
{
    public class LOG_Login
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string USER_NAME { get; set; } = "";
        public string USER_CODE { get; set; } = "";
        public string EMAIL { get; set; } = "";
        public DateTime ENTERY_DATE { get; set; } = DateTime.Now;
    }
}
