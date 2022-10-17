using ConfChat.Shared;
using Microsoft.AspNetCore.SignalR;

namespace ConfChat.Server.Hubs;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
    }

    public async Task ClientToServerMessage(ChatMessage msg)
    {
        try
        {
            _logger.LogInformation("User {user} sent a message: {msg}", msg.Username, msg.Message);
            await Clients.All.SendCoreAsync("ServerToClientMessage", new[] { (object)msg });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error");
            throw;
        }
    }
}