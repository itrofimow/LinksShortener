using System;
using LinksShortener.Core.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LinksShortener.Core
{
    [MongoEntity("Links")]
    public class Link
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Url { get; set; }

        public string Destination { get; set; }

        public int Hits { get; set; }

        public DateTime CreatedAt { get; set; }

        public string OwnerId { get; set; }
    }
}