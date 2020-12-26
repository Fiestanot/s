using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Entity
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime? CreatedAt{ get; set; }
        public DateTime? UpdatedAt{ get; set; }
    }   
}
