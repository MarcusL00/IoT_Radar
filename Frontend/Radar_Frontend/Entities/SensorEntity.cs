using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace radar_frontend.Entities
{
    public class SensorEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // MongoDB's _id field

        [BsonElement("radarId")]
        public string RadarId { get; set; }

        [BsonElement("distanceMeasured")]
        public int DistanceMeasured { get; set; }

        [BsonElement("unit")]
        public string Unit { get; set; }

        [BsonElement("rotation")]
        public int Rotation { get; set; }

        [BsonElement("timestamp")]
        public long Timestamp { get; set; }
    }
}
