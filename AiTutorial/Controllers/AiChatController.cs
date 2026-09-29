using AiTutorial.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiTutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AiChatController : ControllerBase
{
    private readonly IAiChat _aiChat;

    public AiChatController(IAiChat aiChat)
    {
        _aiChat = aiChat;
    }

    [HttpGet("/chat/{query}")]
    public async Task<String> chat(string query) {
        
        return await _aiChat.GetChatResponseAsync(query);
    }

}
