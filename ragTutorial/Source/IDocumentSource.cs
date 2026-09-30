using ragTutorial.Model;

namespace RagMultiSource.Ingestion;

/// <summary>
/// Strategy pattern: every input format (web, text file, PDF, database row, ...)
/// implements this the same way, so RagPipeline never needs to know which source it's reading.
/// Add a new source type by adding a new class — nothing else in the app changes.
/// </summary>
public interface IDocumentSource
{
     IAsyncEnumerable<SourceDocument> LoadAsync(CancellationToken cancellationToken = default);
}
