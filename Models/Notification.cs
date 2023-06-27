using MongoDB.Bson.Serialization.Attributes;

namespace LoopApi.Models
{
    [BsonIgnoreExtraElements]
    public class Notification
    {
        [BsonId]
        [BsonElement("_id")]
        public long id { get; set; }
        [BsonElement("user_id")]
        public long userId { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        [BsonElement("is_read")]
        public int read { get; set; }
        [BsonElement("creation_date")]
        public string creationDate { get; set; }
        [BsonElement("read_date")]
        public string readDate { get; set; }
    }
}
