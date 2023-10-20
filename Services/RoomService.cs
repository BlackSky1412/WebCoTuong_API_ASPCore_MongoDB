
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebCoTuong_API_ASPCore_MongoDB.Configurations;
using WebCoTuong_API_ASPCore_MongoDB.Models;

namespace WebCoTuong_API_ASPCore_MongoDB.Services;

public class RoomService
{
    private readonly IMongoCollection<Room> _roomCollection;

    public RoomService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _roomCollection = mongoDb.GetCollection<Room>(databaseSettings.Value.RoomCollection);
    }

    public async Task<List<Room>> GetAsync() => await _roomCollection.Find(_ => true).ToListAsync();
    public async Task CreateAsync(Room room)
    {
        try
        {
            await _roomCollection.InsertOneAsync(room);
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine(ex.Message);

            // Throw the error to the caller
            throw;
        }
    }
    public async Task<Room> GetAsync(string id)
    {
        try
        {
            var room = await _roomCollection.Find(x => x.RoomId == id).FirstOrDefaultAsync();

            if (room == null)
            {
                throw new Exception("Room not found");
            }

            return room;
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine(ex.Message);

            // Throw the error to the caller
            throw;
        }
    }

    public async Task UpdateAsync(Room room)
    {
        try
        {
            await _roomCollection.ReplaceOneAsync(x => x.RoomId == room.RoomId, room);
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine(ex.Message);

            // Throw the error to the caller
            throw;
        }
    }

    public async Task RemoveAsync(string id)
    {
        try
        {
            await _roomCollection.DeleteOneAsync(x => x.RoomId == id);
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine(ex.Message);

            // Throw the error to the caller
            throw;
        }
    }

}