using Microsoft.AspNetCore.SignalR;

namespace WebCoTuong_API_ASPCore_MongoDB.SignaLR.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}