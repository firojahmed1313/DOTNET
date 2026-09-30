using Microsoft.Extensions.VectorData;
using ragTutorial.Model;

namespace ragTutorial.Service;

public class VectorStoreService : IVectorStoreService
{
    private readonly VectorStoreCollection<string, KnowledgeChunk> _collection;

    public VectorStoreService(VectorStoreCollection<string, KnowledgeChunk> collection)
    {
        _collection = collection;
    }
    public async Task AddAsync(KnowledgeChunk document, CancellationToken cancellationToken = default)
    {
        await _collection.UpsertAsync(document,cancellationToken);
    }

    public async Task<List<KnowledgeChunk>> SearchAsync(ReadOnlyMemory<float> vector, int topK, CancellationToken cancellationToken = default)
    {
        var results = new List<KnowledgeChunk>();

        await foreach (var result in _collection.SearchAsync(vector,topK,cancellationToken: cancellationToken))
        {
            results.Add(result.Record);
        }

        return results;
    }
}
