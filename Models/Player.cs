using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebCoTuong_API_ASPCore_MongoDB.Models;

public class Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? PlayerId { get; set; }
    public string? Name { get; set; }
    public Int64? TotalScore { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    
}