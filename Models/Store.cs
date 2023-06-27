using MongoDB.Bson.Serialization.Attributes;

namespace LoopApi.Models
{
    [BsonIgnoreExtraElements]
    public class Store
    {
        [BsonId]
        [BsonElement("_id")]
        public long id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string url_web_page { get; set; }
        public int points { get; set; }
        public long category_id { get;}
        public long store_user_id { get; set;}
        public string url_image { get; set;}
    }
}
