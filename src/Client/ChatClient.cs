using ConfChat.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ConfChat.Client;

public class ChatClient
{
    private readonly HubConnection _hubConnection;

    public Action<ChatMessage>? OnChatMessageReceived { get; set; }

    public ChatClient(NavigationManager nav)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(nav.ToAbsoluteUri("/hubs/chat"))
            .Build();
    }
    
    public async Task Start()
    {
        await _hubConnection.StartAsync();
        _hubConnection.On<ChatMessage>("ServerToClientMessage", ServerToClientMessage);
    }

    public async Task SendMessageAsync(ChatMessage chatMessage)
    {
        await _hubConnection.SendCoreAsync("ClientToServerMessage", new []{(object)chatMessage});
    }

    private Task ServerToClientMessage(ChatMessage msg)
    {
        OnChatMessageReceived?.Invoke(msg);
        return Task.CompletedTask;
    }
}