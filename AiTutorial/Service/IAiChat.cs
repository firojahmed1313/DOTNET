namespace AiTutorial.Service;

public interface IAiChat
{
    Task<string> GetChatResponseAsync(string userInput);
}
