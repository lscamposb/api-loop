using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoopApi.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonElement("_id")]
        public long id { get; set; }

        [BsonElement("login")]
        public string login { get; set; }

        [BsonElement("password_hash")]
        public string password { get; set; }

        [BsonElement("first_name")]
        public string firstName { get; set; }

        [BsonElement("last_name")]
        public string lastName { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("birth_date")]
        public string? birthDate { get; set; }

        [BsonElement("activated")]
        public bool activated { get; set; }

        [BsonElement("lang_key")]
        public string langKey { get; set; }

        [BsonElement("activation_key")]
        public string? activationKey { get; set; }

        [BsonElement("reset_key")]
        public string? resetKey { get; set; }

        [BsonElement("created_by")]
        public string createdBy { get; set; }

        [BsonElement("created_date")]
        public string createdDate { get; set; }

        [BsonElement("reset_date")]
        public string? resetDate { get; set; }

        [BsonElement("lastModifiedBy")]
        public string lastModifiedBy { get; set; }

        [BsonElement("last_modified_date")]
        public string lastModifiedDate { get; set; }

        [BsonElement("user_type")]
        public string userType { get; }

        [BsonElement("picture")]
        public string? picture { get; set; }

        [BsonElement("gift_points")]
        public GiftPoint[]? giftPoints { get; set; }

        [BsonElement("device")]
        public Device[]? device { get; set; }

        [BsonElement("authority")]
        public Authority[]? authority { get; set; }

        public class GiftPoint
        {          
            [BsonElement("phone_number")]
            public string phone_number { get; set; }

            [BsonElement("points")]
            public string points { get; set; }

            [BsonElement("code")]
            public string code { get; set; }

            [BsonElement("is_applied")]
            public string is_applied { get; }

            [BsonElement("_id")]
            public int? id { get; set; }
        }

        public class Device
        {
            [BsonId]
            [BsonElement("_id")]
            public string id { get; set; }

            [BsonElement("fcm_token")]
            public string fcm_token { get; set; }

            [BsonElement("is_active")]
            public int? is_active { get; set; }

            [BsonElement("created_date")]
            public string created_date { get; set; }

            [BsonElement("updated_date")]
            public string updated_date { get; set; }
        }

        public class Authority
        {
            [BsonElement("name")]
            public string name { get; set; }
        }          
    }
}
