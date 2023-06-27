using MongoDB.Bson.Serialization.Attributes;

namespace LoopApi.Models
{
    [BsonIgnoreExtraElements]
    public class StoreCategories
    {
        [BsonId]
        [BsonElement("_id")]
        public int id { get; set; }

        public string name { get; set; }
    }
}
