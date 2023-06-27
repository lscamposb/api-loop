using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoopApi.Models
{
    public class Ticket
    {
        [BsonId]
        [BsonElement("_id")]
        public long id { get; set; }

        [BsonElement("ticket_number")]
        public string ticket_number { get; set; }

        [BsonElement("machine_number")]
        public int machine_number { get; set; }

        [BsonElement("is_enable")]
        public int is_enable { get; set; }

        [BsonElement("created_date")]
        public string created_date { get; set; }

        [BsonElement("updated_date")]
        public string updated_date { get; set; }

        [BsonElement("is_applied")]
        public int is_applied { get; set; }

        [BsonElement("user_id")]
        public int user_id { get; set; }

        [BsonElement("_class")]
        public string class_ticket { get; set; }
    }
}
