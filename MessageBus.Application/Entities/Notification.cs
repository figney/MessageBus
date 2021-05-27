using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MessageBus.Application.Entities
{
    public class Notification
    {
        [BsonElement("created_at")]
        public DateTime CreationTime { get; set; }
    }
}
