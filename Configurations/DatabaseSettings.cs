namespace WebCoTuong_API_ASPCore_MongoDB.Configurations;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = "";
    public string DatabaseName { get; set; } = "";
    public string PlayerCollection { get; set; } = "";
    public string RoomCollection { get; set; } = "";
    
}