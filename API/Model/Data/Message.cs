using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace SquishFaceAPI.Model.Data
{
    public class Message : MongoDbModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("id")]
        public Guid Id { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("by")]
        public string By { get; set; }
        [BsonElement("on")]
        public DateTime On { get; set; }
        [BsonElement("likes")]
        public List<Like> Likes { get; set; }
    }

    public class Like
    {
        [BsonElement("by")]
        public string By { get; set; }
        [BsonElement("on")]
        public DateTime On { get; set; }
    }
}
