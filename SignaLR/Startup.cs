using WebCoTuong_API_ASPCore_MongoDB.SignaLR.Hubs;

namespace WebCoTuong_API_ASPCore_MongoDB.Services;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddSignalR();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapHub<ChatHub>("/chatHub");

        });
    }
}