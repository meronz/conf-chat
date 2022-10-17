namespace ConfChat.Shared;

public record struct ChatMessage(string Username, string Message, DateTime Timestamp);