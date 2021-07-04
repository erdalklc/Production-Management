using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Models
{
    public class ADAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string USER_CODE { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_NAME { get; set; }
    }
}
