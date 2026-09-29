using AiTutorial.AiTools;
using AiTutorial.Setup;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.AI;

namespace AiTutorial.Service;

public class AiChat : IAiChat
{
    private readonly IChatClient _chatClient;
    private readonly AiToolProvider _toolProvider;
    private readonly EmployeeTools _employeeTools;
    private readonly ProjectTools _projectTools;
    private readonly OrderTools _orderTools;
    public AiChat(IChatClient chatClient, AiToolProvider toolProvider, EmployeeTools employeeTools, ProjectTools projectTools, OrderTools orderTools)
    {
        _chatClient = chatClient;
        _toolProvider = toolProvider;
        _employeeTools = employeeTools;
        _projectTools = projectTools;
        _orderTools = orderTools;
    }

    public async Task<string> GetChatResponseAsync(string userInput)
    {
        try
        {

            var tools = _toolProvider.GetTools(
            _employeeTools,
            _projectTools,
            _orderTools);

            var options = new ChatOptions
            {
                Temperature = 0.2f,
                MaxOutputTokens = 500,
                Tools = [.. tools]
            };

            List<ChatMessage> chat = [
                new ChatMessage(ChatRole.System, "You are a helpful intern"),
                new ChatMessage(ChatRole.User, userInput)
            ];
            var response = await _chatClient.GetResponseAsync(chat, options);    

            //var response = await _chatClient.GetResponseAsync(userInput);
            return response.Text;
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"Error while getting chat response: {ex.Message}");
            return "I'm sorry, but I encountered an error while processing your request"; // Return a default message
        }
    }
}
