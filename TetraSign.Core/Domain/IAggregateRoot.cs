using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TetraSign.Core.Domain;

public interface IAggregateRoot
{
    // [BsonId]
    // [BsonRepresentation(BsonType.ObjectId)]
    string id { get; }
}