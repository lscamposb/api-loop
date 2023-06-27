using MongoDB.Bson.Serialization.Attributes;

namespace LoopApi.Models
{
    [BsonIgnoreExtraElements]
    public class Code
    {
        [BsonId]
        [BsonElement("_id")]
        public long id { get; set; }
        public long coupon_id { get; set; }
        public long code { get; set; }
        public int is_active { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
    }
}
