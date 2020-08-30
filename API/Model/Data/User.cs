using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SquishFaceAPI.Model.Data
{
    public class User : MongoDbModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("id")]
        public Guid Id { get; set; }
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("isReset")]
        public bool IsReset { get; set; }
        [BsonElement("resetKey")]
        public string ResetKey { get; set; }
    }
}
