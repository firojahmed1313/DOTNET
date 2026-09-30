using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ragTutorial.Service;

namespace ragTutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RagController : ControllerBase
{
    private readonly RagPipeline _ragPipeline;

    public RagController(RagPipeline ragPipeline)
    {
        _ragPipeline = ragPipeline;
    }

    [HttpGet("ask/{question}")]
    public async Task<IActionResult> Ask([FromRoute] string question, CancellationToken cancellationToken)
    {
        var answer = await _ragPipeline.AskAsync(question,4,cancellationToken);

        return Ok(new
        {
            question,
            answer
        });
    }
}