using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebCoTuong_API_ASPCore_MongoDB.Models;

public class Room
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? RoomId { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string? idUser1 { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string? idUser2 { get; set; }

}