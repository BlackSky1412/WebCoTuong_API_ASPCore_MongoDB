using WebCoTuong_API_ASPCore_MongoDB.Configurations;
using WebCoTuong_API_ASPCore_MongoDB.Services;
using WebCoTuong_API_ASPCore_MongoDB.SignaLR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDBDatabase"));
builder.Services.AddSingleton<PlayerService>();
builder.Services.AddSingleton<RoomService>();

builder.Services.AddRazorPages();
builder.Services.AddSignalR(); // Thêm dịch vụ SignalR

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
    endpoints.MapHub<ChatHub>("/chatHub"); // Định tuyến hub SignalR
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
#pragma warning restore ASP0014

app.Run();