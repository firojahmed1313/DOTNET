namespace ragTutorial.Model;
using Microsoft.Extensions.VectorData;

public class KnowledgeChunk
{
    [VectorStoreKey]
    public required string Id { get; set; }

    [VectorStoreData(IsIndexed = true)]
    public required string SourceType { get; set; } // "Web", "PDF", "Text", "Database"

    [VectorStoreData]
    public required string Title { get; set; }  // URL, file path, or table row key

    [VectorStoreData]
    public required string Content { get; set; }
    [VectorStoreData]
    public int ChunkIndex { get; set; }

    [VectorStoreVector(dimensions: 4, DistanceFunction = DistanceFunction.CosineSimilarity, IndexKind = IndexKind.Hnsw)]
    public ReadOnlyMemory<float> Embedding { get; set; }
}
