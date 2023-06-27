using MongoDB.Bson.Serialization.Attributes;

namespace LoopApi.Models
{
    [BsonIgnoreExtraElements]
    public class AuthorityCategories
    {
        [BsonId]
        [BsonElement("_id")]
        public long id { get; set; }
        public string name { get; set; }
    }
}
