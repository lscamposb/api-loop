using MongoDB.Bson.Serialization.Attributes;

namespace LoopApi.Models
{
    [BsonIgnoreExtraElements]
    public class FrequentlyQuestion
    {
        [BsonId]
        [BsonElement("_id")]
        public long id { get; set; }
        public string section { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
}
