using ragTutorial.Model;

namespace ragTutorial.Service;

public interface IVectorStoreService
{
    Task AddAsync(
        KnowledgeChunk document,
        CancellationToken cancellationToken = default);

    Task<List<KnowledgeChunk>> SearchAsync(
        ReadOnlyMemory<float> vector,
        int topK,
        CancellationToken cancellationToken = default);
}
