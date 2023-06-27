using MongoDB.Bson.Serialization.Attributes;

namespace LoopApi.Models
{
    [BsonIgnoreExtraElements]
    public class Coupon
    {
        [BsonId]
        [BsonElement("_id")]
        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        [BsonElement("is_enable")]
        public int enable { get; set; }
        [BsonElement("store_id")]
        public long store { get; set;}
        [BsonElement("gift_points")]
        public int giftPoints { get; set; }
        [BsonElement("started_date")]
        public string startedDate { get; set; }
        [BsonElement("end_date")]
        public string endDate { get; set;}
        public string image { get; set; }
        [BsonElement("is_automatic")]
        public int automatic { get; set; }
        [BsonElement("last_modified_by")]
        public string lastModifiedBy { get; set;}
        [BsonElement("created_date")]
        public string createdDate { get; set; }
        [BsonElement("updated_date")]
        public string updatedDate { get; set;}
    }
}
