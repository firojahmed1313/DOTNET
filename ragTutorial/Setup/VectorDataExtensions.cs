namespace ragTutorial.Setup;

using CommunityToolkit.VectorData.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;
using ragTutorial.Model;

public static class VectorDataExtensions
{
    public static IServiceCollection AddVectorDataStore(this IServiceCollection services)
    {
        // Register the vendor-agnostic vector store (InMemory, Qdrant, Azure AI Search, etc.)
        services.AddSingleton<VectorStore, InMemoryVectorStore>();

        // Register the typed collection directly into DI for injection
        services.AddSingleton<VectorStoreCollection<string, KnowledgeChunk>>(sp =>
        {
            var store = sp.GetRequiredService<VectorStore>();
            return store.GetCollection<string, KnowledgeChunk>("knowledge_base");
        });

        return services;
    }
}
