using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Advertiser.Domain.Notifications
{
    public class Notification
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("user_id")]
        public int UserId { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("socket")]
        public bool Socket { get; set; }

        [BsonElement("forced")]
        public bool Forced { get; set; }

        [BsonElement("title_slug")]
        public string TitleSlug { get; set; }

        [BsonElement("content_slug")]
        public string ContentSlug { get; set; }

        [BsonElement("params")]
        public object Params { get; set; }

        [BsonElement("data")]
        public object Data { get; set; }

        [BsonElement("read_time")]
        public DateTime ReadTime { get; set; }

        [BsonElement("is_read")]
        public bool IsRead { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdateTime { get; set; }

        [BsonElement("created_at")]
        public DateTime CreationTime { get; set; }
    }
}
