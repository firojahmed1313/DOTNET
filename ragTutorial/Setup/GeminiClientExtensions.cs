namespace ragTutorial.Setup;

using GenerativeAI.Microsoft;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



public static class GeminiClientExtensions
{
    public static IServiceCollection AddGeminiAiPipeline(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Infrastructure dependencies required by the pipeline
        services.AddDistributedMemoryCache();

        // 1. Register Gemini IChatClient with the middleware pipeline
        services.AddChatClient(sp =>
        {
            var apiKey = configuration["Gemini:ApiKey"]
                ?? throw new InvalidOperationException("Gemini API key is missing.");

            var model = configuration["Gemini:Model"]
                ?? "gemini-2.5-flash";

            IChatClient client = new GenerativeAIChatClient(apiKey, model);

            return new ChatClientBuilder(client)
                .UseDistributedCache(sp.GetRequiredService<IDistributedCache>())
                .UseOpenTelemetry()
                .UseFunctionInvocation()
                .Build(sp);
        });

        // 2. Register Embedding Generator
        services.AddSingleton<IEmbeddingGenerator<string, Embedding<float>>>(sp =>
        {
            var apiKey = configuration["Gemini:ApiKey"]
                ?? throw new InvalidOperationException("Gemini API key is missing.");

            var embeddingModel = configuration["Gemini:EmbeddingModel"]
                ?? "text-embedding-004";

            IEmbeddingGenerator<string, Embedding<float>> generator = new GenerativeAIEmbeddingGenerator(apiKey, embeddingModel);
            return new EmbeddingGeneratorBuilder<string, Embedding<float>>(generator)
                    .UseDistributedCache(sp.GetRequiredService<IDistributedCache>())
                    .UseOpenTelemetry()
                    .Build(sp);
        });

        return services;
    }
}
