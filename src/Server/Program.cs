using ConfChat.Server.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
if(string.IsNullOrEmpty(redisConnectionString))
{
    builder.Services.AddSignalR();
}
else
{
    System.Console.WriteLine($"Using Redis as backplane {redisConnectionString}");
    builder.Services.AddSignalR()
        .AddStackExchangeRedis(redisConnectionString);
}

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");
app.MapFallbackToFile("index.html");
app.Run();