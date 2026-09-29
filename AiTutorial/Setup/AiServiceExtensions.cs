using GenerativeAI;
using GenerativeAI.Microsoft;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;

namespace AiTutorial.Setup;

public static class AiServiceExtensions
{
    public static IServiceCollection AddAiServices(this IServiceCollection services , IConfiguration configuration)
    {
       
        // 1. Ensure supporting infrastructure is registered
        services.AddDistributedMemoryCache();
        services.AddChatClient(sp =>
        {
            // Example A: Using OpenAI
            var apiKey = configuration["GEMINI_API_KEY"]
                         ?? configuration["Gemini:ApiKey"]
                         ?? throw new InvalidOperationException("Gemini API Key is missing.");

            var model = configuration["GOOGLE_AI_MODEL"]
                        ?? configuration["Gemini:Model"]
                        ?? "gemini-3.5-flash";

            IChatClient baseClient = new GenerativeAIChatClient(apiKey, model);

            // 3. Assemble middleware decorators in the desired execution order
            return new ChatClientBuilder(baseClient)
                .UseDistributedCache(sp.GetRequiredService<IDistributedCache>()) // Caches duplicate prompts
                .UseOpenTelemetry()                                             // Captures traces and metrics
                .UseFunctionInvocation()                                        // Intercepts and executes tool calls
                .Build();
        });

        return services;
    }
}
