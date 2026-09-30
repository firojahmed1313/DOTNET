using GenerativeAI.Types;
using Microsoft.Extensions.AI;
using RagMultiSource.Ingestion;
using ragTutorial.Model;
using System.Numerics;

namespace ragTutorial.Service;

public class IngestionService
{
    private readonly IDocumentSource _source;
    private readonly TextChunker _chunker;
    private readonly IEmbeddingGenerator<string,Embedding<float>> _embeddingGenerator;
    private readonly IVectorStoreService _vectorStore;

    public IngestionService(
        IDocumentSource source,
        TextChunker chunker,
        IEmbeddingGenerator<string,Embedding<float>> embeddingGenerator,
        IVectorStoreService vectorStore)
    {
        _source = source;
        _chunker = chunker;
        _embeddingGenerator = embeddingGenerator;
        _vectorStore = vectorStore;
    }

    public async Task IngestAsync(CancellationToken cancellationToken = default)
    {
        await foreach (var doc in _source.LoadAsync(cancellationToken))
        {
            var chunkText = _chunker.Split(doc.Text);
            var embeddings = await _embeddingGenerator.GenerateAsync(chunkText,cancellationToken: cancellationToken);

            var chunkNumber = 0;
            foreach (var chunk in chunkText)
            {
                var vector = new KnowledgeChunk
                {
                    Id = $"{doc.SourceType}:{doc.SourceId}:{chunkNumber++}",
                    SourceType = doc.SourceType,
                    Title = doc.Title,
                    Content = chunk,
                    ChunkIndex = chunkNumber++,
                    Embedding = embeddings[chunkNumber-1].Vector
                };
                await _vectorStore.AddAsync(vector,cancellationToken);
            }
                
        }
    }
}
