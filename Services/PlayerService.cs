using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebCoTuong_API_ASPCore_MongoDB.Configurations;
using WebCoTuong_API_ASPCore_MongoDB.Models;

namespace WebCoTuong_API_ASPCore_MongoDB.Services;

public class PlayerService
{
    private readonly IMongoCollection<Player> _playerCollection;

    public PlayerService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _playerCollection = mongoDb.GetCollection<Player>(databaseSettings.Value.CollectionName);
    }
    
    public async Task<List<Player>> GetAsync() => await _playerCollection.Find(_ => true).ToListAsync();
    public async Task CreateAsync(Player player)
    {
        try
        {
            await _playerCollection.InsertOneAsync(player);
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine(ex.Message);

            // Throw the error to the caller
            throw;
        }
    }
    public async Task<Player> GetAsync(string id)
    {
        try
        {
            var player = await _playerCollection.Find(x => x.PlayerId == id).FirstOrDefaultAsync();

            if (player == null)
            {
                throw new Exception("Player not found");
            }

            return player;
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine(ex.Message);

            // Throw the error to the caller
            throw;
        }
    }

    public async Task UpdateAsync(Player player)
    {
        try
        {
            await _playerCollection.ReplaceOneAsync(x => x.PlayerId == player.PlayerId, player);
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
            await _playerCollection.DeleteOneAsync(x => x.PlayerId == id);
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